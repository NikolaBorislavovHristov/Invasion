namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;
    using System;

    public class AlienFactory : IAlienFactory
    {
        private const int AlienWidth = 50;
        private const int AlienHeight = 50;

        private int numberOfSpecies;
        private Random randomGenerator;

        public AlienFactory()
        {
            this.numberOfSpecies = Enum.GetValues(typeof(Species)).Length;
            this.randomGenerator = new Random();
        }

        public IAlienGameObject Get(Position position)
        {
            Species species = (Species)this.randomGenerator.Next(1, this.numberOfSpecies + 1);
            var alienSize = new Size(AlienWidth, AlienHeight);
            IAlienGameObject newAlien = new AlienGameObject(position, alienSize, species);
            return newAlien;
        }
    }
}