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
                Timer timer = new Timer(16);
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

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            game.OnTimerElapsed();
            //OnPropertyChanged("Shapes");
            App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    Shapes.Clear();
                    Shapes.Add(new BallViewModel(game.Ball));
                    for (int i = 0; i < game.Blocks.Length; i++)
                    {
                        Shapes.Add(new BlockViewModel(game.Blocks[i]));
                    }
                    Shapes.Add(new PaddleViewModel(game.Paddle));
                }
                catch 
                { 
                
                }
            });

        }
    }
}
