using System;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace MysteryNumberZy
{
    public sealed class MusicManagerZy : IDisposable
    {
        private WindowsMediaPlayer wmp;
        private Timer fadeTimer;
        private int fadeStep;
        private int targetVolumeInternal;

        public event EventHandler<int> FadeCompleted; // int = final volume (0..100)

        public bool IsPlaying { get; private set; }
        public int Volume
        {
            get => (int)wmp?.settings?.volume;
            set { if (wmp != null) wmp.settings.volume = Math.Max(0, Math.Min(100, value)); }
        }

        public MusicManagerZy()
        {
            wmp = new WindowsMediaPlayer();
            wmp.settings.autoStart = false;
            wmp.settings.setMode("loop", true);
            fadeTimer = new Timer { Interval = 60 };
            fadeTimer.Tick += FadeTick;
        }

        public void StartLoopFromResource(byte[] bytes, string ext = ".mp3", int startVolume = 80)
        {
            if (bytes == null || bytes.Length == 0) return;

            string tmpPath = Path.Combine(Path.GetTempPath(), "MysteryNumberZy_intro" + ext);
            try { File.WriteAllBytes(tmpPath, bytes); } catch { /* ignore */ }

            wmp.URL = tmpPath;
            wmp.settings.volume = Math.Max(0, Math.Min(100, startVolume));
            wmp.controls.play();
            IsPlaying = true;
        }

        public void FadeTo(int target, int durationMs = 1200)
        {
            if (wmp == null) return;
            targetVolumeInternal = Math.Max(0, Math.Min(100, target));
            int current = wmp.settings.volume;
            if (durationMs <= 0 || current == targetVolumeInternal)
            {
                wmp.settings.volume = targetVolumeInternal;
                if (targetVolumeInternal == 0) Stop();
                FadeCompleted?.Invoke(this, targetVolumeInternal);
                return;
            }
            int ticks = Math.Max(1, durationMs / fadeTimer.Interval);
            fadeStep = (int)Math.Ceiling((targetVolumeInternal - current) / (double)ticks);
            if (fadeStep == 0) fadeStep = (targetVolumeInternal > current) ? 1 : -1;
            fadeTimer.Start();
        }

        private void FadeTick(object sender, EventArgs e)
        {
            int v = wmp.settings.volume + fadeStep;
            bool done = (fadeStep < 0 && v <= targetVolumeInternal) || (fadeStep > 0 && v >= targetVolumeInternal);
            if (done)
            {
                wmp.settings.volume = targetVolumeInternal;
                fadeTimer.Stop();
                if (targetVolumeInternal == 0) Stop();
                FadeCompleted?.Invoke(this, targetVolumeInternal);
                return;
            }
            wmp.settings.volume = Math.Max(0, Math.Min(100, v));
        }

        public void Stop()
        {
            if (wmp == null) return;
            try { wmp.controls.stop(); } catch { }
            IsPlaying = false;
        }

        public void Dispose()
        {
            try { fadeTimer?.Stop(); fadeTimer?.Dispose(); } catch { }
            try { wmp?.controls.stop(); } catch { }
            try { wmp?.close(); } catch { }
            wmp = null;
        }
    }
}
