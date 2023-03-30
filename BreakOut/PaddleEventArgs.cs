using System;

namespace BreakOut
{
    public class PaddleEventArgs : EventArgs
    {
        public Paddle Paddle { get; private set; }

        public PaddleEventArgs(Paddle paddle)
        {
            Paddle = paddle;
        }
    }
}