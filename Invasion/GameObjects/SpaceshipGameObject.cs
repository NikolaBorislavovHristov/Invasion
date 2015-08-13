namespace Invasion.GameObjects
{
    using Invasion.GameObjects.Interfaces;
    using Invasion.Renderers;

    public class SpaceshipGameObject : GameObject, ISpaceshipGameObject
    {
        private const int MoveStep = 10;

        private IGameRenderer renderer;

        public SpaceshipGameObject(Position position, Size size, IGameRenderer renderer)
            : base(position, size)
        {
            this.renderer = renderer;
        }

        public void Move()
        {
        }

        public void Move(Direction direction)
        {
            Position newPosition = this.Position;

            switch (direction)
            {
                case Direction.Up:
                    if (this.Position.Top > 0)
                    {
                        newPosition = this.GetPositionInUpDirection();
                    }
                    break;
                case Direction.Down:
                    if (this.Position.Top + this.Size.Height < this.renderer.Height)
                    {
                        newPosition = this.GetPositionInDownDirection();
                    }
                    break;
            }

            this.Position = newPosition;
        }

        private Position GetPositionInUpDirection()
        {
            int newLeft = this.Position.Left;
            int newTop = this.Position.Top - MoveStep;
            Position newPosition = new Position(newLeft, newTop);

            return newPosition;
        }

        private Position GetPositionInDownDirection()
        {
            int newLeft = this.Position.Left;
            int newTop = this.Position.Top + MoveStep;
            Position newPosition = new Position(newLeft, newTop);

            return newPosition;
        }
    }
}