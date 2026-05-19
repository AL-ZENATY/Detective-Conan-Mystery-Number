namespace MysteryNumberZy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                introMusic?.Dispose();
                try { introVideoPlayer?.Ctlcontrols.stop(); } catch { }
                try { introVideoPlayer?.close(); } catch { }
                introVideoPlayer?.Dispose();


                components.Dispose();


            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlayZy = new System.Windows.Forms.Button();
            this.grpSetupZy = new System.Windows.Forms.GroupBox();
            this.lblFontNameZy = new System.Windows.Forms.Label();
            this.btnFontRightZy = new System.Windows.Forms.Button();
            this.btnFontLeftZy = new System.Windows.Forms.Button();
            this.btnGoZy = new System.Windows.Forms.Button();
            this.btnDefaultZy = new System.Windows.Forms.Button();
            this.cmbAttemptsZy = new System.Windows.Forms.ComboBox();
            this.tbxStopZy = new System.Windows.Forms.TextBox();
            this.tbxStartZy = new System.Windows.Forms.TextBox();
            this.btnClearZy = new System.Windows.Forms.Button();
            this.grpInfoZy = new System.Windows.Forms.GroupBox();
            this.btnMusicZy = new System.Windows.Forms.Button();
            this.rtbLogZy = new System.Windows.Forms.RichTextBox();
            this.btnAboutZy = new System.Windows.Forms.Button();
            this.btnLocateZy = new System.Windows.Forms.Button();
            this.btnCheatZy = new System.Windows.Forms.Button();
            this.grpPlayZy = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbxGuessZy = new System.Windows.Forms.RichTextBox();
            this.lblMyGuessZy = new System.Windows.Forms.Label();
            this.pbxHotZy = new System.Windows.Forms.PictureBox();
            this.pbxWarmZy = new System.Windows.Forms.PictureBox();
            this.pbxColdZy = new System.Windows.Forms.PictureBox();
            this.pbxIceZy = new System.Windows.Forms.PictureBox();
            this.tbrTempZy = new System.Windows.Forms.TrackBar();
            this.pbAttemptsZy = new System.Windows.Forms.ProgressBar();
            this.btnGuessZy = new System.Windows.Forms.Button();
            this.lblAttemptsWrongZy = new System.Windows.Forms.Label();
            this.lblAttemptsLeftZy = new System.Windows.Forms.Label();
            this.lblResultZy = new System.Windows.Forms.Label();
            this.pbxIntroZy = new System.Windows.Forms.PictureBox();
            this.pbxGifZy = new System.Windows.Forms.PictureBox();
            this.grpSetupZy.SuspendLayout();
            this.grpInfoZy.SuspendLayout();
            this.grpPlayZy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHotZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxWarmZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColdZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIceZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTempZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIntroZy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGifZy)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlayZy
            // 
            this.btnPlayZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.introPlayBtnNG;
            this.btnPlayZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlayZy.Location = new System.Drawing.Point(170, 653);
            this.btnPlayZy.Name = "btnPlayZy";
            this.btnPlayZy.Size = new System.Drawing.Size(427, 303);
            this.btnPlayZy.TabIndex = 3;
            this.btnPlayZy.Text = "button1";
            this.btnPlayZy.UseVisualStyleBackColor = true;
            // 
            // grpSetupZy
            // 
            this.grpSetupZy.BackColor = System.Drawing.Color.Transparent;
            this.grpSetupZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.grpSetupZy.Controls.Add(this.lblFontNameZy);
            this.grpSetupZy.Controls.Add(this.btnFontRightZy);
            this.grpSetupZy.Controls.Add(this.btnFontLeftZy);
            this.grpSetupZy.Controls.Add(this.btnGoZy);
            this.grpSetupZy.Controls.Add(this.btnDefaultZy);
            this.grpSetupZy.Controls.Add(this.cmbAttemptsZy);
            this.grpSetupZy.Controls.Add(this.tbxStopZy);
            this.grpSetupZy.Controls.Add(this.tbxStartZy);
            this.grpSetupZy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpSetupZy.Location = new System.Drawing.Point(0, -87);
            this.grpSetupZy.Name = "grpSetupZy";
            this.grpSetupZy.Size = new System.Drawing.Size(859, 1148);
            this.grpSetupZy.TabIndex = 0;
            this.grpSetupZy.TabStop = false;
            // 
            // lblFontNameZy
            // 
            this.lblFontNameZy.AutoSize = true;
            this.lblFontNameZy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblFontNameZy.Location = new System.Drawing.Point(163, 771);
            this.lblFontNameZy.Name = "lblFontNameZy";
            this.lblFontNameZy.Size = new System.Drawing.Size(100, 25);
            this.lblFontNameZy.TabIndex = 8;
            this.lblFontNameZy.Text = "Font Style";
            this.lblFontNameZy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFontRightZy
            // 
            this.btnFontRightZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.fontSwitchRightBtnNG;
            this.btnFontRightZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFontRightZy.Location = new System.Drawing.Point(262, 729);
            this.btnFontRightZy.Name = "btnFontRightZy";
            this.btnFontRightZy.Size = new System.Drawing.Size(122, 119);
            this.btnFontRightZy.TabIndex = 7;
            this.btnFontRightZy.UseVisualStyleBackColor = true;
            // 
            // btnFontLeftZy
            // 
            this.btnFontLeftZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.fontSwitchLeftBtnNG;
            this.btnFontLeftZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFontLeftZy.Location = new System.Drawing.Point(49, 732);
            this.btnFontLeftZy.Name = "btnFontLeftZy";
            this.btnFontLeftZy.Size = new System.Drawing.Size(119, 115);
            this.btnFontLeftZy.TabIndex = 6;
            this.btnFontLeftZy.UseVisualStyleBackColor = true;
            // 
            // btnGoZy
            // 
            this.btnGoZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.readyPlayBtnNG;
            this.btnGoZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGoZy.Location = new System.Drawing.Point(591, 799);
            this.btnGoZy.Name = "btnGoZy";
            this.btnGoZy.Size = new System.Drawing.Size(235, 248);
            this.btnGoZy.TabIndex = 5;
            this.btnGoZy.UseVisualStyleBackColor = true;
            // 
            // btnDefaultZy
            // 
            this.btnDefaultZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.defaultSetupBtnNG;
            this.btnDefaultZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefaultZy.Location = new System.Drawing.Point(22, 894);
            this.btnDefaultZy.Name = "btnDefaultZy";
            this.btnDefaultZy.Size = new System.Drawing.Size(223, 170);
            this.btnDefaultZy.TabIndex = 4;
            this.btnDefaultZy.UseVisualStyleBackColor = true;
            // 
            // cmbAttemptsZy
            // 
            this.cmbAttemptsZy.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cmbAttemptsZy.FormattingEnabled = true;
            this.cmbAttemptsZy.Items.AddRange(new object[] {
            "Select a Number",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbAttemptsZy.Location = new System.Drawing.Point(99, 689);
            this.cmbAttemptsZy.Name = "cmbAttemptsZy";
            this.cmbAttemptsZy.Size = new System.Drawing.Size(239, 21);
            this.cmbAttemptsZy.TabIndex = 3;
            // 
            // tbxStopZy
            // 
            this.tbxStopZy.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbxStopZy.Location = new System.Drawing.Point(89, 411);
            this.tbxStopZy.Multiline = true;
            this.tbxStopZy.Name = "tbxStopZy";
            this.tbxStopZy.Size = new System.Drawing.Size(138, 28);
            this.tbxStopZy.TabIndex = 2;
            // 
            // tbxStartZy
            // 
            this.tbxStartZy.BackColor = System.Drawing.Color.Linen;
            this.tbxStartZy.Location = new System.Drawing.Point(91, 320);
            this.tbxStartZy.Multiline = true;
            this.tbxStartZy.Name = "tbxStartZy";
            this.tbxStartZy.Size = new System.Drawing.Size(138, 29);
            this.tbxStartZy.TabIndex = 1;
            // 
            // btnClearZy
            // 
            this.btnClearZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.clearBtnNG;
            this.btnClearZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearZy.Location = new System.Drawing.Point(875, 316);
            this.btnClearZy.Name = "btnClearZy";
            this.btnClearZy.Size = new System.Drawing.Size(144, 123);
            this.btnClearZy.TabIndex = 1;
            this.btnClearZy.UseVisualStyleBackColor = true;
            // 
            // grpInfoZy
            // 
            this.grpInfoZy.BackColor = System.Drawing.Color.Transparent;
            this.grpInfoZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grpInfoZy.Controls.Add(this.btnMusicZy);
            this.grpInfoZy.Controls.Add(this.rtbLogZy);
            this.grpInfoZy.Controls.Add(this.btnAboutZy);
            this.grpInfoZy.Controls.Add(this.btnLocateZy);
            this.grpInfoZy.Controls.Add(this.btnClearZy);
            this.grpInfoZy.Controls.Add(this.btnCheatZy);
            this.grpInfoZy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpInfoZy.Location = new System.Drawing.Point(859, 2);
            this.grpInfoZy.Name = "grpInfoZy";
            this.grpInfoZy.Size = new System.Drawing.Size(1065, 740);
            this.grpInfoZy.TabIndex = 1;
            this.grpInfoZy.TabStop = false;
            // 
            // btnMusicZy
            // 
            this.btnMusicZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.musicNG;
            this.btnMusicZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMusicZy.Location = new System.Drawing.Point(-8, 162);
            this.btnMusicZy.Name = "btnMusicZy";
            this.btnMusicZy.Size = new System.Drawing.Size(299, 266);
            this.btnMusicZy.TabIndex = 10;
            this.btnMusicZy.Text = "\r\n";
            this.btnMusicZy.UseVisualStyleBackColor = true;
            // 
            // rtbLogZy
            // 
            this.rtbLogZy.Location = new System.Drawing.Point(378, 209);
            this.rtbLogZy.Name = "rtbLogZy";
            this.rtbLogZy.Size = new System.Drawing.Size(373, 345);
            this.rtbLogZy.TabIndex = 0;
            this.rtbLogZy.Text = "";
            // 
            // btnAboutZy
            // 
            this.btnAboutZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.aboutBtnNG;
            this.btnAboutZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAboutZy.Location = new System.Drawing.Point(884, 481);
            this.btnAboutZy.Name = "btnAboutZy";
            this.btnAboutZy.Size = new System.Drawing.Size(126, 132);
            this.btnAboutZy.TabIndex = 3;
            this.btnAboutZy.UseVisualStyleBackColor = true;
            // 
            // btnLocateZy
            // 
            this.btnLocateZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.locateBtnNG;
            this.btnLocateZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocateZy.Location = new System.Drawing.Point(880, 402);
            this.btnLocateZy.Name = "btnLocateZy";
            this.btnLocateZy.Size = new System.Drawing.Size(134, 124);
            this.btnLocateZy.TabIndex = 4;
            this.btnLocateZy.UseVisualStyleBackColor = true;
            // 
            // btnCheatZy
            // 
            this.btnCheatZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.cheatBtnNG;
            this.btnCheatZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCheatZy.Location = new System.Drawing.Point(864, 114);
            this.btnCheatZy.Name = "btnCheatZy";
            this.btnCheatZy.Size = new System.Drawing.Size(165, 236);
            this.btnCheatZy.TabIndex = 2;
            this.btnCheatZy.UseVisualStyleBackColor = true;
            // 
            // grpPlayZy
            // 
            this.grpPlayZy.BackColor = System.Drawing.Color.Transparent;
            this.grpPlayZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.grpPlayZy.Controls.Add(this.pictureBox1);
            this.grpPlayZy.Controls.Add(this.tbxGuessZy);
            this.grpPlayZy.Controls.Add(this.lblMyGuessZy);
            this.grpPlayZy.Controls.Add(this.pbxHotZy);
            this.grpPlayZy.Controls.Add(this.pbxWarmZy);
            this.grpPlayZy.Controls.Add(this.pbxColdZy);
            this.grpPlayZy.Controls.Add(this.pbxIceZy);
            this.grpPlayZy.Controls.Add(this.tbrTempZy);
            this.grpPlayZy.Controls.Add(this.pbAttemptsZy);
            this.grpPlayZy.Controls.Add(this.btnGuessZy);
            this.grpPlayZy.Controls.Add(this.lblAttemptsWrongZy);
            this.grpPlayZy.Controls.Add(this.lblAttemptsLeftZy);
            this.grpPlayZy.Controls.Add(this.lblResultZy);
            this.grpPlayZy.Location = new System.Drawing.Point(859, 732);
            this.grpPlayZy.Name = "grpPlayZy";
            this.grpPlayZy.Size = new System.Drawing.Size(1077, 329);
            this.grpPlayZy.TabIndex = 2;
            this.grpPlayZy.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MysteryNumberZy.Properties.Resources.walletBtn;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(909, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 61);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbxGuessZy
            // 
            this.tbxGuessZy.Font = new System.Drawing.Font("PMingLiU-ExtB", 18F, System.Drawing.FontStyle.Bold);
            this.tbxGuessZy.ForeColor = System.Drawing.Color.Maroon;
            this.tbxGuessZy.Location = new System.Drawing.Point(250, 66);
            this.tbxGuessZy.Name = "tbxGuessZy";
            this.tbxGuessZy.Size = new System.Drawing.Size(269, 80);
            this.tbxGuessZy.TabIndex = 12;
            this.tbxGuessZy.Text = "";
            // 
            // lblMyGuessZy
            // 
            this.lblMyGuessZy.AutoSize = true;
            this.lblMyGuessZy.Font = new System.Drawing.Font("Cambria Math", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMyGuessZy.ForeColor = System.Drawing.Color.Cornsilk;
            this.lblMyGuessZy.Location = new System.Drawing.Point(170, 87);
            this.lblMyGuessZy.Name = "lblMyGuessZy";
            this.lblMyGuessZy.Size = new System.Drawing.Size(98, 73);
            this.lblMyGuessZy.TabIndex = 11;
            this.lblMyGuessZy.Text = "My Guess:";
            // 
            // pbxHotZy
            // 
            this.pbxHotZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.hotTempNC;
            this.pbxHotZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxHotZy.Location = new System.Drawing.Point(925, 159);
            this.pbxHotZy.Name = "pbxHotZy";
            this.pbxHotZy.Size = new System.Drawing.Size(39, 43);
            this.pbxHotZy.TabIndex = 10;
            this.pbxHotZy.TabStop = false;
            // 
            // pbxWarmZy
            // 
            this.pbxWarmZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.warmTempNC;
            this.pbxWarmZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxWarmZy.Location = new System.Drawing.Point(826, 159);
            this.pbxWarmZy.Name = "pbxWarmZy";
            this.pbxWarmZy.Size = new System.Drawing.Size(39, 43);
            this.pbxWarmZy.TabIndex = 9;
            this.pbxWarmZy.TabStop = false;
            // 
            // pbxColdZy
            // 
            this.pbxColdZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.coldTempNC;
            this.pbxColdZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxColdZy.Location = new System.Drawing.Point(704, 159);
            this.pbxColdZy.Name = "pbxColdZy";
            this.pbxColdZy.Size = new System.Drawing.Size(39, 43);
            this.pbxColdZy.TabIndex = 8;
            this.pbxColdZy.TabStop = false;
            // 
            // pbxIceZy
            // 
            this.pbxIceZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.iceColdTempNC;
            this.pbxIceZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxIceZy.Location = new System.Drawing.Point(607, 159);
            this.pbxIceZy.Name = "pbxIceZy";
            this.pbxIceZy.Size = new System.Drawing.Size(39, 43);
            this.pbxIceZy.TabIndex = 7;
            this.pbxIceZy.TabStop = false;
            // 
            // tbrTempZy
            // 
            this.tbrTempZy.BackColor = System.Drawing.SystemColors.Control;
            this.tbrTempZy.Location = new System.Drawing.Point(611, 138);
            this.tbrTempZy.Name = "tbrTempZy";
            this.tbrTempZy.Size = new System.Drawing.Size(350, 45);
            this.tbrTempZy.TabIndex = 6;
            // 
            // pbAttemptsZy
            // 
            this.pbAttemptsZy.BackColor = System.Drawing.SystemColors.ControlText;
            this.pbAttemptsZy.ForeColor = System.Drawing.Color.Black;
            this.pbAttemptsZy.Location = new System.Drawing.Point(767, 16);
            this.pbAttemptsZy.Name = "pbAttemptsZy";
            this.pbAttemptsZy.Size = new System.Drawing.Size(193, 26);
            this.pbAttemptsZy.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAttemptsZy.TabIndex = 5;
            // 
            // btnGuessZy
            // 
            this.btnGuessZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.guessBtnNG;
            this.btnGuessZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuessZy.Location = new System.Drawing.Point(333, 106);
            this.btnGuessZy.Name = "btnGuessZy";
            this.btnGuessZy.Size = new System.Drawing.Size(201, 134);
            this.btnGuessZy.TabIndex = 4;
            this.btnGuessZy.UseVisualStyleBackColor = true;
            // 
            // lblAttemptsWrongZy
            // 
            this.lblAttemptsWrongZy.AutoSize = true;
            this.lblAttemptsWrongZy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblAttemptsWrongZy.ForeColor = System.Drawing.SystemColors.Window;
            this.lblAttemptsWrongZy.Location = new System.Drawing.Point(765, 79);
            this.lblAttemptsWrongZy.Name = "lblAttemptsWrongZy";
            this.lblAttemptsWrongZy.Size = new System.Drawing.Size(50, 16);
            this.lblAttemptsWrongZy.TabIndex = 2;
            this.lblAttemptsWrongZy.Text = "Wrong:";
            // 
            // lblAttemptsLeftZy
            // 
            this.lblAttemptsLeftZy.AutoSize = true;
            this.lblAttemptsLeftZy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblAttemptsLeftZy.ForeColor = System.Drawing.SystemColors.Window;
            this.lblAttemptsLeftZy.Location = new System.Drawing.Point(628, 79);
            this.lblAttemptsLeftZy.Name = "lblAttemptsLeftZy";
            this.lblAttemptsLeftZy.Size = new System.Drawing.Size(62, 16);
            this.lblAttemptsLeftZy.TabIndex = 1;
            this.lblAttemptsLeftZy.Text = "Attempts:";
            // 
            // lblResultZy
            // 
            this.lblResultZy.AutoSize = true;
            this.lblResultZy.Font = new System.Drawing.Font("Calisto MT", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblResultZy.ForeColor = System.Drawing.SystemColors.Menu;
            this.lblResultZy.Location = new System.Drawing.Point(659, 21);
            this.lblResultZy.Name = "lblResultZy";
            this.lblResultZy.Size = new System.Drawing.Size(91, 24);
            this.lblResultZy.TabIndex = 0;
            this.lblResultZy.Text = "RESULT";
            this.lblResultZy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxIntroZy
            // 
            this.pbxIntroZy.BackColor = System.Drawing.SystemColors.ControlText;
            this.pbxIntroZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.introBackgroundBB1;
            this.pbxIntroZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxIntroZy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxIntroZy.Location = new System.Drawing.Point(0, 0);
            this.pbxIntroZy.Name = "pbxIntroZy";
            this.pbxIntroZy.Size = new System.Drawing.Size(1924, 1061);
            this.pbxIntroZy.TabIndex = 0;
            this.pbxIntroZy.TabStop = false;
            // 
            // pbxGifZy
            // 
            this.pbxGifZy.BackgroundImage = global::MysteryNumberZy.Properties.Resources.whiteZy;
            this.pbxGifZy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxGifZy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxGifZy.Location = new System.Drawing.Point(0, 0);
            this.pbxGifZy.Name = "pbxGifZy";
            this.pbxGifZy.Size = new System.Drawing.Size(1924, 1061);
            this.pbxGifZy.TabIndex = 4;
            this.pbxGifZy.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::MysteryNumberZy.Properties.Resources.gameBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.grpSetupZy);
            this.Controls.Add(this.grpInfoZy);
            this.Controls.Add(this.btnPlayZy);
            this.Controls.Add(this.grpPlayZy);
            this.Controls.Add(this.pbxIntroZy);
            this.Controls.Add(this.pbxGifZy);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpSetupZy.ResumeLayout(false);
            this.grpSetupZy.PerformLayout();
            this.grpInfoZy.ResumeLayout(false);
            this.grpPlayZy.ResumeLayout(false);
            this.grpPlayZy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHotZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxWarmZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColdZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIceZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTempZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIntroZy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGifZy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayZy;
        private System.Windows.Forms.GroupBox grpSetupZy;
        private System.Windows.Forms.Label lblFontNameZy;
        private System.Windows.Forms.Button btnFontRightZy;
        private System.Windows.Forms.Button btnFontLeftZy;
        private System.Windows.Forms.Button btnGoZy;
        private System.Windows.Forms.Button btnDefaultZy;
        private System.Windows.Forms.ComboBox cmbAttemptsZy;
        private System.Windows.Forms.TextBox tbxStopZy;
        private System.Windows.Forms.TextBox tbxStartZy;
        private System.Windows.Forms.Button btnClearZy;
        private System.Windows.Forms.GroupBox grpInfoZy;
        private System.Windows.Forms.Button btnMusicZy;
        private System.Windows.Forms.RichTextBox rtbLogZy;
        private System.Windows.Forms.Button btnAboutZy;
        private System.Windows.Forms.Button btnLocateZy;
        private System.Windows.Forms.Button btnCheatZy;
        private System.Windows.Forms.GroupBox grpPlayZy;
        private System.Windows.Forms.Label lblMyGuessZy;
        private System.Windows.Forms.PictureBox pbxHotZy;
        private System.Windows.Forms.PictureBox pbxWarmZy;
        private System.Windows.Forms.PictureBox pbxColdZy;
        private System.Windows.Forms.PictureBox pbxIceZy;
        private System.Windows.Forms.TrackBar tbrTempZy;
        private System.Windows.Forms.ProgressBar pbAttemptsZy;
        private System.Windows.Forms.Button btnGuessZy;
        private System.Windows.Forms.Label lblAttemptsWrongZy;
        private System.Windows.Forms.Label lblAttemptsLeftZy;
        private System.Windows.Forms.Label lblResultZy;
        private System.Windows.Forms.PictureBox pbxIntroZy;
        private System.Windows.Forms.RichTextBox tbxGuessZy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbxGifZy;
    }
}
