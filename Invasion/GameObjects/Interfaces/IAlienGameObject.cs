namespace Invasion.GameObjects.Interfaces
{
    public interface IAlienGameObject : IGameObject, IMoveable
    {
        Species Species { get; }
        Direction DefaultDirection { get; }
    }
}