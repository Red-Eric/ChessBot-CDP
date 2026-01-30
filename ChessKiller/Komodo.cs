using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Komodo
{
    private Process _process;
    private StreamWriter _input;
    private StreamReader _output;

    public int Depth { get; set; }
    public int MultiPV { get; set; }
    public int Elo { get; set; }
    public string Personality { get; set; } = "Default";

    public Komodo(string enginePath, int elo = 2800, int depth = 10, int multipv = 5)
    {
        Elo = elo;
        Depth = depth;
        MultiPV = multipv;

        _process = new Process();
        _process.StartInfo.FileName = enginePath;
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardInput = true;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.CreateNoWindow = true;

        _process.Start();

        _input = _process.StandardInput;
        _output = _process.StandardOutput;

        SendCommand("uci");
        WaitForUciOk();
        SetOptions();
        WaitReady();
    }

    private void SetOptions()
    {
        SendCommand("setoption name UCI LimitStrength value true");
        SendCommand("setoption name Personality value Default");
        SendCommand($"setoption name MultiPV value {MultiPV}");
        SendCommand($"setoption name UCI Elo value {Elo}");
        SendCommand("setoption name Ponder value false");
    }

    private void SendCommand(string cmd)
    {
        if (_process.HasExited)
            return;

        try
        {
            _input.WriteLine(cmd);
            _input.Flush();
        }
        catch { }
    }

    private void WaitForUciOk()
    {
        string line;
        while ((line = SafeReadLine()) != null)
        {
            if (line.Trim() == "uciok")
                break;
        }
    }

    public void WaitReady()
    {
        SendCommand("isready");
        string line;
        //Console.WriteLine("Waiting for Engine.exe");
        while ((line = SafeReadLine()) != null)
        {
            if (line == "readyok")
            {
                break;
            }
        }
    }

    private string SafeReadLine()
    {
        if (_process.HasExited)
            return null;

        try
        {
            return _output.ReadLine();
        }
        catch
        {
            return null;
        }
    }

    public List<Dictionary<string, string>> GetBestMoves(string fen)
    {
        var results = new List<Dictionary<string, string>>();
        var seenMoves = new HashSet<string>();

        SendCommand($"position fen {fen}");
        WaitReady();

        SendCommand($"setoption name Personality value {Personality}");
        SendCommand("setoption name UCI LimitStrength value true");
        SendCommand($"setoption name MultiPV value {MultiPV}");
        SendCommand($"setoption name UCI Elo value {Elo}");
        WaitReady();

        SendCommand($"go depth {Depth}");

        int lastDepth = 0;
        var infoLines = new List<string>();

        string line;
        while ((line = SafeReadLine()) != null)
        {
            if (line.StartsWith("info book move"))
            {
                var p = line.Split(' ');
                if (p.Length > 4)
                {
                    string move = p[4];
                    if (move.Length >= 4 && !seenMoves.Contains(move))
                    {
                        results.Add(new Dictionary<string, string>
                        {
                            { "from", move.Substring(0, 2) },
                            { "to", move.Substring(2, 2) },
                            { "eval", "book" }
                        });
                        seenMoves.Add(move);
                    }
                }
                continue;
            }

            if (line.StartsWith("info"))
            {
                infoLines.Add(line);

                var parts = line.Split(' ');
                int depthIndex = Array.IndexOf(parts, "depth");
                if (depthIndex != -1 && depthIndex + 1 < parts.Length)
                {
                    if (int.TryParse(parts[depthIndex + 1], out int currentDepth))
                        lastDepth = currentDepth;
                }
            }

            if (line.StartsWith("bestmove"))
                break;
        }

        foreach (var infoLine in infoLines)
        {
            if (!infoLine.Contains("multipv") || !infoLine.Contains(" pv "))
                continue;

            if (!infoLine.Contains($"depth {lastDepth} "))
                continue;

            var parts = infoLine.Split(' ');

            if (parts.Length < 5)
                continue;

            int mpv = 1;
            int.TryParse(parts[2], out mpv);
            if (mpv > MultiPV)
                continue;

            string eval = null;
            int scoreIndex = Array.IndexOf(parts, "score");
            if (scoreIndex != -1 && scoreIndex + 2 < parts.Length)
            {
                string type = parts[scoreIndex + 1];
                if (int.TryParse(parts[scoreIndex + 2], out int value))
                {
                    if (fen.Split(' ')[1] == "b")
                        value = -value;

                    eval = type == "cp"
                        ? (value / 100.0).ToString("+#0.##;-#0.##", CultureInfo.InvariantCulture)
                        : $"#{value}";
                }
            }

            int pvIndex = Array.IndexOf(parts, "pv");
            if (pvIndex != -1 && pvIndex + 1 < parts.Length)
            {
                string move = parts[pvIndex + 1];
                if (move.Length >= 4 && !seenMoves.Contains(move))
                {
                    results.Add(new Dictionary<string, string>
                    {
                        { "from", move.Substring(0, 2) },
                        { "to", move.Substring(2, 2) },
                        { "eval", eval }
                    });
                    seenMoves.Add(move);
                }
            }
        }

        SendCommand("stop");
        return results;
    }

    public void Quit()
    {
        if (!_process.HasExited)
        {
            SendCommand("quit");
            _process.WaitForExit();
        }

        _process.Dispose();
    }
}

