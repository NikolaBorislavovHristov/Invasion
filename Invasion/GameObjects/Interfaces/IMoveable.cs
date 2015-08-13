namespace Invasion.GameObjects.Interfaces
{
    public interface IMoveable
    {
        /// <summary>
        /// Move in default direction if such direction exist, else do nothing.
        /// </summary>
        void Move();
        void Move(Direction direction);
    }
}