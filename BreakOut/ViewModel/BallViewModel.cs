using BreakOut.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.ViewModel
{
    class BallViewModel : ObservableObject
    {

        private Ball ball;

        public BallViewModel()
        {
            this.ball = null;
            Width = 50;
            Height = 50;
            Left = 100;
            Top = 100;
        }

        public BallViewModel(Ball ball)
        {
            this.ball = ball;
        }

        private double left;
        public double Left
        {
            get { return (ball != null) ? ball.X : left; }
            set
            {
                left = value;
                if (ball != null) 
                {
                    ball.X = value;
                }
                OnPropertyChanged("Left");
            }
        }

        private double top;
        public double Top
        {
            get { return (ball != null) ? ball.Y : top; }
            set
            {
                top = value;
                if (ball != null)
                {
                    ball.Y = value;
                }
                OnPropertyChanged("Top");
            }
        }

        private double width;
        public double Width
        {
            get { return (ball != null) ? ball.Width : width; }
            set
            {
                width = value;
                if (ball != null)
                {
                    ball.Width = value;
                }
                OnPropertyChanged("Width");
            }
        }


        private double height;
        public double Height
        {
            get { return (ball != null) ? ball.Height : height; }
            set
            {
                height = value;
                if (ball != null)
                {
                    ball.Height = value;
                }
                OnPropertyChanged("Height");
            }
        }
    }
}
