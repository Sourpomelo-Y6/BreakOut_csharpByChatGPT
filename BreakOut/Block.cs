namespace BreakOut
{
    public class Block
    {
        private float width;
        private float height;
        public float x;
        public float y;
        public bool isBroken;

        public void breakBlock()
        {
            isBroken = true;
        }
    }

}
