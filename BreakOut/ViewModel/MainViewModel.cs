using BreakOut.Model;
using BreakOut.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Shapes.Add(new BallViewModel());
            Shapes.Add(new BlockViewModel());
            Shapes.Add(new PaddleViewModel());
        }
    }
}
