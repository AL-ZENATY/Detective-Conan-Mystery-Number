using System;
using System.Drawing;
using System.Windows.Forms;

namespace MysteryNumberZy
{
    public static class TransparentHelperZy
    {
        // =========================================================
        // === BUTTONS (btnFontLeftZy, btnFontRightZy, btnDefaultZy,
        // ===           btnGoZy, btnGuessZy, btnCheatZy, btnClearZy,
        // ===           btnLocateZy, btnAboutZy, btnPlayZy)
        // =========================================================
        public static void AttachHover(Button btn, Image normalImage, Image hoverImage)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.BackColor = Color.Transparent;
            btn.UseVisualStyleBackColor = false;

            btn.BackgroundImage = normalImage;
            btn.BackgroundImageLayout = ImageLayout.Stretch;

            btn.MouseEnter += (s, e) => { if (hoverImage != null) btn.BackgroundImage = hoverImage; };
            btn.MouseLeave += (s, e) => { if (normalImage != null) btn.BackgroundImage = normalImage; };
        }

        // =========================================================
        // === RADIO BUTTON (target control: any RadioButton)
        // =========================================================
        public static void AttachHover(RadioButton rb, Image normalImage, Image hoverImage)
        {
            if (rb == null) return;

            rb.Appearance = Appearance.Button;
            rb.FlatStyle = FlatStyle.Flat;
            rb.FlatAppearance.BorderSize = 0;
            rb.FlatAppearance.MouseOverBackColor = Color.Transparent;
            rb.FlatAppearance.MouseDownBackColor = Color.Transparent;
            rb.BackColor = Color.Transparent;
            rb.UseVisualStyleBackColor = false;

            rb.BackgroundImage = normalImage;
            rb.BackgroundImageLayout = ImageLayout.Stretch;

            rb.MouseEnter += (s, e) => { if (!rb.Checked && hoverImage != null) rb.BackgroundImage = hoverImage; };
            rb.MouseLeave += (s, e) => { if (!rb.Checked && normalImage != null) rb.BackgroundImage = normalImage; };

            rb.CheckedChanged += (s, e) =>
            {
                if (rb.Checked && hoverImage != null) rb.BackgroundImage = hoverImage;
                else if (normalImage != null) rb.BackgroundImage = normalImage;
            };
        }

        // =========================================================
        // === RICH TEXT BOX (e.g., tbxGuessZy)
        // === Transparent + PNG hover + centered text + digits-only max 6
        // =========================================================
        public static RichTextBox AttachHover(RichTextBox rtb, Image normalImage, Image hoverImage)
        {
            if (rtb == null || rtb.Parent == null) return rtb;

            // make sure it's the transparent subclass
            rtb = MakeTransparent(rtb);

            var parent = rtb.Parent;
            int z = parent.Controls.GetChildIndex(rtb);

            // text vertical offset via skin padding
            int leftRight = 10;
            int topOffset = 24;  // increase to move text lower (e.g., 28/32)
            int bottom = 8;

            var skin = new Panel
            {
                Name = rtb.Name + "_Skin",
                Location = rtb.Location,
                Size = rtb.Size,
                Anchor = rtb.Anchor,
                Dock = rtb.Dock,
                BackColor = Color.Transparent,
                BackgroundImage = normalImage,
                BackgroundImageLayout = ImageLayout.Stretch,
                Padding = new Padding(leftRight, topOffset, leftRight, bottom)
            };

            parent.Controls.Add(skin);
            parent.Controls.SetChildIndex(skin, z);

            // put the RTB inside the skin
            rtb.Parent = skin;
            rtb.Dock = DockStyle.Fill;
            rtb.BorderStyle = BorderStyle.None;
            rtb.BackColor = Color.Transparent;
            rtb.Multiline = false;
            rtb.ScrollBars = RichTextBoxScrollBars.None;
            rtb.DetectUrls = false;
            rtb.ShortcutsEnabled = true;

            // hover swap on the SKIN
            void ToHover(object s, EventArgs e) { if (hoverImage != null) { skin.BackgroundImage = hoverImage; skin.Invalidate(); } }
            void ToNormal(object s, EventArgs e) { if (normalImage != null) { skin.BackgroundImage = normalImage; skin.Invalidate(); } }
            skin.MouseEnter += ToHover; skin.MouseLeave += ToNormal;
            rtb.MouseEnter += ToHover; rtb.MouseLeave += ToNormal;

            // center text horizontally
            void CenterText()
            {
                int keep = rtb.SelectionStart;
                int len = rtb.SelectionLength;
                rtb.SelectAll();
                rtb.SelectionAlignment = HorizontalAlignment.Center;
                rtb.Select(keep, len);
            }

            // digits-only + max 6, keep caret; then re-center
            rtb.TextChanged += (s, e) =>
            {
                string t = rtb.Text;
                char[] buf = new char[Math.Min(6, t.Length)];
                int j = 0;
                for (int i = 0; i < t.Length && j < 6; i++)
                {
                    char ch = t[i];
                    if (ch >= '0' && ch <= '9') buf[j++] = ch;
                }
                string cleaned = new string(buf, 0, j);
                if (cleaned != t)
                {
                    int caret = Math.Min(cleaned.Length, rtb.SelectionStart);
                    rtb.Text = cleaned;
                    rtb.SelectionStart = caret;
                }
                CenterText();
            };

            // block Enter and non-digits at keypress
            rtb.KeyPress += (s, e) =>
            {
                if (e.KeyChar == '\r' || e.KeyChar == '\n') { e.Handled = true; return; }
                if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9')) e.Handled = true;
            };

            rtb.SizeChanged += (s, e) => CenterText();
            rtb.GotFocus += (s, e) => CenterText();
            CenterText();

            return rtb;
        }

