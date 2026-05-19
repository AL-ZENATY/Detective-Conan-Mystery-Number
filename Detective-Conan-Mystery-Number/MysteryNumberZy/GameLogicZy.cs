using System;

namespace MysteryNumberZy
{
    public class GuessResultZy
    {
        public bool InRange;        // guess inside [Start..Stop]
        public bool Correct;        // guess == Target
        public int HeatPercent;     // 0..100 (higher = closer)
        public string Zone;         // "ICE" | "COLD" | "WARM" | "HOT"
        public string Hint;         // small text line for the log
    }

    public class GameLogicZy
    {
        private readonly Random _rnd = new Random();

        // Public state Form1 reads
        public bool GameActive { get; private set; }
        public int StartRange { get; private set; }
        public int StopRange { get; private set; }
        public int TargetNumber { get; private set; }
        public int AttemptsTotal { get; private set; }
        public int AttemptsLeft { get; private set; }
        public int WrongGuesses { get; private set; }
        public bool UsedCheat { get; private set; }

        // Simple hint texts (keep short)
        private readonly string[] _iceHints = {
            "ICE cold… nowhere near.",
            "Frozen solid.",
            "Arctic distance."
        };
        private readonly string[] _coldHints = {
            "Still cold.",
            "Far but moving.",
            "Cold breeze."
        };
        private readonly string[] _warmHints = {
            "Getting warm.",
            "On the path.",
            "Warmer!"
        };
        private readonly string[] _hotHints = {
            "HOT! Very close.",
            "Almost there.",
            "Burning hot!"
        };

        public GameLogicZy()
        {
            Reset();
        }

        // Form1 calls this at Clear
        public void Reset()
        {
            GameActive = false;
            StartRange = 0;
            StopRange = 0;
            TargetNumber = 0;
            AttemptsTotal = 0;
            AttemptsLeft = 0;
            WrongGuesses = 0;
            UsedCheat = false;
        }

        // Form1 uses this in Default button
        public void ResetCountersOnly(int attempts)
        {
            AttemptsTotal = attempts;
            AttemptsLeft = attempts;
            WrongGuesses = 0;
        }

        // Form1 uses this in Go button
        public bool StartGame(int start, int stop, int attempts)
        {
            if (attempts <= 0) return false;
            if (start >= stop) return false;

            StartRange = start;
            StopRange = stop;
            AttemptsTotal = attempts;
            AttemptsLeft = attempts;
            WrongGuesses = 0;
            UsedCheat = false;

            TargetNumber = _rnd.Next(start, stop + 1);
            GameActive = true;
            return true;
        }

        // Form1 uses this in Cheat button
        public void MarkCheat()
        {
            UsedCheat = true;
        }

        // Form1 uses this in Guess button
        public GuessResultZy MakeGuess(int guess)
        {
            var r = new GuessResultZy();

            if (!GameActive)
            {
                r.InRange = false;
                r.Correct = false;
                r.HeatPercent = 0;
                r.Zone = "ICE";
                r.Hint = "";
                return r;
            }

            // Range check
            if (guess < StartRange || guess > StopRange)
            {
                r.InRange = false;
                r.Correct = false;
                r.HeatPercent = 0;
                r.Zone = "ICE";
                r.Hint = "";
                return r;
            }

            r.InRange = true;

            // Correct?
            if (guess == TargetNumber)
            {
                r.Correct = true;
                r.HeatPercent = 100;
                r.Zone = "HOT";
                r.Hint = "Solved!";
                GameActive = false;   // round ends
                return r;
            }

            // Not correct → update counters
            r.Correct = false;
            WrongGuesses++;
            AttemptsLeft = Math.Max(0, AttemptsLeft - 1);

            // Heat calculation
            int maxDist = Math.Max(TargetNumber - StartRange, StopRange - TargetNumber);
            if (maxDist <= 0) maxDist = Math.Max(1, StopRange - StartRange);

            int dist = Math.Abs(guess - TargetNumber);
            int heat = 100 - (dist * 100 / maxDist);
            if (heat < 0) heat = 0;
            if (heat > 100) heat = 100;

            r.HeatPercent = heat;

            // Zone + hint
            if (heat >= 75) { r.Zone = "HOT"; r.Hint = _hotHints[_rnd.Next(_hotHints.Length)]; }
            else if (heat >= 50) { r.Zone = "WARM"; r.Hint = _warmHints[_rnd.Next(_warmHints.Length)]; }
            else if (heat >= 25) { r.Zone = "COLD"; r.Hint = _coldHints[_rnd.Next(_coldHints.Length)]; }
            else { r.Zone = "ICE"; r.Hint = _iceHints[_rnd.Next(_iceHints.Length)]; }

            // Out of attempts ends the round
            if (AttemptsLeft == 0) GameActive = false;

            return r;
        }
    }
}
