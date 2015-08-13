namespace Invasion.GameObjects
{
    using Invasion.GameObjects.Interfaces;

    public abstract class GameObject : IGameObject
    {
        public GameObject(Position position, Size size)
        {
            this.Position = position;
            this.Size = size;
            this.IsAlive = true;
        }

        public Position Position { get; protected set; }

        public Size Size { get; protected set; }

        public bool IsAlive { get; protected set; }

        public bool IsOverlapping(IGameObject gameObject)
        {
            IGameObject biggerObject;
            IGameObject smallerObject;

            if (this.Size.Width > gameObject.Size.Width)
            {
                biggerObject = this;
                smallerObject = gameObject;
            }
            else
            {
                biggerObject = gameObject;
                smallerObject = this;
            }

            var smallerObjectTopLeftCorner = new Position(smallerObject.Position.Left, smallerObject.Position.Top);
            var smallerObjectTopRightCorner = new Position(smallerObject.Position.Left + smallerObject.Size.Width, smallerObject.Position.Top);
            var smallerObjectBottomRightCorner = new Position(smallerObject.Position.Left + smallerObject.Size.Width, smallerObject.Position.Top + smallerObject.Size.Height);
            var smallerObjectBottomLeftCorner = new Position(smallerObject.Position.Left, smallerObject.Position.Top + smallerObject.Size.Height);

            var corners = new Position[] 
            { 
                smallerObjectTopLeftCorner,
                smallerObjectTopRightCorner,
                smallerObjectBottomRightCorner,
                smallerObjectBottomLeftCorner
            };

            var biggerObjectLeftBound = biggerObject.Position.Left;
            var biggerObjectRightBound = biggerObject.Position.Left + biggerObject.Size.Width;
            var biggerObjectTopBound = biggerObject.Position.Top;
            var biggerObjectBottomBound = biggerObject.Position.Top + biggerObject.Size.Height;

            foreach (var corner in corners)
            {
                if (biggerObjectLeftBound <= corner.Left && corner.Left <= biggerObjectRightBound &&
                    biggerObjectTopBound <= corner.Top && corner.Top <= biggerObjectBottomBound)
                {
                    return true;
                }
            }

            return false;
        }

        public void Kill()
        {
            this.IsAlive = false;
        }
    }
}