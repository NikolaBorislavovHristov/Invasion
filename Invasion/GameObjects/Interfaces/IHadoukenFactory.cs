namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;
    using System.Collections.Generic;

    public interface IHadoukenFactory
    {
        IEnumerable<IHadoukenGameObject> Get(Size shooterSize, Position shooterPosition);
    }
}