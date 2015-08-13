namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;
    using Invasion.Renderers;

    public class SpaceshipFactory : ISpaceshipFactory
    {
        private const int SpaceshipWidth = 115;
        private const int SpaceshipHeight = 75;

        public SpaceshipFactory()
        {
        }

        public ISpaceshipGameObject Get(IGameRenderer renderer)
        {
            var spaceshipSize = new Size(SpaceshipWidth, SpaceshipHeight);

            int spaceshipLeft = 0;
            int spaceshipTop = (renderer.Height / 2) - (spaceshipSize.Height / 2);

            var spaceshipPosition = new Position(spaceshipLeft, spaceshipTop);

            ISpaceshipGameObject newSpaceship = new SpaceshipGameObject(spaceshipPosition, spaceshipSize, renderer);
            return newSpaceship;
        }
    }
}