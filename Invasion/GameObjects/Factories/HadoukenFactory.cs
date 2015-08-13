namespace Invasion.GameObjects.Factories
{
    using Invasion.GameObjects.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public class HadoukenFactory : IHadoukenFactory
    {
        private const int HadoukenWidth = 25;
        private const int HadoukenHeight = 25;

        private const int MegaHadoukenWidth = 75;
        private const int MegaHadoukenHeight = 75;
        private const int MegaHadoukenChance = 5;

        private Random randomGenerator;
        private MediaPlayer player;

        public HadoukenFactory()
        {
            this.randomGenerator = new Random();
            this.player = new MediaPlayer();
        }

        public IEnumerable<IHadoukenGameObject> Get(Size shooterSize, Position shooterPosition)
        {
            List<IHadoukenGameObject> hadoukens = new List<IHadoukenGameObject>();

            int randomNumber = this.randomGenerator.Next(1, 101);
            bool isThereMegaHadouken = (randomNumber <= MegaHadoukenChance);

            if (isThereMegaHadouken)
            {
                this.AddMegaHadouken(hadoukens, shooterSize, shooterPosition);
                this.PlayMegaHadoukenSound();
            }
            else
            {
                this.AddHadoukens(hadoukens, shooterSize, shooterPosition);
            }

            return hadoukens;
        }

        private void AddMegaHadouken(List<IHadoukenGameObject> hadoukens, Size shooterSize, Position shooterPosition)
        {
            var megaHadoukenSize = new Size(MegaHadoukenWidth, MegaHadoukenHeight);

            int megaHadoukenLeft = shooterPosition.Left + shooterSize.Width;
            int megaHadoukenTop = shooterPosition.Top;
            var megaHadoukenPosition = new Position(megaHadoukenLeft, megaHadoukenTop);

            var megaHadouken = new MegaHadoukenGameObject(megaHadoukenPosition, megaHadoukenSize);

            hadoukens.Add(megaHadouken);
        }

        private void AddHadoukens(List<IHadoukenGameObject> hadoukens, Size shooterSize, Position shooterPosition)
        {
            var hadoukenSize = new Size(HadoukenWidth, HadoukenHeight);

            int topHadoukenLeft = shooterPosition.Left + shooterSize.Width;
            int topHadoukenTop = shooterPosition.Top;
            var topHadoukenPosition = new Position(topHadoukenLeft, topHadoukenTop);

            int bottomHadoukenLeft = shooterPosition.Left + shooterSize.Width;
            int bottomHadoukenTop = shooterPosition.Top + shooterSize.Height - hadoukenSize.Height;
            var bottomHadoukenPosition = new Position(bottomHadoukenLeft, bottomHadoukenTop);  

            var topHadouken = new HadoukenGameObject(topHadoukenPosition, hadoukenSize);
            var bottomHadouken = new HadoukenGameObject(bottomHadoukenPosition, hadoukenSize);

            hadoukens.Add(topHadouken);
            hadoukens.Add(bottomHadouken);
        }

        private void PlayMegaHadoukenSound()
        {
            //this.player.Open(new Uri(MegaHadoukenSoundPath, UriKind.RelativeOrAbsolute));
            //this.player.Play();
        }
    }
}