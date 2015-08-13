namespace Invasion.Engine
{
    using Invasion.GameObjects;
    using Invasion.GameObjects.Factories;
    using Invasion.GameObjects.Interfaces;
    using Invasion.Players;
    using Invasion.Renderers;
    using System;
    using System.Collections.Generic;
    using System.Windows.Threading;

    public class GameEngine
    {
        private const string ThemeSongPath = "../../Resources/Sounds/Prodigy.mp3";
        private const string MegaHadoukenSoundPath = "../../Resources/Sounds/MegaHadoukenSound.mp3";
        private const int TimeSpanInMilliseconds = 30;
        private const int FrequencyOfAlienCreating = 10;

        private IGameRenderer renderer;
        private ISpaceshipFactory spaceshipFactory;
        private IAlienFactory alienFactory;
        private IHadoukenFactory hadoukenFactory;
        private IGamePlayer gamePlayer;

        private ISpaceshipGameObject spaceship;
        private List<IAlienGameObject> aliens;
        private List<IHadoukenGameObject> hadoukens;

        private int cycle;
        private DispatcherTimer timer;
        private Random randomGenerator;

        public GameEngine(IGameRenderer renderer, ISpaceshipFactory spaceshipFactory, IAlienFactory alienFactory, IHadoukenFactory hadoukenFactory, IGamePlayer gamePlayer)
        {
            this.renderer = renderer;
            this.renderer.KeyDownEvent += this.HandleGameCommandUsedEvent;

            this.spaceshipFactory = spaceshipFactory;
            this.alienFactory = alienFactory;
            this.hadoukenFactory = hadoukenFactory;

            this.hadoukens = new List<IHadoukenGameObject>();
            this.aliens = new List<IAlienGameObject>();

            this.randomGenerator = new Random();
            this.gamePlayer = gamePlayer;
        }

        public void InitializeGame()
        {
            this.spaceship = this.spaceshipFactory.Get(this.renderer);
            this.hadoukens.Clear();
            this.aliens.Clear();
            this.renderer.Clear();
            this.cycle = 0;
            this.SetTimer();
            this.gamePlayer.PlayAndRepeat(ThemeSongPath);
        }

        public void StartGame()
        {
            this.renderer.Start();
            this.timer.Start();
        }

        private void SetTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(TimeSpanInMilliseconds);
            this.timer.Tick += this.GameLoop;
        }

        private void GameLoop(object sender, EventArgs args)
        {
            this.cycle++;
            this.ClearRenderer();

            this.CreatAlien();

            this.RemoveGameObjectsOutOfBounds(this.aliens);
            this.RemoveGameObjectsOutOfBounds(this.hadoukens);
            this.RemoveGameObjectsInContact(this.aliens, this.hadoukens);

            this.MoveGameObjects(this.aliens);
            this.MoveGameObjects(this.hadoukens);

            this.DrawGameObjects(this.spaceship);
            this.DrawGameObjects(this.aliens);
            this.DrawGameObjects(this.hadoukens);

            if (this.SpaceshipCrashesIntoEnemy(this.aliens))
            {
                this.GameOver();
            }
        }

        private void ClearRenderer()
        {
            this.renderer.Clear();
        }

        private void DrawGameObjects(IGameObject gameObject)
        {
            this.renderer.Draw(gameObject);
        }

        private void DrawGameObjects<T>(IEnumerable<T> gameObjects) where T : IGameObject
        {
            foreach (var gameObject in gameObjects)
            {
                this.DrawGameObjects(gameObject);
            }
        }

        private bool SpaceshipCrashesIntoEnemy<T>(IEnumerable<T> enemyGameObjects) where T : IGameObject
        {
            foreach (var enemyGameObject in enemyGameObjects)
            {
                if (this.spaceship.IsOverlapping(enemyGameObject))
                {
                    return true;
                }
            }

            return false;
        }

        private void RemoveGameObjectsOutOfBounds<T>(List<T> gameObjects) where T : IGameObject
        {
            foreach (var gameObject in gameObjects)
            {
                if (!this.renderer.IsInBounds(gameObject.Position))
                {
                    gameObject.Kill();
                }
            }

            gameObjects.RemoveAll(go => !go.IsAlive);
        }

        private void RemoveGameObjectsInContact<T, U>(List<T> tGroup, List<U> uGroup) where T : IGameObject /*and*/ where U : IGameObject
        {
            foreach (var tElement in tGroup)
            {
                foreach (var uElement in uGroup)
                {
                    if (tElement.IsOverlapping(uElement))
                    {
                        tElement.Kill();
                        uElement.Kill();
                    }
                }
            }

            tGroup.RemoveAll(tElement => !tElement.IsAlive);
            uGroup.RemoveAll(uElement => !uElement.IsAlive);
        }

        private void MoveGameObjects<T>(IEnumerable<T> gameObjects) where T : IMoveable
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Move();
            }
        }

        private void CreatAlien()
        {
            if (this.cycle % FrequencyOfAlienCreating == 0)
            {
                var alienPosition = new Position(this.renderer.Width, this.randomGenerator.Next(this.renderer.Height));
                var newAlien = this.alienFactory.Get(alienPosition);
                this.aliens.Add(newAlien);
            }
        }

        private void GameOver()
        {
            this.renderer.Stop();
            this.gamePlayer.ClearPlaylist();
            this.timer.Stop();
        }

        private void HandleGameCommandUsedEvent(object sender, KeyDownEventArgs args)
        {
            switch (args.Command)
            {
                case Command.MoveSpaceshipUp:
                    this.spaceship.Move(Direction.Up);
                    break;
                case Command.MoveSpaceshipDown:
                    this.spaceship.Move(Direction.Down);
                    break;
                case Command.FireWithSpaceship:
                    this.FireHadouken(this.spaceship);
                    break;
                case Command.RestartGame:
                    this.InitializeGame();
                    this.StartGame();
                    break;
            }
        }

        private void FireHadouken(ISpaceshipGameObject shooter)
        {
            var generatedHadoukens = this.hadoukenFactory.Get(shooter.Size, shooter.Position);
            foreach (var hadouken in generatedHadoukens)
            {
                this.hadoukens.Add(hadouken);
                if (hadouken is IMegaHadoukenGameObject)
                {
                    this.gamePlayer.PlayOnce(MegaHadoukenSoundPath);
                }
            }
        }   
    }
}