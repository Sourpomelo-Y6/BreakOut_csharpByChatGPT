using BreakOut.Model;
using BreakOut.Model.Shapes;
using BreakOut.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Input;

namespace BreakOut.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public ObservableCollection<object> Shapes { get; set; }

        Game game;

        public MainViewModel()
        {
            game = new Game();
            Shapes = new ObservableCollection<object>();
            //test();
            //game.Start();
            Completeflag = false;
        }

        private void test()
        {
            Shapes.Add(new BallViewModel());
            Shapes.Add(new BlockViewModel());
            Shapes.Add(new PaddleViewModel());
        }

        private RelayCommand startCommand;

        public ICommand StartCommand
        {
            get
            {
                if (startCommand == null)
                {
                    startCommand = new RelayCommand(Start);
                }

                return startCommand;
            }
        }

        private void Start()
        {
            if (game.Start())
            {
                Timer timer = new Timer(100);
                timer.Elapsed += OnTimerElapsed;
                timer.Start();

                Shapes.Add(new BallViewModel(game.Ball));
                for (int i = 0; i < game.Blocks.Length; i++)
                {
                    Shapes.Add(new BlockViewModel(game.Blocks[i]));
                }
                Shapes.Add(new PaddleViewModel(game.Paddle));
            }
        }

        private RelayCommand leftCommand;

        public ICommand LeftCommand
        {
            get
            {
                if (leftCommand == null)
                {
                    leftCommand = new RelayCommand(LeftKey);
                }

                return leftCommand;
            }
        }

        private void LeftKey()
        {
            game.MovePaddleLeft();
        }

        private RelayCommand rightCommand;

        public ICommand RightCommand
        {
            get
            {
                if (rightCommand == null)
                {
                    rightCommand = new RelayCommand(RightKey);
                }

                return rightCommand;
            }
        }

        public bool Endflag { get; internal set; }
        public bool Completeflag { get; internal set; }

        private void RightKey()
        {
            game.MovePaddleRight();
        }



        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            game.OnTimerElapsed();
            //OnPropertyChanged("Shapes");

            if (!game.IsOvered() && !Endflag)
            {
                
                App.Current.Dispatcher.Invoke(() =>
                {
                    Shapes.Clear();
                    Shapes.Add(new BallViewModel(game.Ball));
                    for (int i = 0; i < game.Blocks.Length; i++)
                    {
                        Shapes.Add(new BlockViewModel(game.Blocks[i]));
                    }
                    Shapes.Add(new PaddleViewModel(game.Paddle));

                    Completeflag = true;
                });
                
            }

        }
    }
}
