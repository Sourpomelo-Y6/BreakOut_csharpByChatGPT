using System;

namespace BreakOut
{
    public class BlockEventArgs : EventArgs
    {
        public Block Block { get; private set; }

        public BlockEventArgs(Block block)
        {
            Block = block;
        }
    }
}