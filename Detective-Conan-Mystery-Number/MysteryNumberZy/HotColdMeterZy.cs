using System;
using System.Drawing;
using System.Windows.Forms;

namespace MysteryNumberZy
{
    public static class HotColdMeterZy
    {
        // ------------- Public helpers you already used -------------
        public static int ClampPercent(int percent)
        {
            if (percent < 0) return 0;
            if (percent > 100) return 100;
            return percent;
        }

        // 0–25 ICE, 25–50 COLD, 50–75 WARM, 75–100 HOT
        public static string ZoneFromPercent(int percent)
        {
            percent = ClampPercent(percent);
            if (percent < 25) return "ICE";
            if (percent < 50) return "COLD";
            if (percent < 75) return "WARM";
            return "HOT";
        }

        // ------------- NEW: meter setup + overlay pointer (▲) -------------

        /// <summary>
        /// Make the given TrackBar a slim HORIZONTAL meter (keeps width),
        /// add an up-pointing triangle overlay above it, and wire up auto-repaint.
        /// </summary>
        /// <param name="bar">Your TrackBar (already placed in the designer)</param>
        /// <param name="barHeight">Height in pixels for a slim look (8..16 is nice)</param>
        /// <param name="overlayHeight">Height of the overlay area that draws the ▲</param>
        public static void SetupHorizontalSlimMeter(TrackBar bar, int barHeight = 12, int overlayHeight = 16)
        {
            if (bar == null) return;

            bar.AutoSize = false;     // allow custom height
            bar.Minimum = 0;
            bar.Maximum = 100;
            bar.Value = 0;
            bar.TickStyle = TickStyle.None;
            bar.Enabled = false;     // visual meter only
            bar.Height = Math.Max(6, barHeight);

            // Create or reuse overlay attached to this bar
            UpArrowOverlayZy overlay = GetOrAttachOverlay(bar, overlayHeight);

            // Keep overlay aligned as layout changes
            // (Idempotent to attach multiple anonymous handlers; OK for this scenario)
            void align(object s, EventArgs e) => overlay.AlignToTrack();
            bar.LocationChanged += align;
            bar.SizeChanged += align;

            // Try to hook form-level layout too (parent might be a groupbox/panel)
            if (bar.FindForm() is Form form)
            {
                form.Resize += align;
                form.Layout += align;
            }

            overlay.AlignToTrack();
            overlay.Invalidate();
        }

        /// <summary>
        /// Set meter to <paramref name="percent"/> (0..100), overlay repaints automatically.
        /// </summary>
        public static void Update(TrackBar bar, int percent)
        {
            if (bar == null) return;
            int heat = ClampPercent(percent);
            if (heat < bar.Minimum) heat = bar.Minimum;
            if (heat > bar.Maximum) heat = bar.Maximum;

            // Only set when changed to avoid extra invalidates
            if (bar.Value != heat)
                bar.Value = heat;

            // Nudge overlay just in case
            var overlay = FindOverlay(bar);
            overlay?.Invalidate();
        }

        /// <summary>
        /// Manually realign overlay to the bar (useful after big layout changes or resets).
        /// </summary>
        public static void AlignOverlay(TrackBar bar)
        {
            FindOverlay(bar)?.AlignToTrack();
        }

        // ------------- Internal: attach/find overlay -------------

        private const string overlayTagKey = "HotColdMeterZyOverlay";

        private static UpArrowOverlayZy GetOrAttachOverlay(TrackBar bar, int overlayHeight)
        {
            var existing = FindOverlay(bar);
            if (existing != null)
            {
                if (overlayHeight > 0) existing.Height = overlayHeight;
                return existing;
            }

            var ov = new UpArrowOverlayZy(bar)
            {
                Height = Math.Max(12, overlayHeight),
                BackColor = Color.Transparent
            };

            // Stash a reference so we can find it later
            bar.Tag = new MeterTagWrapper
            {
                Overlay = ov,
                PreviousTag = bar.Tag   // preserve any existing Tag content
            };

            ov.AlignToTrack();
            ov.BringToFront();
            return ov;
        }

        private static UpArrowOverlayZy FindOverlay(TrackBar bar)
        {
            if (bar?.Tag is MeterTagWrapper wrap)
                return wrap.Overlay;
            return null;
        }

        private sealed class MeterTagWrapper
        {
            public object PreviousTag { get; set; }
            public UpArrowOverlayZy Overlay { get; set; }
        }

        // ------------- Nested overlay control (▲) -------------

        private sealed class UpArrowOverlayZy : Control
        {
            private readonly TrackBar track;
            private const int sidePadding = 8;   // keep arrow away from end caps

            public UpArrowOverlayZy(TrackBar target)
            {
                track = target ?? throw new ArgumentNullException(nameof(target));

                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.SupportsTransparentBackColor, true);

                BackColor = Color.Transparent;
                TabStop = false;

                // repaint when value changes
                track.ValueChanged += (s, e) => Invalidate();
            }

            public void AlignToTrack()
            {
                if (track == null || track.Parent == null) return;

                if (Parent != track.Parent)
                {
                    track.Parent.Controls.Add(this);
                    BringToFront();
                }

                Width = track.Width;
                if (Height < 16) Height = 16;

                Left = track.Left;
                Top = track.Top - Height + 2;

                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (track == null) return;

                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                double span = Math.Max(1, track.Maximum - track.Minimum);
                double pct = (track.Value - track.Minimum) / span;

                int left = sidePadding;
                int right = Width - sidePadding;
                int x = left + (int)Math.Round((right - left) * pct);

                int tipY = Height - 2;     // tip close to bar
                int baseY = tipY - 10;     // arrow height
                int halfBase = 6;          // arrow half width

                Point p1 = new Point(x, tipY);                 // tip
                Point p2 = new Point(x - halfBase, baseY);     // base left
                Point p3 = new Point(x + halfBase, baseY);     // base right

                using (var brush = new SolidBrush(Color.White))
                    g.FillPolygon(brush, new[] { p1, p2, p3 });

                using (var pen = new Pen(Color.Gray))
                    g.DrawPolygon(pen, new[] { p1, p2, p3 });
            }
        }
    }
}
