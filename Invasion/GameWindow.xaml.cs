namespace Invasion
{
    using Invasion.Engine;
    using Invasion.GameObjects.Factories;
    using Invasion.Players;
    using Invasion.Renderers;
    using System.Windows;

    public partial class GameWindow : Window
    {
        private GameEngine engine;

        public GameWindow()
        {
            this.InitializeComponent();

            var wpfGameRenderer = new WpfGameRenderer(this.GameCanvas);
            var spaceshipFactory = new SpaceshipFactory();
            var alienFactory = new AlienFactory();
            var hadoukenFactory = new HadoukenFactory();
            var gamePlayer = new GamePlayer();

            this.engine = new GameEngine(wpfGameRenderer, spaceshipFactory, alienFactory, hadoukenFactory, gamePlayer);
            this.engine.InitializeGame();
            this.engine.StartGame();
        }
    }
}