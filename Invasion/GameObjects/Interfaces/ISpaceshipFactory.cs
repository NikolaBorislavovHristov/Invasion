namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;
    using Invasion.Renderers;

    public interface ISpaceshipFactory
    {
        ISpaceshipGameObject Get(IGameRenderer renderer);
    }
}