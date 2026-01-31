using System.Drawing;
using System.Formats.Tar;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using PuppeteerSharp;

namespace ChessKiller
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        string castling = "KQkq";
        string sideChessCom = "white";
        string sideLichess = "white";
        string lastFENChessCom = "r1bnkbnr/pppp4/3q1p2/3Pp1pp/4P3/P1N2N1P/1PP1BPP1/R1BQK2R w KQkq - 1 11";
        string lastFENLichess = "r1bnkbnr/pppp4/3q1p2/3Pp1pp/4P3/P1N2N1P/1PP1BPP1/R1BQK2R w KQkq - 1 11";
        const string updateURL = "aHR0cHM6Ly9hcGkuZ2l0aHViLmNvbS9yZXBvcy9SZWQtRXJpYy9DaGVzc0JvdC1DRFAvY29udGVudHMvQ2hlc3NLaWxsZXIvdmVyc2lvbi50eHQ=";

        string browserPath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe";
        private CancellationTokenSource cts;
        private List<string> pers = new List<string>
        {
            "Default",
            "Aggressive",
            "Defensive",
            "Active",
            "Positional",
            "Endgame",
            "Beginner",
            "Human"
        };
        private List<string> descriptions = new List<string>
        {
            "Balanced Komodo Dragon 3.3 settings, no bias",
            "Prefers attacks, sacrifices material for initiative",
            "Plays solid, prioritizes king safety and structure",
            "Seeks activity, open lines, piece mobility",
            "Long-term planning, structure and space advantage",
            "Optimized evaluation for simplified positions",
            "Introduces inaccuracies and human-like mistakes",
            "Adds randomness, hesitation and imperfect choices"
        };
        private List<String> firstMove = new List<string>
        {
            "e2e4",
            "d2d4",
            "c2c4",
        };
        private int autoMoveDelay = 0;
        Random rnd = new Random();
        bool autoMove = false;
        bool siteFlag = false;
        bool injected = false;
        Komodo engine = new Komodo("engine.exe", 3500, 10, 5);

        public Form1()
        {
            InitializeComponent();
            personalities.Items.AddRange(pers.ToArray());
            personalities.SelectedIndex = 0;
            persDescr.Text = descriptions[0];
            //ConsoleManager.AllocConsole();

            this.Load += Form1_Load;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string m = await GetUpdateAsync();
            cts = new CancellationTokenSource();
            // U or N
            if (m == "U")
            {
                MessageBox.Show(
                    "If you had an error or something, go to the Discord server (Chess Killer Community) and press 'Info' for more information.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                await AutomateBrowserAsync(cts.Token);
            }
        }

        public static async Task<string> GetUpdateAsync()
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("ChessKiller-Updater");

            HttpResponseMessage response = await client.GetAsync(Encoding.UTF8.GetString(Convert.FromBase64String(updateURL)));
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(json);
            string base64Content = doc.RootElement.GetProperty("content").GetString();

            byte[] bytes = Convert.FromBase64String(base64Content);
            string version = Encoding.UTF8.GetString(bytes).Trim();

            return version;
        }


        private void elo_value_Scroll(object sender, EventArgs e)
        {

            elo_text_value.Text = elo_value.Value.ToString();
            engine.Elo = (int)elo_value.Value;
            lastFENLichess = "";
            lastFENChessCom = "";
        }

        private void arrow_value_Scroll(object sender, EventArgs e)
        {
            arrow_text_value.Text = arrow_value.Value.ToString();
            engine.MultiPV = (int)arrow_value.Value;
            lastFENLichess = "";
            lastFENChessCom = "";
        }

        private void depth_value_Scroll(object sender, EventArgs e)
        {
            depth_text_value.Text = depth_value.Value.ToString();
            engine.Depth = (int)depth_value.Value;
            lastFENLichess = "";
            lastFENChessCom = "";
        }

        private void autoDelay_value_Scroll(object sender, EventArgs e)
        {
            autoMove_delay_Text_value.Text = autoDelay_value.Value.ToString();
            autoMoveDelay = rnd.Next(0, (int)autoDelay_value.Value);
            lastFENLichess = "";
            lastFENChessCom = "";
        }

        private void auto_move_value_CheckedChanged(object sender, EventArgs e)
        {

            if (auto_move_value.Checked)
            {
                autoMove = true;
                lastFENLichess = "";
                lastFENChessCom = "";
            }
            else
            {
                autoMove = false;
            }
        }

        private void personalities_SelectedIndexChanged(object sender, EventArgs e)
        {
            engine.Personality = personalities.Text;
            persDescr.Text = descriptions[personalities.SelectedIndex];
            lastFENLichess = "";
            lastFENChessCom = "";
        }

        //////// Hack thread

        private async Task AutomateBrowserAsync(CancellationToken token)
        {
            // Open Browser

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                ExecutablePath = browserPath,
                DefaultViewport = null,
                Args = new[] { "--start-maximized" }
            });

            //var pages = await browser.PagesAsync();
            IPage page = null;

            while (!token.IsCancellationRequested)
            {
                try
                {

                    if (page == null)
                    {
                        var existingPages = await browser.PagesAsync();
                        page = existingPages.FirstOrDefault() ?? await browser.NewPageAsync();
                        injected = false;
                        await page.EvaluateExpressionOnNewDocumentAsync("function clearHighlightSquares() {\r\n    const url = window.location.href;\r\n    if (!url.includes(\"lichess\") && !url.includes(\"chess.com\")) {\r\n        return;\r\n    }\r\n    document.querySelectorAll(\".customH\").forEach((el) => el.remove());\r\n}");
                        await page.EvaluateExpressionOnNewDocumentAsync("function highlightMovesOnBoard(moves, side, fen_) {\r\n    clearHighlightSquares();\r\n\r\n    if (!Array.isArray(moves)) return;\r\n    if (\r\n        !(\r\n            (side === \"w\" && fen_.split(\" \")[1] === \"w\") ||\r\n            (side === \"b\" && fen_.split(\" \")[1] === \"b\")\r\n        )\r\n    ) {\r\n        return;\r\n    }\r\n\r\n    let parent;\r\n    const url = window.location.href;\r\n\r\n    if (url.includes(\"chess.com\")) {\r\n        parent = document.querySelector(\"wc-chess-board\");\r\n    } else if (url.includes(\"lichess\")) {\r\n        parent = document.querySelector(\"cg-container\");\r\n    } else {\r\n        return;\r\n    }\r\n\r\n    if (!parent) return;\r\n\r\n    const squareSize = parent.offsetWidth / 8;\r\n    const maxMoves = 5;\r\n    const colors = [\"blue\", \"green\", \"yellow\", \"orange\", \"red\"];\r\n\r\n    parent.querySelectorAll(\".customH\").forEach((el) => el.remove());\r\n\r\n    function squareToPosition(square) {\r\n        const fileChar = square[0];\r\n        const rankChar = square[1];\r\n        const rank = parseInt(rankChar, 10) - 1;\r\n\r\n        let file;\r\n        if (side === \"w\") {\r\n            file = fileChar.charCodeAt(0) - \"a\".charCodeAt(0);\r\n            const y = (7 - rank) * squareSize;\r\n            const x = file * squareSize;\r\n            return { x, y };\r\n        } else {\r\n            file = \"h\".charCodeAt(0) - fileChar.charCodeAt(0);\r\n            const y = rank * squareSize;\r\n            const x = file * squareSize;\r\n            return { x, y };\r\n        }\r\n    }\r\n\r\n    function drawArrow(fromSquare, toSquare, color, score) {\r\n        const from = squareToPosition(fromSquare);\r\n        const to = squareToPosition(toSquare);\r\n\r\n        const svg = document.createElementNS(\"http://www.w3.org/2000/svg\", \"svg\");\r\n        svg.setAttribute(\"class\", \"customH\");\r\n        svg.setAttribute(\"width\", parent.offsetWidth);\r\n        svg.setAttribute(\"height\", parent.offsetWidth);\r\n        svg.style.position = \"absolute\";\r\n        svg.style.left = \"0\";\r\n        svg.style.top = \"0\";\r\n        svg.style.pointerEvents = \"none\";\r\n        svg.style.overflow = \"visible\";\r\n        svg.style.zIndex = \"10\";\r\n\r\n        const defs = document.createElementNS(\"http://www.w3.org/2000/svg\", \"defs\");\r\n        const marker = document.createElementNS(\"http://www.w3.org/2000/svg\", \"marker\");\r\n        marker.setAttribute(\"id\", `arrowhead-${color}`);\r\n        marker.setAttribute(\"markerWidth\", \"3.5\");\r\n        marker.setAttribute(\"markerHeight\", \"2.5\");\r\n        marker.setAttribute(\"refX\", \"1.75\");\r\n        marker.setAttribute(\"refY\", \"1.25\");\r\n        marker.setAttribute(\"orient\", \"auto\");\r\n        marker.setAttribute(\"markerUnits\", \"strokeWidth\");\r\n\r\n        const arrowPath = document.createElementNS(\"http://www.w3.org/2000/svg\", \"path\");\r\n        arrowPath.setAttribute(\"d\", \"M0,0 L3.5,1.25 L0,2.5 Z\");\r\n        arrowPath.setAttribute(\"fill\", color);\r\n        marker.appendChild(arrowPath);\r\n        defs.appendChild(marker);\r\n        svg.appendChild(defs);\r\n\r\n        const line = document.createElementNS(\"http://www.w3.org/2000/svg\", \"line\");\r\n        line.setAttribute(\"x1\", from.x + squareSize / 2);\r\n        line.setAttribute(\"y1\", from.y + squareSize / 2);\r\n        line.setAttribute(\"x2\", to.x + squareSize / 2);\r\n        line.setAttribute(\"y2\", to.y + squareSize / 2);\r\n        line.setAttribute(\"stroke\", color);\r\n        line.setAttribute(\"stroke-width\", \"5\");\r\n        line.setAttribute(\"marker-end\", `url(#arrowhead-${color})`);\r\n        line.setAttribute(\"opacity\", \"0.6\");\r\n        svg.appendChild(line);\r\n\r\n        if (score !== undefined) {\r\n            const text = document.createElementNS(\"http://www.w3.org/2000/svg\", \"text\");\r\n            text.setAttribute(\"x\", to.x + squareSize - 4);\r\n            text.setAttribute(\"y\", to.y + 12);\r\n            text.setAttribute(\"fill\", color);\r\n            text.setAttribute(\"font-size\", \"13\");\r\n            text.setAttribute(\"font-weight\", \"bold\");\r\n            text.setAttribute(\"text-anchor\", \"end\");\r\n            text.setAttribute(\"alignment-baseline\", \"hanging\");\r\n            text.setAttribute(\"opacity\", \"1\");\r\n            text.textContent = score;\r\n            svg.appendChild(text);\r\n        }\r\n\r\n        parent.appendChild(svg);\r\n    }\r\n\r\n    parent.style.position = \"relative\";\r\n\r\n    moves.slice(0, maxMoves).forEach((move, index) => {\r\n        const color = colors[index] || \"red\";\r\n        drawArrow(move.from, move.to, color, move.eval);\r\n    });\r\n}");
                        await page.EvaluateExpressionOnNewDocumentAsync("function createEvalBar(initialScore = \"0.0\", initialColor = \"white\") {\r\n    const url = window.location.href;\r\n    let boardContainer;\r\n\r\n    if (url.includes(\"chess.com\")) {\r\n        boardContainer = document.querySelector(\".board\");\r\n    } else if (url.includes(\"lichess\")) {\r\n        boardContainer = document.querySelector(\"cg-board\");\r\n    } else {\r\n        console.error(\"Plateau non trouvé pour ce site !\");\r\n        return;\r\n    }\r\n\r\n    if (!boardContainer) return console.error(\"Plateau non trouvé !\");\r\n\r\n    let w_ = boardContainer.offsetWidth;\r\n\r\n    // Conteneur principal\r\n    let evalContainer = document.getElementById(\"customEval\");\r\n    let topBar, bottomBar, scoreText;\r\n\r\n    if (!evalContainer) {\r\n        evalContainer = document.createElement(\"div\");\r\n        evalContainer.id = \"customEval\";\r\n        evalContainer.style.zIndex = \"9999\";\r\n        evalContainer.style.width = `${(w_ * 6) / 100}px`;\r\n        evalContainer.style.height = `${boardContainer.offsetWidth}px`;\r\n        evalContainer.style.background = \"#eee\";\r\n        evalContainer.style.position = \"relative\";\r\n        evalContainer.style.border = \"1px solid #aaa\";\r\n        evalContainer.style.borderRadius = \"4px\";\r\n        evalContainer.style.overflow = \"hidden\";\r\n\r\n        if (url.includes(\"chess.com\")) {\r\n            evalContainer.style.marginLeft = \"10px\";\r\n        } else {\r\n            evalContainer.style.left = \"-50px\";\r\n        }\r\n\r\n        topBar = document.createElement(\"div\");\r\n        bottomBar = document.createElement(\"div\");\r\n\r\n        [topBar, bottomBar].forEach(bar => {\r\n            bar.style.width = \"100%\";\r\n            bar.style.position = \"absolute\";\r\n            bar.style.transition = \"height 0.3s ease\";\r\n        });\r\n\r\n        topBar.style.top = \"0\";\r\n        bottomBar.style.bottom = \"0\";\r\n\r\n        evalContainer.appendChild(topBar);\r\n        evalContainer.appendChild(bottomBar);\r\n\r\n        // Texte score\r\n        scoreText = document.createElement(\"div\");\r\n        scoreText.style.position = \"absolute\";\r\n        scoreText.style.bottom = \"0\";\r\n        scoreText.style.left = \"50%\";\r\n        scoreText.style.transform = \"translateX(-50%)\";\r\n        scoreText.style.color = \"red\";\r\n        scoreText.style.fontWeight = \"bold\";\r\n        scoreText.style.fontSize = \"12px\";\r\n        scoreText.style.pointerEvents = \"none\";\r\n        evalContainer.appendChild(scoreText);\r\n\r\n        // Insertion dans le DOM\r\n        if (url.includes(\"chess.com\")) {\r\n            boardContainer.parentNode.style.display = \"flex\";\r\n            boardContainer.parentNode.insertBefore(evalContainer, boardContainer);\r\n        } else {\r\n            boardContainer.parentNode.insertBefore(evalContainer, boardContainer);\r\n        }\r\n\r\n    } else {\r\n        topBar = evalContainer.children[0];\r\n        bottomBar = evalContainer.children[1];\r\n        scoreText = evalContainer.children[2];\r\n    }\r\n\r\n    // Fonction pour parser le score\r\n    function parseScore(scoreStr) {\r\n        if (!scoreStr) return { score: 0, mate: false };\r\n\r\n        scoreStr = scoreStr.trim();\r\n        let mate = scoreStr.startsWith(\"#\");\r\n        if (mate) scoreStr = scoreStr.slice(1);\r\n\r\n        let score = parseFloat(scoreStr.replace(\"+\", \"\")) || 0;\r\n        return { score, mate };\r\n    }\r\n\r\n    let { score, mate } = parseScore(initialScore);\r\n    let percent = 50;\r\n\r\n    if (mate) {\r\n        let sign = score > 0 ? \"+\" : \"-\";\r\n        scoreText.textContent = \"#\" + sign + Math.abs(score);\r\n        percent =\r\n            (score > 0 && initialColor === \"white\") ||\r\n            (score < 0 && initialColor === \"black\")\r\n                ? 100\r\n                : 0;\r\n    } else {\r\n        let sign = score > 0 ? \"+\" : \"\";\r\n        scoreText.textContent = sign + score.toFixed(1);\r\n        if (initialColor === \"black\") score = -score;\r\n\r\n        if (score >= 7) percent = 90;\r\n        else if (score <= -7) percent = 10;\r\n        else percent = 50 + (score / 7) * 40;\r\n    }\r\n\r\n    // Couleurs barres\r\n    if (initialColor === \"white\") {\r\n        bottomBar.style.background = \"#ffffff\";\r\n        topBar.style.background = \"#312e2b\";\r\n    } else {\r\n        bottomBar.style.background = \"#312e2b\";\r\n        topBar.style.background = \"#ffffff\";\r\n    }\r\n\r\n    bottomBar.style.height = percent + \"%\";\r\n    topBar.style.height = 100 - percent + \"%\";\r\n}");
                        await page.EvaluateExpressionOnNewDocumentAsync("function clickButtonByTextIncludes(text) {\r\n    const buttons = document.querySelectorAll(\"button\");\r\n\r\n    for (const btn of buttons) {\r\n        if (btn.innerText && btn.innerText.includes(text)) {\r\n            btn.click();\r\n            return true;\r\n        }\r\n    }\r\n    return false;\r\n}");
                        await page.EvaluateExpressionOnNewDocumentAsync("function clickNewOpponent() {\r\n    const btn = document.querySelector(\".fbt.new-opponent\");\r\n    if (btn) {\r\n        btn.click();\r\n        return true;\r\n    }\r\n    return false;\r\n}\r\n");
                        await page.EvaluateExpressionOnNewDocumentAsync("function getGameObject() {\r\n  if (window.game) return window.game;\r\n  const board = document.querySelector(\".board\");\r\n  if (board && board.game) {\r\n    return board.game;\r\n  }\r\n  return null;\r\n}\r\n\r\nfunction movePiece(from, to, promotion = \"q\", moveDelay = 0) {\r\n  const game = getGameObject();\r\n  if (!game) return false;\r\n\r\n  const legal = game.getLegalMoves();\r\n  let move = legal.find((m) => m.from === from && m.to === to);\r\n  if (!move) return false;\r\n\r\n  if (promotion && move.promotionTypes) {\r\n    move.promotionType = promotion;\r\n  }\r\n\r\n  setTimeout(() => {\r\n    try {\r\n      game.move({ ...move, animate: true, userGenerated: true });\r\n    } catch (err) {\r\n      console.log(\"err de deplacement\");\r\n    }\r\n  }, moveDelay);\r\n\r\n  return true;\r\n}\r\n");

                        await page.EvaluateExpressionOnNewDocumentAsync(@"
                            (function() {
                                const OrigWS = window.WebSocket;
                                window._chessWS = null;
                
                                window.WebSocket = function(url, proto) {
                                    const ws = new OrigWS(url, proto);
                                    window._chessWS = ws;
                                    return ws;
                                };
                            })();
                        ");
                        continue;
                    }

                    if (page != null)
                    {

                        if (page != null && !string.IsNullOrEmpty(page.Url) && page.Url.Contains("chess.com"))
                        {
                            await page.WaitForSelectorAsync("wc-chess-board");
                            if (autoMove)
                            {
                                await page.EvaluateExpressionAsync("clickButtonByTextIncludes('Nouvelle')");
                                await page.EvaluateExpressionAsync("clickButtonByTextIncludes('new')");
                            }

                            if(autoMove && lastFENChessCom.Contains("/pppppppp/8/8/8/8/PPPPPPPP/") && sideChessCom == "white")
                            {
                                lastFENChessCom = "";
                            }

                            string fen = await page.EvaluateExpressionAsync<string>(@"document.querySelector('wc-chess-board').game.getFEN();");
                            int sideIndicator = await page.EvaluateExpressionAsync<int>(@"document.querySelector('wc-chess-board').game.getPlayingAs();");

                            sideChessCom = sideIndicator == 1 ? "white" : "black";

                            if (fen != lastFENChessCom)
                            {
                                await page.EvaluateExpressionAsync("if (typeof clearHighlightSquares === \"function\") {\r\n    clearHighlightSquares();\r\n}");
                                lastFENChessCom = fen;
                                var moves = engine.GetBestMoves(fen);

                                var moves2 = moves.Select(m => new
                                {
                                    from = m["from"].ToString(),
                                    to = m["to"].ToString(),
                                    eval = m["eval"].ToString()
                                }).ToList();

                                await page.EvaluateFunctionAsync(@"
                                (moves, side, fen) => {
                                    if (typeof highlightMovesOnBoard === 'function') {
                                        highlightMovesOnBoard(moves, side, fen);
                                    }
                                }
                                ", moves2, sideChessCom[0], fen);


                                if (autoMove && moves.Count > 0 && sideChessCom[0] == fen.Split(' ')[1][0])
                                {
                                    int randomDelay = rnd.Next(0, autoMoveDelay + 1);

                                    await page.EvaluateFunctionAsync(
                                        "movePiece",
                                        moves[0]["from"],
                                        moves[0]["to"],
                                        "q",
                                        randomDelay
                                    );
                                }

                                if (moves.Count > 0)
                                {
                                    await page.EvaluateFunctionAsync(@"
                                    (evalScore, side) => {
                                        if (typeof createEvalBar === 'function') {
                                            createEvalBar(evalScore, side);
                                        }
                                    }
                                    ", moves[0]["eval"], sideChessCom);


                                }
                            }


                        }

                        if (page != null && !string.IsNullOrEmpty(page.Url) && page.Url.Contains("lichess"))
                        {
                            await page.WaitForSelectorAsync("cg-board");
                            if (autoMove)
                            {
                                await page.EvaluateExpressionAsync("clickNewOpponent()");
                            }

                            //Console.Clear();
                            bool flag = await page.EvaluateFunctionAsync<bool>(@"() => {
                            try {
                                return (
                                    typeof site !== 'undefined' &&
                                    site.sound &&
                                    typeof site.sound.move === 'function'
                                );
                            } catch (e) {
                                return false;
                            }
                            }");

                            //Console.WriteLine("true");

                            if (flag)
                            {
                                await page.EvaluateExpressionAsync("window.castling = \"\";\r\n\r\n(function hookSoundMove() {\r\n\r\n    window.fen_ = window.fen_ || null;\r\n\r\n    const originalMove = site.sound.move;\r\n\r\n    site.sound.move = function(x) {\r\n        try {\r\n            let data = (typeof x === \"string\") ? JSON.parse(x) : x;\r\n\r\n            if (data.fen && data.uci && typeof data.ply === \"number\") {\r\n                let uci = data.uci;\r\n                let san = data.san;\r\n                let board = data.fen;\r\n                let ply = data.ply;\r\n\r\n                // reset au début\r\n                if (ply < 2) window.castling = \"KQkq\";\r\n\r\n                const from = uci.slice(0, 2);\r\n                const to = uci.slice(2, 4);\r\n                const isWhite = ply % 2 != 0;\r\n\r\n                /* ===== WHITE ===== */\r\n                if (isWhite) {\r\n                    if (from === \"e1\") window.castling = window.castling.replace(\"K\", \"\").replace(\"Q\", \"\");\r\n                    if (from === \"h1\") window.castling = window.castling.replace(\"K\", \"\").replace(\"Q\", \"\");\r\n                    if (from === \"a1\") window.castling = window.castling.replace(\"Q\", \"\").replace(\"Q\", \"\");\r\n                }\r\n\r\n                /* ===== BLACK ===== */\r\n                if (!isWhite) {\r\n                    if (from === \"e8\") window.castling = window.castling.replace(\"k\", \"\").replace(\"q\", \"\");\r\n                    if (from === \"h8\") window.castling = window.castling.replace(\"k\", \"\").replace(\"q\", \"\");\r\n                    if (from === \"a8\") window.castling = window.castling.replace(\"q\", \"\").replace(\"q\", \"\");\r\n                }\r\n\r\n                // roque explicite\r\n\r\n                if((san === \"O-O\" || san === \"O-O-O\") && isWhite) window.castling = window.castling.replace(\"KQ\", \"\")\r\n                if((san === \"O-O\" || san === \"O-O-O\") && !isWhite) window.castling = window.castling.replace(\"kq\", \"\")\r\n\r\n                if (window.castling === \"\") window.castling = \"-\";\r\n\r\n                const side = isWhite ? \"b\" : \"w\";\r\n                const fullmove = Math.floor(ply / 2) + 1;\r\n\r\n                const fenFinal = `${board} ${side} ${window.castling} - 0 ${fullmove}`;\r\n                window.fen_ = fenFinal;\r\n\r\n                console.log(window.fen_);\r\n            }\r\n\r\n        } catch (e) {\r\n            console.error(\"Erreur site.sound.move hook:\", e);\r\n        }\r\n\r\n        return originalMove.apply(this, arguments);\r\n    };\r\n\r\n})();\r\n");
                                siteFlag = true;

                            }
                            else
                            {
                                //Console.WriteLine("flag is false");
                                siteFlag = false;
                            }

                            ///
                            if (siteFlag)
                            {
                                string fen = await page.EvaluateFunctionAsync<string>("() => window.fen_") ?? "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

                                sideLichess = await page.EvaluateExpressionAsync<string>(@"
                                    (() => {
                                        if (document.querySelector('.cg-wrap.orientation-white.manipulable')) return 'white';
                                        if (document.querySelector('.cg-wrap.orientation-black.manipulable')) return 'black';
                                        return 'white';
                                    })()
                                    ");

                                if (autoMove && fen.Contains("/pppppppp/8/8/8/8/PPPPPPPP/") && sideLichess == "white")
                                {
                                    await page.EvaluateFunctionAsync(@"
                                        (uci) => {
                                            if (!window._chessWS) return;
                                            window._chessWS.send(JSON.stringify({
                                                t: 'move',
                                                d: { u: uci, a: 1 }
                                            }));
                                        }", firstMove[rnd.Next(0, 3)]);
                                }

                                if (lastFENLichess != fen)
                                {
                                    lastFENLichess = fen;
                                    await page.EvaluateExpressionAsync("clearHighlightSquares();");


                                    var moves = engine.GetBestMoves(fen);

                                    var moves2 = moves.Select(m => new
                                    {
                                        from = m["from"].ToString(),
                                        to = m["to"].ToString(),
                                        eval = m["eval"].ToString()
                                    }).ToList();

                                    await page.EvaluateFunctionAsync(@"(moves, side, fen) =>{highlightMovesOnBoard(moves, side, fen);}", moves2, sideLichess[0], lastFENLichess);

                                    if (moves.Count > 0)
                                    {
                                        await page.EvaluateFunctionAsync(
                                        "(evalScore, side) => createEvalBar(evalScore, side)",
                                        moves[0]["eval"],
                                        sideLichess
                                        );
                                    }

                                    if (moves.Count > 0 && autoMove)
                                    {
                                        string uciSend = moves[0]["from"] + moves[0]["to"];

                                        int sleepTime = rnd.Next(0, autoMoveDelay);

                                        Thread.Sleep(sleepTime);

                                        await page.EvaluateFunctionAsync(@"
                                        (uci) => {
                                            if (!window._chessWS) return;
                                            window._chessWS.send(JSON.stringify({
                                                t: 'move',
                                                d: { u: uci, a: 1 }
                                            }));
                                        }", uciSend);
                                    }
                                }
                            }




                        }


                    }



                }
                catch (Exception ex)
                {
                    //Console.Clear();
                    //Console.WriteLine("=== EXCEPTION ===");
                    //Console.WriteLine($"Type      : {ex.GetType().FullName}");
                    //Console.WriteLine($"Message   : {ex.Message}");
                    //Console.WriteLine($"Source    : {ex.Source}");
                    //Console.WriteLine("StackTrace:");
                    //Console.WriteLine(ex.StackTrace);
                    //Console.WriteLine("=================");
                    continue;
                }
                await Task.Delay(150, token);
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DiscordCard discordCardForm = new DiscordCard();
            discordCardForm.ShowDialog();
        }
    }
}
