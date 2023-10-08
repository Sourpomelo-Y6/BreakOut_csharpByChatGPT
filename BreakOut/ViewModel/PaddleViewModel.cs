﻿using System;
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

        private double left;
        public double Left
        {
            get { return left; }
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }

        private double top;
        public double Top
        {
            get { return top; }
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }

        private double width;
        public double Width 
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }


        private double height;
        public double Height 
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }


    }
}
