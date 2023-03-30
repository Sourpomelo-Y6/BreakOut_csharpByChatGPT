using System;
using System.Timers;

namespace BreakOut
{
    public class Game
    {
        public int Score { get; private set; }

        public Ball Ball { get; private set; }

        public Paddle Paddle { get; private set; }

        public Block[] Blocks { get; private set; }

        private bool isStarted = false;

        private bool isPaused = false;

        private readonly double canvasWidth;

        private readonly double canvasHeight;

        public Game(double canvasWidth, double canvasHeight)
        {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;

            Ball = new Ball(canvasWidth / 2, canvasHeight / 2);
            Paddle = new Paddle(canvasWidth / 2, canvasHeight - 50);
            Blocks = new Block[40];

            for (int i = 0; i < 40; i++)
            {
                var x = (i % 8) * 60 + 30;
                var y = (i / 8) * 20 + 30;
                Blocks[i] = new Block(x, y);
            }
        }

        public void Start()
        {
            if (isStarted)
            {
                return;
            }

            isStarted = true;
            isPaused = false;

            Ball.Reset();
            Score = 0;

            Timer timer = new Timer(16);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public void Reset()
        {
            isStarted = false;
            isPaused = false;
            Ball.Reset();
            Score = 0;

            foreach (var block in Blocks)
            {
                block.IsBroken = false;
            }

            BallMoved?.Invoke(this, new BallEventArgs(Ball));
            PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        public void MovePaddleLeft()
        {
            if (!isStarted || isPaused)
            {
                return;
            }

            Paddle.MoveLeft();
            PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        public void MovePaddleRight()
        {
            if (!isStarted || isPaused)
            {
                return;
            }

            Paddle.MoveRight();
            PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (isPaused)
            {
                return;
            }

            Ball.Move();

            if (Ball.X - Ball.Radius <= 0 || Ball.X + Ball.Radius >= canvasWidth)
            {
                Ball.ReverseX();
            }

            if (Ball.Y - Ball.Radius <= 0)
            {
                Ball.ReverseY();
            }

            if (Ball.Y + Ball.Radius >= canvasHeight)
            {
                GameOver?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (IsBallCollidedWithPaddle(Ball, Paddle))
            {
                Ball.ReverseY();
            }

            for (int i = 0; i < Blocks.Length; i++)
            {
                var block = Blocks[i];

                if (!block.IsBroken && IsBallCollidedWithBlock(Ball, block))
                {
                    block.Break();
                    BrickBroken?.Invoke(this, new BlockEventArgs(block));
                    Score += 10;
                    if (IsGameCleared())
                    {
                        GameOver?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }
            }

            BallMoved?.Invoke(this, new BallEventArgs(Ball));
        }

        private bool IsBallCollidedWithPaddle(Ball ball, Paddle paddle)
        {
            double deltaX = Math.Abs(ball.X - paddle.X);
            double deltaY = Math.Abs(ball.Y - paddle.Y);

            if (deltaX > (paddle.Width / 2 + ball.Radius))
            {
                return false;
            }

            if (deltaY > (paddle.Height / 2 + ball.Radius))
            {
                return false;
            }

            if (deltaX <= (paddle.Width / 2))
            {
                return true;
            }

            if (deltaY <= (paddle.Height / 2))
            {
                return true;
            }

            double dx = deltaX - paddle.Width / 2;
            double dy = deltaY - paddle.Height / 2;
            return (dx * dx + dy * dy <= (ball.Radius * ball.Radius));
        }

        private bool IsBallCollidedWithBlock(Ball ball, Block block)
        {
            double deltaX = Math.Abs(ball.X - block.X);
            double deltaY = Math.Abs(ball.Y - block.Y);

            if (deltaX > (block.Width / 2 + ball.Radius))
            {
                return false;
            }

            if (deltaY > (block.Height / 2 + ball.Radius))
            {
                return false;
            }

            if (deltaX <= (block.Width / 2))
            {
                return true;
            }

            if (deltaY <= (block.Height / 2))
            {
                return true;
            }

            double dx = deltaX - block.Width / 2;
            double dy = deltaY - block.Height / 2;
            return (dx * dx + dy * dy <= (ball.Radius * ball.Radius));
        }

        private bool IsGameCleared()
        {
            foreach (var block in Blocks)
            {
                if (!block.IsBroken)
                {
                    return false;
                }
            }

            return true;
        }

        private void OnBallMoved(object sender, BallEventArgs e)
        {
            BallMoved?.Invoke(this, e);
        }

        private void OnPaddleMoved(object sender, PaddleEventArgs e)
        {
            PaddleMoved?.Invoke(this, e);
        }

        private void OnBrickBroken(object sender, BlockEventArgs e)
        {
            Score += 10;
            BrickBroken?.Invoke(this, e);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            isStarted = false;
            isPaused = false;
            GameOver?.Invoke(this, e);
        }

        public event EventHandler<BallEventArgs> BallMoved;

        public event EventHandler<PaddleEventArgs> PaddleMoved;

        public event EventHandler<BlockEventArgs> BrickBroken;

        public event EventHandler GameOver;
    }
}

