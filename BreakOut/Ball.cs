namespace BreakOut
{
    public class Ball
    {
        public float speedX;
        public float speedY;
        public float x;
        public float y;

        public void move()
        {
            x += speedX;
            y += speedY;
        }

        public void reverseX()
        {
            speedX *= -1;
        }

        public void reverseY()
        {
            speedY *= -1;
        }
    }

}
