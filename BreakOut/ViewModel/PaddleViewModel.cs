using BreakOut.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.ViewModel
{
    class PaddleViewModel : ObservableObject
    {
        public PaddleViewModel()
        {
            Width = 100;
            Height = 50;
            Left = 20;
            Top = 20;
        }

        private Paddle paddle;

        public PaddleViewModel(Paddle paddle)
        {
            this.paddle = paddle;
        }

        private double left;
        public double Left
        {
            get { return (paddle != null) ? paddle.X : left; }
            set
            {
                left = value;
                if (paddle != null)
                {
                    paddle.X = value;
                }
                OnPropertyChanged("Left");
            }
        }

        private double top;
        public double Top
        {
            get { return (paddle != null) ? paddle.Y : top; }
            set
            {
                top = value;
                if (paddle != null)
                {
                    paddle.Y = value;
                }
                OnPropertyChanged("Top");
            }
        }

        private double width;
        public double Width
        {
            get { return (paddle != null) ? paddle.Width : width; }
            set
            {
                width = value;
                if (paddle != null)
                {
                    paddle.Width = value;
                }
                OnPropertyChanged("Width");
            }
        }


        private double height;
        public double Height
        {
            get { return (paddle != null) ? paddle.Height : height; }
            set
            {
                height = value;
                if (paddle != null)
                {
                    paddle.Height = value;
                }
                OnPropertyChanged("Height");
            }
        }

        
    }
}
