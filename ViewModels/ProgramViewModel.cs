using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SlotsGamble.Models;

namespace SlotsGamble.ViewModels
{
    public class ProgramViewModel : INotifyPropertyChanged
    {
        private SlotGambleModel _slotGambleModel;
        private DispatcherTimer _timer;

        public ICommand StartGame { get; }
        public ICommand StopGame { get; }

        private int _scoreD0;
        public int ScoreD0
        {
            get { return _scoreD0; }
            set
            {
                if (_scoreD0 != value)
                {
                    _scoreD0 = value;
                    OnPropertyChanged(nameof(ScoreD0));
                }
            }
        }

        private int _scoreD1;
        public int ScoreD1
        {
            get { return _scoreD1; }
            set
            {
                if (_scoreD1 != value)
                {
                    _scoreD1 = value;
                    OnPropertyChanged(nameof(ScoreD1));
                }
            }
        }

        private int _scoreD2;
        public int ScoreD2
        {
            get { return _scoreD2; }
            set
            {
                if (_scoreD2 != value)
                {
                    _scoreD2 = value;
                    OnPropertyChanged(nameof(ScoreD2));
                }
            }
        }

        private int _scoreD3;
        public int ScoreD3
        {
            get { return _scoreD3; }
            set
            {
                if (_scoreD3 != value)
                {
                    _scoreD3 = value;
                    OnPropertyChanged(nameof(ScoreD3));
                }
            }
        }

        private int _scoreD4;
        public int ScoreD4
        {
            get { return _scoreD4; }
            set
            {
                if (_scoreD4 != value)
                {
                    _scoreD4 = value;
                    OnPropertyChanged(nameof(ScoreD4));
                }
            }
        }

        private int _generated0;
        public int Generated0
        {
            get { return _generated0; }
            set
            {
                if (_generated0 != value)
                {
                    _generated0 = value;
                    OnPropertyChanged(nameof(Generated0));
                }
            }
        }

        private int _generated1;
        public int Generated1
        {
            get { return _generated1; }
            set
            {
                if (_generated1 != value)
                {
                    _generated1 = value;
                    OnPropertyChanged(nameof(Generated1));
                }
            }
        }

        private int _generated2;
        public int Generated2
        {
            get { return _generated2; }
            set
            {
                if (_generated2 != value)
                {
                    _generated2 = value;
                    OnPropertyChanged(nameof(Generated2));
                }
            }
        }

        private int _speed;
        public int Speed
        {
            get { return _speed; }
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged(nameof(Speed));
                }
            }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged(nameof(Level));
                }
            }
        }

        public ProgramViewModel()
        {
            _slotGambleModel = new SlotGambleModel();
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromMilliseconds(_slotGambleModel.Speed);
            _timer.Tick += OnTimerTick;

            StartGame = new RelayCommand(StartSlotMachine);
            StopGame = new RelayCommand(StopSlotMachine);
            UpdateScore();
            UpdateSlots();
            UpdateSpeed();
            UpdateLevel();
        }

        private void StartSlotMachine()
        {
            _slotGambleModel.StartSlotMachine();
            _timer.Start();
        }

        private void StopSlotMachine()
        {
            _slotGambleModel.StopSlotMachine();
            _timer.Stop();
            UpdateScore();
            UpdateSlots();
            UpdateSpeed();
            UpdateLevel();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _slotGambleModel.GenerateRandomNumbers();
            UpdateSlots();
        }

        private void UpdateScore()
        {
            int score = _slotGambleModel.Score;

            if (score <= 0)
            {
                _slotGambleModel.StopSlotMachine();
                _timer.Stop();
                MessageBox.Show("Došly ti chechtáky! Konec hry.", "Konec hry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ScoreD0 = (score % 10 > 0) ? (score % 10) : 0;
            ScoreD1 = ((score / 10) % 10 > 0) ? ((score / 10) % 10) : 0;
            ScoreD2 = ((score / 100) % 10 > 0) ? ((score / 100) % 10) : 0;
            ScoreD3 = ((score / 1000) % 10 > 0) ? ((score / 1000) % 10) : 0;
            ScoreD4 = ((score / 10000) % 10 > 0) ? ((score / 10000) % 10) : 0;
        }

        private void UpdateSlots()
        {
            Generated0 = _slotGambleModel.GeneratedNumbers[0];
            Generated1 = _slotGambleModel.GeneratedNumbers[1];
            Generated2 = _slotGambleModel.GeneratedNumbers[2];
        }

        private void UpdateSpeed()
        {
            Speed = _slotGambleModel.Speed;
        }

        private void UpdateLevel()
        {
            Level = _slotGambleModel.Level;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
