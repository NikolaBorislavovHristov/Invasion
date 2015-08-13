namespace Invasion.GameObjects
{
    using Invasion.GameObjects.Interfaces;

    public class MegaHadoukenGameObject : HadoukenGameObject, IMegaHadoukenGameObject
    {
        public MegaHadoukenGameObject(Position position, Size size)
            : base(position, size)
        {
        }
    }
}