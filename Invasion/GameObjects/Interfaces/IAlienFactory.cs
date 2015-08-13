namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;

    public interface IAlienFactory
    {
        IAlienGameObject Get(Position position);
    }
}