        // =========================================================
        // === RICH TEXT LOG (e.g., rtbLogZy)
        // === Transparent + optional PNG background (+ optional hover)
        // =========================================================
        public static RichTextBox AttachBackground(RichTextBox rtb, Image normalImage, Image hoverImage = null, Padding? pad = null)
        {
            if (rtb == null || rtb.Parent == null) return rtb;

            rtb = MakeTransparent(rtb);

            var parent = rtb.Parent;
            int z = parent.Controls.GetChildIndex(rtb);
            var padding = pad ?? new Padding(16, 16, 16, 16);

            var skin = new Panel
            {
                Name = rtb.Name + "_Skin",
                Location = rtb.Location,
                Size = rtb.Size,
                Anchor = rtb.Anchor,
                Dock = rtb.Dock,
                BackColor = Color.Transparent,
                BackgroundImage = normalImage,                 // can be null for full transparency
                BackgroundImageLayout = ImageLayout.Stretch,
                Padding = padding
            };

            parent.Controls.Add(skin);
            parent.Controls.SetChildIndex(skin, z);

            rtb.Parent = skin;
            rtb.Dock = DockStyle.Fill;
            rtb.BorderStyle = BorderStyle.None;
            rtb.BackColor = Color.Transparent;
            rtb.ReadOnly = true;
            rtb.Multiline = true;
            rtb.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtb.DetectUrls = false;
            rtb.WordWrap = true;

            if (hoverImage != null)
            {
                void ToHover(object s, EventArgs e) { skin.BackgroundImage = hoverImage; skin.Invalidate(); }
                void ToNormal(object s, EventArgs e) { skin.BackgroundImage = normalImage; skin.Invalidate(); }
                skin.MouseEnter += ToHover; skin.MouseLeave += ToNormal;
                rtb.MouseEnter += ToHover; rtb.MouseLeave += ToNormal;
            }

            return rtb;
        }

        // Convenience overload: fully transparent (no image), padding only
        public static RichTextBox AttachBackground(RichTextBox rtb, Padding pad)
            => AttachBackground(rtb, normalImage: null, hoverImage: null, pad: pad);

        // =========================================================
        // === Transparent subclass swapper for RichTextBox
        // =========================================================
        public static RichTextBox MakeTransparent(RichTextBox oldCtrl)
        {
            if (oldCtrl is TransparentRichInputZy) return oldCtrl;
            if (oldCtrl == null || oldCtrl.Parent == null) return oldCtrl;

            var parent = oldCtrl.Parent;
            int z = parent.Controls.GetChildIndex(oldCtrl);

            var r = new TransparentRichInputZy
            {
                Name = oldCtrl.Name,
                Text = oldCtrl.Text,
                Location = oldCtrl.Location,
                Size = oldCtrl.Size,
                Anchor = oldCtrl.Anchor,
                Dock = oldCtrl.Dock,
                TabIndex = oldCtrl.TabIndex,
                Font = oldCtrl.Font,
                ForeColor = oldCtrl.ForeColor
            };

            parent.Controls.Remove(oldCtrl);
            parent.Controls.Add(r);
            parent.Controls.SetChildIndex(r, z);
            oldCtrl.Dispose();
            return r;
        }

        // =========================================================
        // === Transparent rich input control
        // =========================================================
        private class TransparentRichInputZy : RichTextBox
        {
            public TransparentRichInputZy()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint, true);

