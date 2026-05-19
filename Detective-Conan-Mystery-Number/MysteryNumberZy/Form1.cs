// ============================================================
//  MysteryNumberZy - Detective Conan Style
//  Developer: Zy (Abdullah Zenaty)
// ============================================================

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace MysteryNumberZy
{
    public partial class Form1 : Form
    {
        // engine = all game rules
        private GameLogicZy _engine = new GameLogicZy();
        private readonly GameLogicZy game = new GameLogicZy();

        private const int DefaultAttemptsZy = 10;

        // simple font list for left/right buttons
        private readonly string[] _availableFontsZy = { "Segoe UI", "Comic Sans MS", "Impact", "Arial Black" };
        private int _currentFontIndexZy = 0;

        // Intro audio/video
        private readonly MusicManagerZy introMusic = new MusicManagerZy();
        private AxWindowsMediaPlayer introVideoPlayer;   // hidden until we play
        private string introVideoPath;                   // temp MP4 on disk
        private bool introVideoStarted = false;          // guard double start
        private bool introMusicKilled = false;

        // ===== In-game BGM + SFX =====
        private readonly MusicManagerZy bgmMusic = new MusicManagerZy();
        private bool bgmIsOn = false;      // actual playback state
        private bool bgmDesiredOn = true;  // user toggle (default ON)
        private Timer bgmDelayTimer;       // 3s delay after ShowGameSetup

        // One-shot SFX / jingles (WIN/LOSE + music toggle SFX)
        private WindowsMediaPlayer sfxPlayer = new WindowsMediaPlayer();
        private string sfxOnPath;
        private string sfxOffPath;
        private string winPath;
        private string losePath;

        // Fade-out for jingles when starting a new game
        private Timer sfxFadeTimer;
        private bool jingleActive = false;

        // ===== FAST UI SFX (preloaded & primed WMP per sound) =====
        private class UiSfxEntry
        {
            public WindowsMediaPlayer Player;
            public bool Primed;
            public string Path;
        }
        private readonly Dictionary<string, UiSfxEntry> uiSfx = new Dictionary<string, UiSfxEntry>();

        public Form1()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // --- UI wiring ---
            btnPlayZy.Parent = pbxIntroZy;
            btnPlayZy.BackColor = Color.Transparent;
            btnPlayZy.FlatStyle = FlatStyle.Flat;
            btnPlayZy.FlatAppearance.BorderSize = 0;
            btnPlayZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnPlayZy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnPlayZy.UseVisualStyleBackColor = false;
            btnPlayZy.BringToFront();

            btnGoZy.BackgroundImage = Properties.Resources.readyPlayBtnNG;
            btnDefaultZy.BackgroundImage = Properties.Resources.defaultSetupBtnNG;
            btnGuessZy.BackgroundImage = Properties.Resources.guessBtnNG;

            WireEvents();

            TransparentHelperZy.AttachHover(btnPlayZy, Properties.Resources.introPlayBtnNG, Properties.Resources.introPlayBtnWG);
            TransparentHelperZy.AttachHover(btnGoZy, Properties.Resources.readyPlayBtnNG, Properties.Resources.readyPlayBtnWG);
            TransparentHelperZy.AttachHover(btnDefaultZy, Properties.Resources.defaultSetupBtnNG, Properties.Resources.defaultSetupBtnWG);
            TransparentHelperZy.AttachHover(btnGuessZy, Properties.Resources.guessBtnNG, Properties.Resources.guessBtnWG);
            TransparentHelperZy.AttachHover(btnCheatZy, Properties.Resources.cheatBtnNG, Properties.Resources.cheatBtnWG);
            TransparentHelperZy.AttachHover(btnClearZy, Properties.Resources.clearBtnNG, Properties.Resources.clearBtnWG);
            TransparentHelperZy.AttachHover(btnLocateZy, Properties.Resources.locateBtnNG, Properties.Resources.btnLocateWG);
            TransparentHelperZy.AttachHover(btnAboutZy, Properties.Resources.aboutBtnNG, Properties.Resources.aboutBtnWG);
            TransparentHelperZy.AttachHover(btnFontLeftZy, Properties.Resources.fontSwitchLeftBtnNG, Properties.Resources.fontSwitchLeftBtnWG);
            TransparentHelperZy.AttachHover(btnFontRightZy, Properties.Resources.fontSwitchRightBtnNG, Properties.Resources.fontSwitchRightBtnWG);
            TransparentHelperZy.AttachHover(btnMusicZy, Properties.Resources.musicNG, Properties.Resources.musicWG);

            // Guess box (transparent rich box with hover PNG)
            tbxGuessZy = TransparentHelperZy.AttachHover(tbxGuessZy, Properties.Resources.guessTbxNG, Properties.Resources.guessTbxWG);
            CenterRtbText(tbxGuessZy); // one-time nudge; its own events keep it centered

            // Log box: transparent skin + inner padding
            rtbLogZy = TransparentHelperZy.AttachBackground(rtbLogZy, new Padding(18, 18, 18, 18));
            // Hide scrollbar handle and auto-scroll to bottom on new text (and keep centered)
            rtbLogZy.ScrollBars = RichTextBoxScrollBars.None;
            rtbLogZy.TextChanged += (s2, e2) =>
            {
                CenterRtbText(rtbLogZy);
                rtbLogZy.SelectionStart = rtbLogZy.TextLength;
                rtbLogZy.ScrollToCaret();
            };
            CenterRtbText(rtbLogZy);

            // Keep the font label centered between arrows at all times
            lblFontNameZy.AutoSize = false;
            lblFontNameZy.TextAlign = ContentAlignment.MiddleCenter;
            lblFontNameZy.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            btnPlayZy.MouseEnter += (s, ev) => pbxIntroZy.BackgroundImage = Properties.Resources.introBackgroundRB2;
            btnPlayZy.MouseLeave += (s, ev) => pbxIntroZy.BackgroundImage = Properties.Resources.introBackgroundBB1;

            ApplyFont(_availableFontsZy[_currentFontIndexZy]);

            // --- Hot/Cold meter (horizontal, slim) ---
            HotColdMeterZy.SetupHorizontalSlimMeter(tbrTempZy, barHeight: 12, overlayHeight: 16);
            HotColdMeterZy.Update(tbrTempZy, 0); // start at 0 heat
            ShowTempZone("ICE");

            // Intro screen visible; main groups hidden
            grpSetupZy.Visible = false;
            grpPlayZy.Visible = false;
            grpInfoZy.Visible = false;

            // ---- Intro Audio: loop immediately ----
            introMusic.StartLoopFromResource(Properties.Resources.gameIntro, ".mp3", startVolume: 80);

            // ---- Intro Video Player (hidden, no UI) ----
            introVideoPlayer = new AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(introVideoPlayer)).BeginInit();
            introVideoPlayer.CreateControl();
            introVideoPlayer.uiMode = "none";
            introVideoPlayer.stretchToFit = true;
            introVideoPlayer.settings.autoStart = false;   // never autoplay
            introVideoPlayer.Dock = DockStyle.Fill;
            introVideoPlayer.Visible = false;
            this.Controls.Add(introVideoPlayer);
            introVideoPlayer.SendToBack();
            ((System.ComponentModel.ISupportInitialize)(introVideoPlayer)).EndInit();

            // Video end -> go to game
            introVideoPlayer.PlayStateChange += (s, ev) =>
            {
                if ((WMPPlayState)ev.newState == WMPPlayState.wmppsMediaEnded)
                {
                    introVideoPlayer.Visible = false;
                    ShowGameSetup();
                }
            };

            // Save the intro video to temp
            if (introVideoPath == null)
            {
                introVideoPath = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_introVideo.mp4");
                try { File.WriteAllBytes(introVideoPath, Properties.Resources.introVideo); } catch { }
            }

            introMusic.FadeCompleted += (s, finalVol) =>
            {
                if (finalVol == 0 && !introVideoStarted)
                {
                    introVideoStarted = true;
                    StartIntroVideo();
                }
            };

            // ====== Prepare SFX one-shots (music toggle + end jingles) ======
            try
            {
                sfxOnPath  = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_sfxMusicOn.mp3");
                sfxOffPath = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_sfxMusicOff.mp3");
                winPath    = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_win.mp3");
                losePath   = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_lose.mp3");

                File.WriteAllBytes(sfxOnPath,  Properties.Resources.radioBtnOnAudio);
                File.WriteAllBytes(sfxOffPath, Properties.Resources.radioBtnOffAudio);
                File.WriteAllBytes(winPath,    Properties.Resources.gameWinAudio);
                File.WriteAllBytes(losePath,   Properties.Resources.gameOver);
            }
            catch { /* ignore */ }

            sfxPlayer.settings.autoStart = false;
            sfxPlayer.settings.volume = 100; // crisp SFX

            // ====== Register & prime UI SFX (each gets its own warmed player) ======
            RegisterUiSfx("attemptsCmd",  Properties.Resources.attemptsCmdAudio, "attemptsCmdAudio.mp3");
            RegisterUiSfx("fontbtnright", Properties.Resources.fontRightAudio,   "fontRightAudio.mp3");
            RegisterUiSfx("fontbtnleft",  Properties.Resources.fontLeftAudio,    "fontLeftAudio.mp3");
            RegisterUiSfx("defaultbtn",   Properties.Resources.defaultBtnAudio,  "defaultBtnAudio.mp3");
            RegisterUiSfx("Gobtn",        Properties.Resources.goBtnAudio,       "goBtnAudio.mp3");
            RegisterUiSfx("gewssBtn",     Properties.Resources.guessBtnAudio,    "guessBtnAudio.mp3");
            RegisterUiSfx("geuessTbx",    Properties.Resources.guessTbxAudio,    "guessTbxAudio.mp3");
            RegisterUiSfx("infoBtns",     Properties.Resources.infoBtnsAudio,    "infoBtnsAudio.mp3");
            RegisterUiSfx("startAttbx",   Properties.Resources.startsAtAudio,     "startsAtAudio.mp3");
            RegisterUiSfx("stopAtTbx",    Properties.Resources.stopsAtAudio,      "stopsAtAudio.mp3");
        }

        private void WireEvents()
        {
            btnPlayZy.Click += btnPlayZy_Click;
            btnGoZy.Click += btnGoZy_Click;
            btnDefaultZy.Click += btnDefaultZy_Click;
            btnFontLeftZy.Click += btnFontLeftZy_Click;
            btnFontRightZy.Click += btnFontRightZy_Click;
            btnGuessZy.Click += btnGuessZy_Click;
            btnClearZy.Click += btnClearZy_Click;
            btnCheatZy.Click += btnCheatZy_Click;
            btnLocateZy.Click += btnLocateZy_Click;
            btnAboutZy.Click += btnAboutZy_Click;

            btnMusicZy.Click += btnMusicZy_Click; // toggle BGM

            // keep minimal SFX delegates (immediate)
            cmbAttemptsZy.SelectedIndexChanged += (s, e) => PlayUiSfx("attemptsCmd");

            tbxGuessZy.Enter      += (s, e) => PlayUiSfx("geuessTbx");
            tbxGuessZy.MouseDown  += (s, e) => { if (e.Button == MouseButtons.Left) PlayUiSfx("geuessTbx"); };

            tbxStartZy.Enter      += (s, e) => PlayUiSfx("startAttbx");
            tbxStartZy.MouseDown  += (s, e) => { if (e.Button == MouseButtons.Left) PlayUiSfx("startAttbx"); };

            tbxStopZy.Enter       += (s, e) => PlayUiSfx("stopAtTbx");
            tbxStopZy.MouseDown   += (s, e) => { if (e.Button == MouseButtons.Left) PlayUiSfx("stopAtTbx"); };
        }

        // ========= Intro =========
        private void btnPlayZy_Click(object sender, EventArgs e)
        {
            pbxIntroZy.Visible = false;
            btnPlayZy.Visible = false;
            btnPlayZy.Enabled = false;

            introMusic.FadeTo(0, durationMs: 1200);

            var killTimer = new Timer { Interval = 1500 };
            killTimer.Tick += (s2, ev2) =>
            {
                killTimer.Stop();
                KillIntroMusic();
            };
            killTimer.Start();
        }

        private void StartIntroVideo()
        {
            KillIntroMusic();

            introVideoPlayer.URL = introVideoPath;
            introVideoPlayer.Ctlcontrols.currentPosition = 0;
            introVideoPlayer.Visible = true;
            introVideoPlayer.BringToFront();
            introVideoPlayer.Ctlcontrols.play();
        }

        private void ShowGameSetup()
        {
            KillIntroMusic();

            grpSetupZy.Visible = true;
            grpSetupZy.Enabled = true;

            grpPlayZy.Visible = true;
            grpPlayZy.Enabled = false; // until Go!

            grpInfoZy.Visible = true;

            this.Text = "Mystery Number - ZY";
            rtbLogZy.AppendText(
                "Welcome to Mystery Number, Detective!\r\n\r\n" +
                "Set START/STOP and Attempts, then press Go!\r\n\r\n" +
                "Good luck!\r\n\r\n");
            tbxStartZy.Focus();

            // schedule BGM (only if user wants, and not already on)
            CancelBgmDelay();
            bgmDelayTimer = new Timer { Interval = 3000 };
            bgmDelayTimer.Tick += (s, ev) =>
            {
                CancelBgmDelay();
                if (bgmDesiredOn && !bgmIsOn)
                {
                    try
                    {
                        bgmMusic.StartLoopFromResource(Properties.Resources.ingameAudio, ".mp3", startVolume: 0);
                        bgmMusic.FadeTo(35, durationMs: 1000);
                        bgmIsOn = true;
                    }
                    catch { }
                }
            };
            bgmDelayTimer.Start();
        }

        // murder the intro loop so it cannot linger
        private void KillIntroMusic()
        {
            if (introMusicKilled) return;
            try { introMusic.FadeTo(0, 0); } catch { }
            try { introMusic.Stop(); } catch { }
            try { introMusic.Dispose(); } catch { }
            introMusicKilled = true;
        }

        // ========= Fonts (left/right) =========
        private void btnFontLeftZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("fontbtnleft");  // immediate SFX
            _currentFontIndexZy--;
            if (_currentFontIndexZy < 0) _currentFontIndexZy = _availableFontsZy.Length - 1;
            ApplyFont(_availableFontsZy[_currentFontIndexZy]);
        }

        private void btnFontRightZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("fontbtnright"); // immediate SFX
            _currentFontIndexZy++;
            if (_currentFontIndexZy >= _availableFontsZy.Length) _currentFontIndexZy = 0;
            ApplyFont(_availableFontsZy[_currentFontIndexZy]);
        }

        private void ApplyFont(string family)
        {
            lblFontNameZy.Text = family;
            try
            {
                var f = new Font(family, 10f, FontStyle.Regular);
                ApplyFontToAll(this, f);         // control.Font for everything
                ApplyFontToAllRichText(family);  // normalize RTF content + keep centered
                this.Text = "Mystery Number - " + f.Name;
            }
            catch
            {
                var fallback = new Font("Segoe UI", 10f, FontStyle.Regular);
                ApplyFontToAll(this, fallback);
                ApplyFontToAllRichText(fallback.Name);
                this.Text = "Mystery Number - " + fallback.Name;
            }
        }

        private void ApplyFontToAll(Control root, Font f)
        {
            foreach (Control c in root.Controls)
            {
                c.Font = f;
                foreach (Control inner in c.Controls) inner.Font = f;
            }
        }

        // Walk the tree: set control.Font as usual AND update the *content* of RichTextBoxes,
        // then re-center them so alignment survives font swaps.
        private void ApplyFontToAllRichText(string familyName)
        {
            void Recurse(Control root)
            {
                foreach (Control c in root.Controls)
                {
                    // keep control.Font consistent
                    c.Font = new Font(familyName, c.Font.Size, c.Font.Style);

                    if (c is RichTextBox rtb)
                    {
                        bool ro = rtb.ReadOnly;
                        int ss = rtb.SelectionStart;
                        int sl = rtb.SelectionLength;

                        try
                        {
                            rtb.ReadOnly = false;
                            rtb.SelectAll();
                            rtb.SelectionFont = new Font(familyName, rtb.Font.Size, rtb.Font.Style);
                        }
                        finally
                        {
                            rtb.Select(ss, sl);
                            rtb.ReadOnly = ro;
                        }

                        CenterRtbText(rtb);
                    }

                    if (c.HasChildren) Recurse(c);
                }
            }
            Recurse(this);
        }

        // Center all text in a RichTextBox without moving the caret
        private static void CenterRtbText(RichTextBox rtb)
        {
            if (rtb == null) return;
            int start = rtb.SelectionStart;
            int len   = rtb.SelectionLength;
            rtb.SelectAll();
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            rtb.Select(start, len);
        }

        // ========= Go! (start game) =========
        private void btnGoZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("Gobtn"); // immediate SFX

            // If a win/lose jingle is playing, fade it away before new round
            FadeOutJingleThen(null);

            if (!int.TryParse(tbxStartZy.Text, out int start) ||
                !int.TryParse(tbxStopZy.Text, out int stop) ||
                cmbAttemptsZy.SelectedItem == null ||
                !int.TryParse(cmbAttemptsZy.SelectedItem.ToString(), out int attempts))
            {
                MessageBox.Show("Please fill all setup fields with numbers.", "Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = _engine.StartGame(start, stop, attempts);
            if (!ok)
            {
                MessageBox.Show("Start must be smaller than Stop, and attempts > 0.", "Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // fresh game state
            CancelBgmDelay();             // clear any old delay from previous screen

            grpSetupZy.Enabled = false;
            grpPlayZy.Enabled = true;

            lblAttemptsLeftZy.Text = $"Attempts left: {_engine.AttemptsLeft}";
            lblAttemptsWrongZy.Text = $"Wrong guesses: {_engine.WrongGuesses}";
            lblResultZy.Text = "";
            pbAttemptsZy.Maximum = _engine.AttemptsTotal;
            pbAttemptsZy.Value = _engine.AttemptsLeft;

            HotColdMeterZy.Update(tbrTempZy, 0);
            HotColdMeterZy.AlignOverlay(tbrTempZy);
            ShowTempZone("ICE");

            rtbLogZy.AppendText($"New number generated between {_engine.StartRange} and {_engine.StopRange}.\r\n" +
                                $"Attempts: {_engine.AttemptsTotal}\r\n\r\n");
            tbxGuessZy.Clear();
            tbxGuessZy.Focus();

            // Start/ensure BGM if user wants it
            if (bgmDesiredOn && !bgmIsOn)
            {
                try
                {
                    bgmMusic.StartLoopFromResource(Properties.Resources.ingameAudio, ".mp3", startVolume: 0);
                    bgmMusic.FadeTo(35, durationMs: 600);
                    bgmIsOn = true;
                }
                catch { }
            }
        }

        // ========= Guess =========
        private void btnGuessZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("gewssBtn"); // immediate SFX

            if (!_engine.GameActive)
            {
                MessageBox.Show("Press Go! first.", "Info");
                return;
            }

            if (!int.TryParse(tbxGuessZy.Text, out int guess))
            {
                MessageBox.Show("Enter a number.", "Input");
                tbxGuessZy.Focus();
                return;
            }

            var result = _engine.MakeGuess(guess);
            if (!result.InRange)
            {
                MessageBox.Show($"Stay in range {_engine.StartRange} - {_engine.StopRange}.", "Range");
                tbxGuessZy.SelectAll();
                tbxGuessZy.Focus();
                return;
            }

            int heat = Math.Max(0, Math.Min(100, result.HeatPercent));
            HotColdMeterZy.Update(tbrTempZy, heat);
            string zone = HotColdMeterZy.ZoneFromPercent(heat);
            ShowTempZone(zone);
            lblResultZy.Text = zone;

            // update labels/progress based on engine state
            lblAttemptsLeftZy.Text = $"Attempts left: {_engine.AttemptsLeft}";
            lblAttemptsWrongZy.Text = $"Wrong guesses: {_engine.WrongGuesses}";
            pbAttemptsZy.Value = Math.Max(0, Math.Min(_engine.AttemptsLeft, pbAttemptsZy.Maximum));

            if (result.Correct)
            {
                // Fade out BGM and play WIN jingle
                FadeOutBgmThen(() => StartJingle(winPath));
                rtbLogZy.AppendText($"Correct! The number was {_engine.TargetNumber}.\r\n\r\n");
                MessageBox.Show("Congratulations Detective! You solved the mystery!", "Case Closed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grpPlayZy.Enabled = false;
                grpSetupZy.Enabled = true;
            }
            else
            {
                rtbLogZy.AppendText($"Guess {guess} => {zone}! {result.Hint}\r\n\r\n");
            }

            if (_engine.AttemptsLeft == 0 && !result.Correct)
            {
                // Out of tries: fade BGM and play LOSE jingle
                FadeOutBgmThen(() => StartJingle(losePath));
                MessageBox.Show($"No attempts left!\nThe correct number was {_engine.TargetNumber}.", "Game Over",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtbLogZy.AppendText($"Game Over! The secret number was {_engine.TargetNumber}.\r\n\r\n");
                grpPlayZy.Enabled = false;
                grpSetupZy.Enabled = true;
            }

            tbxGuessZy.Clear();
            tbxGuessZy.Focus();
        }

        // ======= MUSIC HELPERS =======
        private void FadeOutBgmThen(Action after)
        {
            // cancel delayed BGM to avoid “come back” during jingle
            CancelBgmDelay();

            if (bgmIsOn)
            {
                void handler(object s, int v)
                {
                    bgmMusic.FadeCompleted -= handler;
                    try { bgmMusic.Stop(); } catch { }
                    bgmIsOn = false;
                    after?.Invoke();
                }
                bgmMusic.FadeCompleted += handler;
                try { bgmMusic.FadeTo(0, 500); } catch { after?.Invoke(); }
            }
            else
            {
                after?.Invoke();
            }
        }

        private void CancelBgmDelay()
        {
            try { bgmDelayTimer?.Stop(); } catch { }
            bgmDelayTimer = null;
        }

        // ========= Music toggle (BGM only) =========
        private void btnMusicZy_Click(object sender, EventArgs e)
        {
            bgmDesiredOn = !bgmDesiredOn;

            if (!bgmDesiredOn)
            {
                // Turn OFF BGM: play off SFX while fading out
                PlayOneShot(sfxOffPath);
                CancelBgmDelay();

                if (bgmIsOn)
                {
                    bgmMusic.FadeCompleted -= OnBgmFadeCompletedStop; // de-dup
                    bgmMusic.FadeCompleted += OnBgmFadeCompletedStop;
                    bgmMusic.FadeTo(0, 700);
                }
            }
            else
            {
                // Turn ON BGM: play on SFX then start/fade in
                PlayOneShot(sfxOnPath);

                if (!bgmIsOn)
                {
                    try
                    {
                        bgmMusic.StartLoopFromResource(Properties.Resources.ingameAudio, ".mp3", startVolume: 0);
                        bgmMusic.FadeTo(35, 700);
                        bgmIsOn = true;
                    }
                    catch { }
                }
                else
                {
                    bgmMusic.FadeTo(35, 400);
                }
            }
        }

        private void OnBgmFadeCompletedStop(object sender, int finalVol)
        {
            if (finalVol == 0)
            {
                try { bgmMusic.Stop(); } catch { }
                bgmIsOn = false;
            }
            bgmMusic.FadeCompleted -= OnBgmFadeCompletedStop;
        }

        // ======= JINGLE (win/lose) helpers: start + fade-out on Go =======
        private void StartJingle(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            try
            {
                // Stop any previous fade timer
                sfxFadeTimer?.Stop();
                sfxFadeTimer = null;

                sfxPlayer.controls.stop();
                sfxPlayer.URL = path;
                sfxPlayer.settings.volume = 100;
                sfxPlayer.controls.play();
                jingleActive = true;
            }
            catch { }
        }

        private void FadeOutJingleThen(Action after)
        {
            if (!jingleActive)
            {
                after?.Invoke();
                return;
            }

            try
            {
                sfxFadeTimer?.Stop();
                sfxFadeTimer = new Timer { Interval = 50 }; // smooth-ish
                int v = sfxPlayer.settings.volume;          // start from current
                sfxFadeTimer.Tick += (s, ev) =>
                {
                    v -= 10;
                    if (v <= 0)
                    {
                        v = 0;
                        sfxFadeTimer.Stop();
                        try { sfxPlayer.controls.stop(); } catch { }
                        jingleActive = false;
                        after?.Invoke();
                    }
                    sfxPlayer.settings.volume = v;
                };
                sfxFadeTimer.Start();
            }
            catch
            {
                // if anything goes weird, just stop
                try { sfxPlayer.controls.stop(); } catch { }
                jingleActive = false;
                after?.Invoke();
            }
        }

        private void PlayOneShot(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            try
            {
                // Use same WMP, but mark as not a jingle (don’t fade on Go)
                sfxFadeTimer?.Stop();
                sfxPlayer.controls.stop();
                sfxPlayer.URL = path;
                sfxPlayer.settings.volume = 100;
                sfxPlayer.controls.play();
                // leave jingleActive unchanged for SFX
            }
            catch { }
        }

        // ======= UI SFX (preloaded & primed) =======
        private void RegisterUiSfx(string key, byte[] resourceBytes, string fileName)
        {
            try
            {
                string path = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_" + fileName);
                File.WriteAllBytes(path, resourceBytes);

                var w = new WindowsMediaPlayer();
                w.settings.volume = 0;                 // mute while priming
                w.settings.autoStart = true;           // allow our one-time warm play
                w.URL = path;

                var entry = new UiSfxEntry { Player = w, Primed = false, Path = path };

                // Prime once: when it first hits Playing, pause immediately and unmute for future
                w.PlayStateChange += (int newState) =>
                {
                    try
                    {
                        if (!entry.Primed && (WMPPlayState)newState == WMPPlayState.wmppsPlaying)
                        {
                            w.controls.pause();       // keep it loaded/buffered
                            w.settings.volume = 100;  // restore volume for real plays
                            w.settings.autoStart = false;
                            entry.Primed = true;
                        }
                    }
                    catch { /* ignore */ }
                };

                uiSfx[key] = entry;
            }
            catch
            {
                // If any single SFX fails to register, we just skip it (silent for that control)
            }
        }

        private void PlayUiSfx(string key)
        {
            try
            {
                if (uiSfx.TryGetValue(key, out var entry) && entry?.Player != null)
                {
                    var p = entry.Player;
                    // ensure primed; if not yet, let WMP handle startup — we’ll be primed after this
                    if (entry.Primed)
                    {
                        p.controls.stop();                // reset state
                        p.controls.currentPosition = 0;   // go to beginning
                    }
                    p.settings.volume = 100;
                    p.controls.play();
                }
            }
            catch
            {
                // never crash on UI click sfx
            }
        }

        // ========= Info buttons =========
        private void btnClearZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("infoBtns"); // immediate SFX

            if (_engine.GameActive)
            {
                var resp = MessageBox.Show("A game is running. Stop and clear?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.No) return;
            }

            _engine.Reset();

            tbxStartZy.Clear();
            tbxStopZy.Clear();
            tbxGuessZy.Clear();
            cmbAttemptsZy.SelectedIndex = -1;

            lblResultZy.Text = "";
            lblAttemptsLeftZy.Text = "Attempts left: 0";
            lblAttemptsWrongZy.Text = "Wrong guesses: 0";
            pbAttemptsZy.Value = 0;

            HotColdMeterZy.Update(tbrTempZy, 0);
            HotColdMeterZy.AlignOverlay(tbrTempZy);
            ShowTempZone("ICE");

            grpSetupZy.Enabled = true;
            grpPlayZy.Enabled = false;

            // audio state: stop bgm and any jingle
            CancelBgmDelay();
            if (bgmIsOn) { try { bgmMusic.FadeTo(0, 300); bgmMusic.Stop(); } catch { } bgmIsOn = false; }
            FadeOutJingleThen(null);

            rtbLogZy.Clear();
            rtbLogZy.AppendText(
                "Welcome to Mystery Number, Detective!\r\n\r\n" +
                "Set START/STOP and Attempts, then press Go!\r\n\r\n" +
                "Good luck!\r\n\r\n");
        }

        private void btnCheatZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("infoBtns"); // immediate SFX

            if (!_engine.GameActive)
            {
                MessageBox.Show("Start a game first.", "Cheat");
                return;
            }
            _engine.MarkCheat();
            MessageBox.Show($"(CHEAT) Secret = {_engine.TargetNumber}");
            rtbLogZy.AppendText($"(CHEAT) Secret = {_engine.TargetNumber}\r\n\r\n");
        }

        private void btnLocateZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("infoBtns"); // immediate SFX

            try
            {
                string folderPath = Application.StartupPath;
                Process.Start("explorer.exe", folderPath);
                string readme = Path.Combine(folderPath, "ReadMe.txt");
                if (File.Exists(readme))
                    Process.Start(new ProcessStartInfo(readme) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show("Unable to open folder or file.", "Locate Error");
            }
        }

        private void btnAboutZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("infoBtns"); // immediate SFX

            string about =
                "Mystery Number - Detective Conan Style\r\n" +
                "Version 4.3\r\n\r\n" +
                "Developer: Zy (Abdullah Zenaty)\r\n\r\n" +
                "Guess the secret number. Hints go from ICE to HOT.";
            MessageBox.Show(about, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========= Helpers for pictures =========
        private void ShowTempZone(string zone)
        {
            if (pbxIceZy != null)  pbxIceZy.BackgroundImage  = Properties.Resources.iceColdTempNC;
            if (pbxColdZy != null) pbxColdZy.BackgroundImage = Properties.Resources.coldTempNC;
            if (pbxWarmZy != null) pbxWarmZy.BackgroundImage = Properties.Resources.warmTempNC;
            if (pbxHotZy != null)  pbxHotZy.BackgroundImage  = Properties.Resources.hotTempNC;

            if (zone == "ICE"  && pbxIceZy  != null) pbxIceZy.BackgroundImage  = Properties.Resources.iceColdTempWC;
            if (zone == "COLD" && pbxColdZy != null) pbxColdZy.BackgroundImage = Properties.Resources.coldTempWC;
            if (zone == "WARM" && pbxWarmZy != null) pbxWarmZy.BackgroundImage = Properties.Resources.warmTempWC;
            if (zone == "HOT"  && pbxHotZy  != null) pbxHotZy.BackgroundImage  = Properties.Resources.hotTempWC;
        }

        // ========= Defaults =========
        private void btnDefaultZy_Click(object sender, EventArgs e)
        {
            PlayUiSfx("defaultbtn"); // immediate SFX

            tbxStartZy.Text = "1";
            tbxStopZy.Text = "100";
            cmbAttemptsZy.SelectedItem = "10";

            lblResultZy.Text = "";
            HotColdMeterZy.Update(tbrTempZy, 0);
            HotColdMeterZy.AlignOverlay(tbrTempZy);
            pbAttemptsZy.Value = 0;

            ShowTempZone("ICE");

            _engine.ResetCountersOnly(DefaultAttemptsZy);
            lblAttemptsLeftZy.Text = $"Attempts left: {_engine.AttemptsLeft}";
            lblAttemptsWrongZy.Text = "Wrong guesses: 0";
        }

        // ---- Cleanup so WMP releases handles properly ----
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try { KillIntroMusic(); } catch { }

            try { bgmMusic?.Dispose(); } catch { }
            try { bgmDelayTimer?.Stop(); bgmDelayTimer?.Dispose(); } catch { }

            try { sfxFadeTimer?.Stop(); sfxFadeTimer?.Dispose(); } catch { }
            try { sfxPlayer?.controls.stop(); } catch { }
            try { sfxPlayer = null; } catch { }

            // UI SFX cleanup: stop and release every player
            try
            {
                foreach (var kv in uiSfx.Values)
                {
                    try { kv.Player.controls.stop(); } catch { }
                }
                uiSfx.Clear();
            }
            catch { }

            try { introVideoPlayer?.Ctlcontrols.stop(); } catch { }
            try { introVideoPlayer?.close(); } catch { }
            try { introVideoPlayer?.Dispose(); } catch { }

            base.OnFormClosed(e);
        }
    }
}
