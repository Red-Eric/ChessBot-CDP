namespace ChessKiller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            titleApp = new Label();
            label2 = new Label();
            elo_text_value = new Label();
            elo_value = new TrackBar();
            label3 = new Label();
            label4 = new Label();
            arrow_value = new TrackBar();
            arrow_text_value = new Label();
            label6 = new Label();
            label7 = new Label();
            depth_value = new TrackBar();
            depth_text_value = new Label();
            label9 = new Label();
            auto_move_value = new CheckBox();
            label10 = new Label();
            label11 = new Label();
            autoDelay_value = new TrackBar();
            autoMove_delay_Text_value = new Label();
            label13 = new Label();
            label14 = new Label();
            personalities = new ComboBox();
            persDescr = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)elo_value).BeginInit();
            ((System.ComponentModel.ISupportInitialize)arrow_value).BeginInit();
            ((System.ComponentModel.ISupportInitialize)depth_value).BeginInit();
            ((System.ComponentModel.ISupportInitialize)autoDelay_value).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 54, 61);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(titleApp);
            panel1.Location = new Point(-2, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(536, 52);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(48, 54, 61);
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.icons8_info_144;
            pictureBox1.Location = new Point(447, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 30);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(48, 54, 61);
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = Properties.Resources.icons8_effacer_144;
            pictureBox3.Location = new Point(489, 9);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(30, 30);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // titleApp
            // 
            titleApp.Font = new Font("Cambria", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            titleApp.ForeColor = Color.Red;
            titleApp.Location = new Point(0, 0);
            titleApp.Name = "titleApp";
            titleApp.Size = new Size(533, 52);
            titleApp.TabIndex = 0;
            titleApp.Text = "ChessKiller";
            titleApp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(13, 17, 23);
            label2.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(230, 237, 243);
            label2.Location = new Point(12, 68);
            label2.Name = "label2";
            label2.Size = new Size(89, 44);
            label2.TabIndex = 1;
            label2.Text = "ELO";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // elo_text_value
            // 
            elo_text_value.BackColor = Color.FromArgb(13, 17, 23);
            elo_text_value.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            elo_text_value.ForeColor = Color.FromArgb(230, 237, 243);
            elo_text_value.Location = new Point(429, 68);
            elo_text_value.Name = "elo_text_value";
            elo_text_value.Size = new Size(89, 44);
            elo_text_value.TabIndex = 2;
            elo_text_value.Text = "3500";
            elo_text_value.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // elo_value
            // 
            elo_value.LargeChange = 100;
            elo_value.Location = new Point(12, 115);
            elo_value.Maximum = 3500;
            elo_value.Minimum = 100;
            elo_value.Name = "elo_value";
            elo_value.Size = new Size(506, 56);
            elo_value.SmallChange = 100;
            elo_value.TabIndex = 3;
            elo_value.TickFrequency = 100;
            elo_value.TickStyle = TickStyle.None;
            elo_value.Value = 3500;
            elo_value.Scroll += elo_value_Scroll;
            // 
            // label3
            // 
            label3.Font = new Font("Cambria", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(139, 148, 158);
            label3.Location = new Point(25, 138);
            label3.Name = "label3";
            label3.Size = new Size(493, 33);
            label3.TabIndex = 4;
            label3.Text = "Limits engine strength to simulate human rating";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Cambria", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(139, 148, 158);
            label4.Location = new Point(25, 244);
            label4.Name = "label4";
            label4.Size = new Size(493, 33);
            label4.TabIndex = 8;
            label4.Text = "Number of best candidate moves shown by engine";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // arrow_value
            // 
            arrow_value.Location = new Point(12, 221);
            arrow_value.Maximum = 5;
            arrow_value.Minimum = 2;
            arrow_value.Name = "arrow_value";
            arrow_value.Size = new Size(506, 56);
            arrow_value.TabIndex = 7;
            arrow_value.TabStop = false;
            arrow_value.TickStyle = TickStyle.None;
            arrow_value.Value = 5;
            arrow_value.Scroll += arrow_value_Scroll;
            // 
            // arrow_text_value
            // 
            arrow_text_value.BackColor = Color.FromArgb(13, 17, 23);
            arrow_text_value.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            arrow_text_value.ForeColor = Color.FromArgb(230, 237, 243);
            arrow_text_value.Location = new Point(429, 174);
            arrow_text_value.Name = "arrow_text_value";
            arrow_text_value.Size = new Size(89, 44);
            arrow_text_value.TabIndex = 6;
            arrow_text_value.Text = "5";
            arrow_text_value.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(13, 17, 23);
            label6.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(230, 237, 243);
            label6.Location = new Point(12, 174);
            label6.Name = "label6";
            label6.Size = new Size(89, 44);
            label6.TabIndex = 5;
            label6.Text = "Arrows";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.Font = new Font("Cambria", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(139, 148, 158);
            label7.Location = new Point(25, 362);
            label7.Name = "label7";
            label7.Size = new Size(493, 33);
            label7.TabIndex = 12;
            label7.Text = "Search depth: higher = stronger but slower";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // depth_value
            // 
            depth_value.Location = new Point(12, 339);
            depth_value.Maximum = 20;
            depth_value.Minimum = 1;
            depth_value.Name = "depth_value";
            depth_value.Size = new Size(506, 56);
            depth_value.TabIndex = 11;
            depth_value.TickStyle = TickStyle.None;
            depth_value.Value = 10;
            depth_value.Scroll += depth_value_Scroll;
            // 
            // depth_text_value
            // 
            depth_text_value.BackColor = Color.FromArgb(13, 17, 23);
            depth_text_value.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            depth_text_value.ForeColor = Color.FromArgb(230, 237, 243);
            depth_text_value.Location = new Point(429, 292);
            depth_text_value.Name = "depth_text_value";
            depth_text_value.Size = new Size(89, 44);
            depth_text_value.TabIndex = 10;
            depth_text_value.Text = "10";
            depth_text_value.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.BackColor = Color.FromArgb(13, 17, 23);
            label9.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(230, 237, 243);
            label9.Location = new Point(12, 292);
            label9.Name = "label9";
            label9.Size = new Size(89, 44);
            label9.TabIndex = 9;
            label9.Text = "Depth";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // auto_move_value
            // 
            auto_move_value.AutoSize = true;
            auto_move_value.Location = new Point(481, 431);
            auto_move_value.Name = "auto_move_value";
            auto_move_value.Size = new Size(18, 17);
            auto_move_value.TabIndex = 13;
            auto_move_value.TextAlign = ContentAlignment.MiddleCenter;
            auto_move_value.UseVisualStyleBackColor = true;
            auto_move_value.CheckedChanged += auto_move_value_CheckedChanged;
            // 
            // label10
            // 
            label10.BackColor = Color.FromArgb(13, 17, 23);
            label10.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(230, 237, 243);
            label10.Location = new Point(12, 413);
            label10.Name = "label10";
            label10.Size = new Size(240, 44);
            label10.TabIndex = 14;
            label10.Text = "Auto Move";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.Font = new Font("Cambria", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(139, 148, 158);
            label11.Location = new Point(25, 536);
            label11.Name = "label11";
            label11.Size = new Size(481, 33);
            label11.TabIndex = 18;
            label11.Text = "Random delay between 0 and the selected value to avoid patterns";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // autoDelay_value
            // 
            autoDelay_value.LargeChange = 100;
            autoDelay_value.Location = new Point(12, 513);
            autoDelay_value.Maximum = 20000;
            autoDelay_value.Name = "autoDelay_value";
            autoDelay_value.Size = new Size(506, 56);
            autoDelay_value.SmallChange = 100;
            autoDelay_value.TabIndex = 17;
            autoDelay_value.TickFrequency = 100;
            autoDelay_value.TickStyle = TickStyle.None;
            autoDelay_value.Scroll += autoDelay_value_Scroll;
            // 
            // autoMove_delay_Text_value
            // 
            autoMove_delay_Text_value.BackColor = Color.FromArgb(13, 17, 23);
            autoMove_delay_Text_value.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            autoMove_delay_Text_value.ForeColor = Color.FromArgb(230, 237, 243);
            autoMove_delay_Text_value.Location = new Point(429, 466);
            autoMove_delay_Text_value.Name = "autoMove_delay_Text_value";
            autoMove_delay_Text_value.Size = new Size(89, 44);
            autoMove_delay_Text_value.TabIndex = 16;
            autoMove_delay_Text_value.Text = "0";
            autoMove_delay_Text_value.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.BackColor = Color.FromArgb(13, 17, 23);
            label13.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.FromArgb(230, 237, 243);
            label13.Location = new Point(12, 466);
            label13.Name = "label13";
            label13.Size = new Size(196, 44);
            label13.TabIndex = 15;
            label13.Text = "AutoMove Delay";
            label13.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.BackColor = Color.FromArgb(13, 17, 23);
            label14.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.FromArgb(230, 237, 243);
            label14.Location = new Point(12, 591);
            label14.Name = "label14";
            label14.Size = new Size(128, 44);
            label14.TabIndex = 19;
            label14.Text = "Personality";
            label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // personalities
            // 
            personalities.BackColor = Color.FromArgb(48, 54, 61);
            personalities.Cursor = Cursors.Hand;
            personalities.Font = new Font("Cambria", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            personalities.ForeColor = Color.FromArgb(230, 237, 243);
            personalities.FormattingEnabled = true;
            personalities.Location = new Point(161, 600);
            personalities.Name = "personalities";
            personalities.Size = new Size(345, 35);
            personalities.TabIndex = 20;
            personalities.SelectedIndexChanged += personalities_SelectedIndexChanged;
            // 
            // persDescr
            // 
            persDescr.Font = new Font("Cambria", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            persDescr.ForeColor = Color.FromArgb(139, 148, 158);
            persDescr.Location = new Point(25, 652);
            persDescr.Name = "persDescr";
            persDescr.Size = new Size(481, 33);
            persDescr.TabIndex = 21;
            persDescr.Text = "Balanced Komodo Dragon 3.3 settings, no bias";
            persDescr.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 17, 23);
            ClientSize = new Size(529, 714);
            ControlBox = false;
            Controls.Add(persDescr);
            Controls.Add(personalities);
            Controls.Add(label14);
            Controls.Add(label11);
            Controls.Add(autoDelay_value);
            Controls.Add(autoMove_delay_Text_value);
            Controls.Add(label13);
            Controls.Add(label10);
            Controls.Add(auto_move_value);
            Controls.Add(label7);
            Controls.Add(depth_value);
            Controls.Add(depth_text_value);
            Controls.Add(label9);
            Controls.Add(label4);
            Controls.Add(arrow_value);
            Controls.Add(arrow_text_value);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(elo_value);
            Controls.Add(elo_text_value);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)elo_value).EndInit();
            ((System.ComponentModel.ISupportInitialize)arrow_value).EndInit();
            ((System.ComponentModel.ISupportInitialize)depth_value).EndInit();
            ((System.ComponentModel.ISupportInitialize)autoDelay_value).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label titleApp;
        private Label label2;
        private Label elo_text_value;
        private TrackBar elo_value;
        private Label label3;
        private Label label4;
        private TrackBar arrow_value;
        private Label arrow_text_value;
        private Label label6;
        private Label label7;
        private TrackBar depth_value;
        private Label depth_text_value;
        private Label label9;
        private CheckBox auto_move_value;
        private Label label10;
        private Label label11;
        private TrackBar autoDelay_value;
        private Label autoMove_delay_Text_value;
        private Label label13;
        private Label label14;
        private ComboBox personalities;
        private Label persDescr;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
    }
}
