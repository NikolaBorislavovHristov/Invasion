namespace Invasion.Renderers
{
    using Invasion.GameObjects;
    using Invasion.GameObjects.Interfaces;
    using System;

    public interface IGameRenderer
    {
        event EventHandler<KeyDownEventArgs> KeyDownEvent;
        int Width { get; }
        int Height { get; }
        bool IsInBounds(Position position);
        void Clear();
        void Draw(IGameObject gameObject);
        void Stop();
        void Start();
    }
}