namespace BreakOut
{
    public class Paddle
    {
        private float width;
        private float height;
        public float x;
        public float y;

        public void moveRight()
        {
            x += 10f;
        }

        public void moveLeft()
        {
            x -= 10f;
        }
    }

}
