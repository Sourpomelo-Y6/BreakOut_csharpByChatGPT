namespace BreakOut
{
    public class Block
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public bool IsBroken { get; set; }

        public Block(double x, double y)
        {
            X = x;
            Y = y;
            Width = 50;
            Height = 15;
            IsBroken = false;
        }

        public void Break()
        {
            IsBroken = true;
        }
    }

}
