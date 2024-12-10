using System;
using System.Windows.Threading;
using System.ComponentModel;

namespace SlotsGamble.Models
{
    public class SlotGambleModel : INotifyPropertyChanged
    {
        private int _score;
        public int[] GeneratedNumbers { get; set; }
        public int Level { get; set; }
        public int Speed { get; set; }

        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        public string ResultMessage { get; private set; }

        private const int SIZE = 3;
        private Random _random;
        private DispatcherTimer _timer;

        public SlotGambleModel()
        {
            GeneratedNumbers = new int[SIZE];
            Level = 1;
            Speed = 50;
            Score = 0;

            _random = new Random();
            _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = TimeSpan.FromMilliseconds(Speed);
        }

        public int[] GenerateRandomNumbers()
        {
            for (int i = 0; i < SIZE; i++)
            {
                GeneratedNumbers[i] = _random.Next(1, 8);
            }
            return GeneratedNumbers;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            GenerateRandomNumbers();
            // Můžete volitelně přidat logiku pro zobrazení čísel na UI
        }

        public void StartSlotMachine()
        {
            _timer.Start();
        }

        public void StopSlotMachine()
        {
            _timer.Stop();
            ScoreUpdate();
        }

        public void ResetSpeed()
        {
            Speed = 500;
        }

        public void FastenUp()
        {
            Speed -= Speed / 33;
        }

        public bool isTooFast()
        {
            return Speed <= 10;
        }

        public void LevelDecrement()
        {
            Level = (Level > 1) ? Level - 1 : Level;
        }

        public void ScoreUpdate()
        {
            int score = 0;

            if (GeneratedNumbers[0] == GeneratedNumbers[1] && GeneratedNumbers[1] == GeneratedNumbers[2])
            {
                score = (GeneratedNumbers[0] + GeneratedNumbers[1] + GeneratedNumbers[2]) * Level;
                Level++;
                if (isTooFast()) ResetSpeed();
                else FastenUp();
            }
            else if (GeneratedNumbers[0] == GeneratedNumbers[1] || GeneratedNumbers[0] == GeneratedNumbers[2] || GeneratedNumbers[1] == GeneratedNumbers[2])
            {
                score = (GeneratedNumbers[0] == GeneratedNumbers[1]) ? GeneratedNumbers[0] + GeneratedNumbers[1] :
                        (GeneratedNumbers[0] == GeneratedNumbers[2]) ? GeneratedNumbers[0] + GeneratedNumbers[2] :
                        (GeneratedNumbers[1] == GeneratedNumbers[2]) ? GeneratedNumbers[1] + GeneratedNumbers[2] : 0;
                Level++;
                if (isTooFast()) ResetSpeed();
                else FastenUp();
            }
            else
            {
                score -= (GeneratedNumbers[0] * GeneratedNumbers[1] * GeneratedNumbers[2]) * 2;
                LevelDecrement();
            }
            Score += score;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
