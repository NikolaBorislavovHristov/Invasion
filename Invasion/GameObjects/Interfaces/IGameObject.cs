namespace Invasion.GameObjects.Interfaces
{
    public interface IGameObject
    {
        Position Position { get; }
        Size Size { get; }
        bool IsAlive { get; }
        bool IsOverlapping(IGameObject gameObject);
        void Kill();
    }
}