                BorderStyle = BorderStyle.None;
                BackColor = Color.Transparent;
                Multiline = false;
                ScrollBars = RichTextBoxScrollBars.None;
                DetectUrls = false;
                WordWrap = false;
            }

            protected override CreateParams CreateParams
            {
                get { var cp = base.CreateParams; cp.ExStyle |= 0x20; return cp; } // WS_EX_TRANSPARENT
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // skip fill -> shows parent/skin behind
            }
        }

        // =========================================================
        // === TRACKBAR SKIN (target: tbrTempZy)
        // === Keeps your original TrackBar, hides it, draws a faux slider on a skin panel
        // =========================================================
        public static TrackBar SkinTrackBar(TrackBar real, Image normalImage, Image hoverImage = null, Padding? pad = null)
        {
            if (real == null || real.Parent == null) return real;

            var parent = real.Parent;
            int z = parent.Controls.GetChildIndex(real);

            // Skin panel that owns the PNG (like your buttons)
            var skin = new Panel
            {
                Name = real.Name + "_Skin",
                Location = real.Location,
                Size = real.Size,
                Anchor = real.Anchor,
                Dock = real.Dock,
                BackColor = Color.Transparent,
                BackgroundImage = normalImage,              // can be null if you just want transparent
                BackgroundImageLayout = ImageLayout.Stretch,
                Padding = pad ?? new Padding(10, 10, 10, 10)
            };
            parent.Controls.Add(skin);
            parent.Controls.SetChildIndex(skin, z);

            // Faux slider drawn on top of the skin
            var faux = new FauxTrackBarZy
            {
                Dock = DockStyle.Fill,
                Minimum = real.Minimum,
                Maximum = real.Maximum,
                Value = real.Value
            };
            skin.Controls.Add(faux);

            // Keep the real TrackBar around (for your code), but hide it
            real.Visible = false;

            // Hover swap (like buttons)
            void ToHover(object s, EventArgs e) { if (hoverImage != null) { skin.BackgroundImage = hoverImage; skin.Invalidate(); } }
            void ToNormal(object s, EventArgs e) { if (normalImage != null) { skin.BackgroundImage = normalImage; skin.Invalidate(); } }
            skin.MouseEnter += ToHover; skin.MouseLeave += ToNormal;
            faux.MouseEnter += ToHover; faux.MouseLeave += ToNormal;

            // Sync real -> faux
            real.ValueChanged += (s, e) =>
            {
                if (faux.Value != real.Value)
                    faux.Value = real.Value;
            };

            // Sync faux -> real
            faux.ValueChanged += (s, e) =>
            {
                int v = Math.Max(real.Minimum, Math.Min(real.Maximum, faux.Value));
                if (real.Value != v) real.Value = v;
            };

            return real; // same field/type stays usable everywhere
        }

        // Simple drawn slider control (transparent, mouse-driven)
        private class FauxTrackBarZy : Control
        {
            public event EventHandler ValueChanged;

            private int _minimum = 0;
            public int Minimum { get => _minimum; set { _minimum = value; if (_value < _minimum) _value = _minimum; Invalidate(); } }

            private int _maximum = 100;
            public int Maximum { get => _maximum; set { _maximum = Math.Max(value, _minimum + 1); if (_value > _maximum) _value = _maximum; Invalidate(); } }

            private int _value = 0;
            public int Value
            {
                get => _value;
                set
                {
                    int nv = Math.Max(Minimum, Math.Min(Maximum, value));
                    if (nv != _value)
                    {
                        _value = nv;
                        Invalidate();
                        ValueChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            public FauxTrackBarZy()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw, true);
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // transparent look
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Track rect inside padding
                Rectangle track = ClientRectangle;
                track.Inflate(-10, -(int)Math.Round(track.Height * 0.6) + 6); // thin horizontal track

                // Draw track outline
                using (var p = new Pen(Color.Silver))
                    g.DrawRectangle(p, new Rectangle(track.Left, track.Top, track.Width - 1, track.Height - 1));

                // Percent
                double pct = (Maximum <= Minimum) ? 0.0 : (double)(Value - Minimum) / (Maximum - Minimum);

                // Fill
                int fillW = Math.Max(1, (int)Math.Round((track.Width - 2) * pct));
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, track.Left + 1, track.Top + 1, fillW, track.Height - 2);

                // Thumb
                int thumbX = track.Left + (int)Math.Round((track.Width - 1) * pct);
                var thumb = new Rectangle(thumbX - 6, track.Top - 8, 12, track.Height + 16);
                using (var b = new SolidBrush(Color.White)) g.FillRectangle(b, thumb);
                g.DrawRectangle(Pens.Gray, thumb);
            }

            private bool _dragging;
            protected override void OnMouseDown(MouseEventArgs e)
            {
                _dragging = true;
                SetFromMouse(e.X);
                Capture = true;
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(MouseEventArgs e)
            {
                if (_dragging) SetFromMouse(e.X);
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(MouseEventArgs e)
            {
                _dragging = false;
                Capture = false;
                base.OnMouseUp(e);
            }

            private void SetFromMouse(int mouseX)
            {
                var track = ClientRectangle;
                track.Inflate(-10, -(int)Math.Round(track.Height * 0.6) + 6);

                int x = Math.Max(track.Left, Math.Min(track.Right - 1, mouseX));
                double pct = (double)(x - track.Left) / Math.Max(1, (track.Width - 1));
                Value = Minimum + (int)Math.Round(pct * (Maximum - Minimum));
            }
        }

    }
}
