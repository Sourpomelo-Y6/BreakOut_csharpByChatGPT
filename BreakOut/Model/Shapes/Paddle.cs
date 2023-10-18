namespace BreakOut.Model.Shapes
{
    public class Paddle : Shape
    {

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

            if (X + Width / 2 > 640)
            {
                X = 640 - Width / 2;
            }
        }
    }

}
