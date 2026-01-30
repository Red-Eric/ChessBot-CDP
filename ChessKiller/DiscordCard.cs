using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessKiller
{
    public partial class DiscordCard : Form
    {
        public DiscordCard()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // go to discord 
            Process.Start(new ProcessStartInfo("https://discord.gg/WtGDhSYCxE") { UseShellExecute = true });
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/@Redson_Eric") { UseShellExecute = true });

        }
    }
}
