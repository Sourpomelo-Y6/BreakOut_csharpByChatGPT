﻿using BreakOut.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.ViewModel
{
    class BlockViewModel : ObservableObject
    {
        public BlockViewModel()
        {
            this.block = null;
            Width = 100;
            Height = 50;
            Left = 200;
            Top = 200;
        }

        private Block block;

        public BlockViewModel(Block block)
        {
            this.block = block;
        }

        private double left;
        public double Left
        {
            get { return (block != null) ? block.X : left; }
            set
            {
                left = value;
                if (block != null)
                {
                    block.X = value;
                }
                OnPropertyChanged("Left");
            }
        }

        private double top;
        public double Top
        {
            get { return (block != null) ? block.Y : top; }
            set
            {
                top = value;
                if (block != null)
                {
                    block.Y = value;
                }
                OnPropertyChanged("Top");
            }
        }

        private double width;
        public double Width
        {
            get { return (block != null) ? block.Width : width; }
            set
            {
                width = value;
                if (block != null)
                {
                    block.Width = value;
                }
                OnPropertyChanged("Width");
            }
        }


        private double height;
        public double Height
        {
            get { return (block != null) ? block.Height : height; }
            set
            {
                height = value;
                if (block != null)
                {
                    block.Height = value;
                }
                OnPropertyChanged("Height");
            }
        }
    }
}
