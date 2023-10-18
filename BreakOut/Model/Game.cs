using BreakOut.Model.Shapes;
using System;
using System.Timers;

namespace BreakOut.Model
{
    public class Game
    {
        public int Score { get; private set; }

        public Ball Ball { get; private set; }

        public Paddle Paddle { get; private set; }

        public Block[] Blocks { get; private set; }
        
        private bool isStarted = false;

        private bool isPaused = false;
        private bool isOvered = false;
        private readonly double canvasWidth;

        private readonly double canvasHeight;

        public Game()
        {
            this.canvasWidth = 640;//canvasWidth;
            this.canvasHeight = 640;//canvasHeight;

            Ball = new Ball(canvasWidth / 2, canvasHeight / 2);
            Paddle = new Paddle(canvasWidth / 2, canvasHeight - 50);
            Blocks = new Block[40];

            for (int i = 0; i < 40; i++)
            {
                var x = (i % 8) * 60 + 30;
                var y = (i / 8) * 20 + 30;
                Blocks[i] = new Block(x, y);
            }

            //BallMoved += OnBallMoved;
            //PaddleMoved += OnPaddleMoved;
            //BlockBroken += OnBlockBroken;
            //GameOver += OnGameOver;
        }

        public bool Start()
        {
            if (isStarted)
            {
                return false;
            }

            isStarted = true;
            isPaused = false;

            Ball.Reset();
            Score = 0;

            return true;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public bool IsOvered() 
        {
            return isOvered;
        } 

        public void Reset()
        {
            isStarted = false;
            isPaused = false;
            isOvered = false;
            Ball.Reset();
            Score = 0;

            foreach (var block in Blocks)
            {
                block.IsBroken = false;
            }

            //BallMoved?.Invoke(this, new BallEventArgs(Ball));
            //PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        public void MovePaddleLeft()
        {
            if (!isStarted || isPaused)
            {
                return;
            }

            Paddle.MoveLeft();
            //PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        public void MovePaddleRight()
        {
            if (!isStarted || isPaused)
            {
                return;
            }

            Paddle.MoveRight();
            //PaddleMoved?.Invoke(this, new PaddleEventArgs(Paddle));
        }

        public void OnTimerElapsed()
        {
            if (!isStarted || isPaused)
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
                OnGameOver();//?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (IsBallCollidedWithPaddle(Ball, Paddle))
            {
                Ball.ReverseY();
            }

            for (int i = 0; i < Blocks.Length; i++)
            {
                var block = Blocks[i];


                if (!block.IsBroken && IsBallCollidedWithBlock(Ball,block))
                {
                    switch (CheckBlockCollision(Ball, block))
                    {
                        case BlockCollisionType.Top:
                            Ball.ReverseY();
                            break;
                        case BlockCollisionType.Bottom:
                            Ball.ReverseY();
                            break;
                        case BlockCollisionType.Left:
                            Ball.ReverseX();
                            break;
                        case BlockCollisionType.Right:
                            Ball.ReverseX();
                            break;
                        default:
                            //error!
                            break;
                    }

                    block.Break();
                    //BlockBroken?.Invoke(this, new BlockEventArgs(block));
                    Score += 10;
                    if (IsGameCleared())
                    {
                        OnGameOver();//?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }
            }

            //BallMoved?.Invoke(this, new BallEventArgs(Ball));
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


        public enum BlockCollisionType
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }

        public static BlockCollisionType CheckBlockCollision(Ball ball, Block block)
        {
            double ballBottom = ball.Y + ball.Radius;
            double ballTop = ball.Y - ball.Radius;
            double ballLeft = ball.X - ball.Radius;
            double ballRight = ball.X + ball.Radius;

            double blockBottom = block.Y + block.Height / 2;
            double blockTop = block.Y - block.Height / 2;
            double blockLeft = block.X - block.Width / 2;
            double blockRight = block.X + block.Width / 2;

            if (ballBottom >= blockTop && ballTop <= blockBottom && ballRight >= blockLeft && ballLeft <= blockRight)
            {
                // Ball is colliding with block
                double overlapBottom = blockBottom - ballTop;
                double overlapTop = ballBottom - blockTop;
                double overlapLeft = ballRight - blockLeft;
                double overlapRight = blockRight - ballLeft;

                if (overlapBottom <= overlapTop && overlapBottom <= overlapLeft && overlapBottom <= overlapRight)
                {
                    return BlockCollisionType.Top;
                }
                else if (overlapTop <= overlapBottom && overlapTop <= overlapLeft && overlapTop <= overlapRight)
                {
                    return BlockCollisionType.Bottom;
                }
                else if (overlapLeft <= overlapBottom && overlapLeft <= overlapTop && overlapLeft <= overlapRight)
                {
                    return BlockCollisionType.Left;
                }
                else if (overlapRight <= overlapBottom && overlapRight <= overlapTop && overlapRight <= overlapLeft)
                {
                    return BlockCollisionType.Right;
                }
            }

            return BlockCollisionType.None;
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

        //private void OnBallMoved(object sender, BallEventArgs e)
        //{
        //    //BallMoved?.Invoke(this, e);
        //}

        //private void OnPaddleMoved(object sender, PaddleEventArgs e)
        //{
        //    //PaddleMoved?.Invoke(this, e);
        //}

        //private void OnBlockBroken(object sender, BlockEventArgs e)
        //{
        //    Score += 10;
        //    //BrickBroken?.Invoke(this, e);
        //}

        private void OnGameOver()
        {
            if (isStarted)
            {
                isStarted = false;
                isPaused = false;
                isOvered = true;
                //GameOver?.Invoke(this, e);
            }
        }

        //public event EventHandler<BallEventArgs> BallMoved;

        //public event EventHandler<PaddleEventArgs> PaddleMoved;

        //public event EventHandler<BlockEventArgs> BlockBroken;

        //public event EventHandler GameOver;
    }
}

