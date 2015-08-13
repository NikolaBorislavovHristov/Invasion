namespace Invasion.GameObjects
{
    using Invasion.GameObjects.Interfaces;
    using System;

    public class AlienGameObject : GameObject, IAlienGameObject
    {
        private const int TopPositionChangeFrom = -4;
        private const int TopPositionChangeTo = 4;
        private const int MoveStep = 5;
        private const Direction DirectionByDefault = Direction.Left;

        private Random randomGenerator;

        public AlienGameObject(Position position, Size size, Species species)
            : base(position, size)
        {
            this.Species = species;
            this.DefaultDirection = DirectionByDefault;
            this.randomGenerator = new Random();
        }

        public Species Species { get; protected set; }

        public Direction DefaultDirection { get; protected set; }

        public void Move()
        {
            this.Move(this.DefaultDirection);
        }

        public void Move(Direction direction)
        {
            Position newPosition;

            switch (direction)
            {
                case Direction.Left:
                    newPosition = this.GetPositionInLeftDirection();
                    break;
                default:
                    newPosition = this.Position;
                    break;
            }

            this.Position = newPosition;
        }

        private Position GetPositionInLeftDirection()
        {
            int newLeft = this.Position.Left - MoveStep;
            int newTop = this.Position.Top + this.randomGenerator.Next(TopPositionChangeFrom, TopPositionChangeTo);
            Position newPosition = new Position(newLeft, newTop);

            return newPosition;
        }
    }
}