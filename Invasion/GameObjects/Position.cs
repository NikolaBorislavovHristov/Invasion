namespace Invasion.GameObjects
{
    public struct Position
    {
        public Position(int left, int top)
            : this()
        {
            this.Left = left;
            this.Top = top;
        }

        public int Left { get; private set; }

        public int Top { get; private set; }
    }
}