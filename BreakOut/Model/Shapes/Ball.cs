namespace BreakOut.Model.Shapes
{
    public class Ball : Shape
    {

        public double SpeedX { get; private set; }

        public double SpeedY { get; private set; }


        private readonly double initialSpeed = -5;

        private readonly double maxSpeed = 10;
        internal double Radius;

        public Ball(double x, double y)
        {
            X = x;
            Y = y;
            SpeedX = initialSpeed;
            SpeedY = initialSpeed;
            Radius = 10;
            Width = Radius;
            Height = Radius;
        }

        public void Reset()
        {
            X = 250;
            Y = 250;
            SpeedX = initialSpeed;
            SpeedY = initialSpeed;
        }

        public void Move()
        {
            X += SpeedX;
            Y += SpeedY;

            if (SpeedX > maxSpeed)
            {
                SpeedX = maxSpeed;
            }

            if (SpeedY > maxSpeed)
            {
                SpeedY = maxSpeed;
            }
        }

        public void ReverseX()
        {
            SpeedX = -SpeedX;
        }

        public void ReverseY()
        {
            SpeedY = -SpeedY;
        }
    }
}
