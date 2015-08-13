namespace Invasion.GameObjects.Interfaces
{
    public interface IHadoukenGameObject : IGameObject, IMoveable
    {
        Direction DefaultDirection { get; }
    }
}