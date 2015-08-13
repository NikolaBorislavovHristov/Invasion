namespace Invasion.GameObjects
{
    using Invasion.GameObjects.Interfaces;

    public class HadoukenGameObject : GameObject, IHadoukenGameObject
    {
        private const int MoveStep = 15;
        private const Direction DirectionByDefault = Direction.Right;

        public HadoukenGameObject(Position position, Size size)
            : base(position, size)
        {
            this.DefaultDirection = DirectionByDefault;
        }

        public Direction DefaultDirection { get; protected set; }

        public void Move()
        {
            this.Move(this.DefaultDirection);
        }

        public void Move(Direction direction)
        {
            Position newPosition = this.Position;

            switch (direction)
            {
                case Direction.Right:
                    newPosition = this.GetPositionInRightDirection();
                    break;
                default:
                    newPosition = this.GetPositionInOtherDirection();
                    break;
            }

            this.Position = newPosition;
        }

        private Position GetPositionInRightDirection()
        {
            int newLeft = this.Position.Left + MoveStep;
            int newTop = this.Position.Top;
            Position newPosition = new Position(newLeft, newTop);

            return newPosition;
        }

        private Position GetPositionInOtherDirection()
        {
            Position newPosition = this.Position;
            return newPosition;
        }
    }
}