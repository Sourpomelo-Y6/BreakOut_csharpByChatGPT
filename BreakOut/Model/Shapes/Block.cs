namespace BreakOut.Model.Shapes
{
    public class Block : Shape
    {
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
