namespace Invasion.GameObjects
{
    using System;

    public struct Size
    {
        private const string WidthOutOfRangeMessage = "Width must be greater than zero.";
        private const string HeightOutOfRangeMessage = "Height must be greater than zero.";

        private int width;
        private int height;

        public Size(int width, int height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(WidthOutOfRangeMessage);
                }

                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(HeightOutOfRangeMessage);
                }

                this.height = value;
            }
        }
    }
}