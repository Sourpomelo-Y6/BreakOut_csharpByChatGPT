using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BreakOut
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ゲームオブジェクトを作成
            game = new Game(canvas.ActualWidth, canvas.ActualHeight);

            // パドルを追加
            AddPaddle(game.Paddle);

            // ブロックを追加
            foreach (var block in game.Blocks)
            {
                AddBlock(block);
            }

            // ボールを追加
            AddBall(game.Ball);
        }

        private void AddPaddle(Paddle paddle)
        {
            var rect = new Rectangle
            {
                Width = paddle.Width,
                Height = paddle.Height,
                Fill = Brushes.Black
            };

            canvas.Children.Add(rect);

            Canvas.SetLeft(rect, paddle.X - paddle.Width / 2);
            Canvas.SetTop(rect, paddle.Y - paddle.Height / 2);
        }

        private void AddBlock(Block block)
        {
            if (block.IsBroken)
            {
                return;
            }

            var rect = new Rectangle
            {
                Width = block.Width,
                Height = block.Height,
                Fill = Brushes.Red
            };

            canvas.Children.Add(rect);

            Canvas.SetLeft(rect, block.X - block.Width / 2);
            Canvas.SetTop(rect, block.Y - block.Height / 2);
        }

        private void AddBall(Ball ball)
        {
            var ellipse = new Ellipse
            {
                Width = ball.Radius * 2,
                Height = ball.Radius * 2,
                Fill = Brushes.Blue
            };

            canvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, ball.X - ball.Radius);
            Canvas.SetTop(ellipse, ball.Y - ball.Radius);
        }

        private void UpdatePaddle(Paddle paddle)
        {
            foreach (var child in canvas.Children)
            {
                if (child is Rectangle rect && rect.Fill == Brushes.Black)
                {
                    Canvas.SetLeft(rect, paddle.X - paddle.Width / 2);
                    Canvas.SetTop(rect, paddle.Y - paddle.Height / 2);
                    break;
                }
            }
        }
        private void UpdateBall(Ball ball)
        {
            foreach (var child in canvas.Children)
            {
                if (child is Ellipse ellipse && ellipse.Fill == Brushes.Blue)
                {
                    Canvas.SetLeft(ellipse, ball.X - ball.Radius);
                    Canvas.SetTop(ellipse, ball.Y - ball.Radius);
                    break;
                }
            }
        }

        private void RemoveBlock(Block block)
        {
            foreach (var child in canvas.Children)
            {
                if (child is Rectangle rect && rect.Fill == Brushes.Red &&
                    Canvas.GetLeft(rect) == block.X - block.Width / 2 &&
                    Canvas.GetTop(rect) == block.Y - block.Height / 2)
                {
                    canvas.Children.Remove(rect);
                    break;
                }
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            startButton.Visibility = Visibility.Collapsed;
            resetButton.Visibility = Visibility.Visible;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            game.Reset();
            foreach (var child in canvas.Children)
            {
                canvas.Children.Remove(child);
            }
            Window_Loaded(sender, e);
            startButton.Visibility = Visibility.Visible;
            resetButton.Visibility = Visibility.Collapsed;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    game.MovePaddleLeft();
                    break;
                case Key.Right:
                    game.MovePaddleRight();
                    break;
            }
        }

        private void game_GameOver(object sender, System.EventArgs e)
        {
            MessageBox.Show($"Game Over! Score: {game.Score}");
            resetButton_Click(sender, null);
        }

        private void game_BrickBroken(object sender, BlockEventArgs e)
        {
            RemoveBlock(e.Block);
        }

        private void game_BallMoved(object sender, BallEventArgs e)
        {
            UpdateBall(e.Ball);
        }

        private void game_PaddleMoved(object sender, PaddleEventArgs e)
        {
            UpdatePaddle(e.Paddle);
        }
    }

}
