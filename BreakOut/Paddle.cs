﻿namespace BreakOut
{
    public class Paddle
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        private readonly double speed = 10;

        public Paddle(double x, double y)
        {
            X = x;
            Y = y;
            Width = 80;
            Height = 15;
        }

        public void MoveLeft()
        {
            X -= speed;

            if (X - Width / 2 < 0)
            {
                X = Width / 2;
            }
        }

        public void MoveRight()
        {
            X += speed;

            if (X + Width / 2 > 500)
            {
                X = 500 - Width / 2;
            }
        }
    }

}
