namespace Invasion.Renderers
{
    using Invasion.GameObjects;
    using Invasion.GameObjects.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    public class WpfGameRenderer : IGameRenderer
    {
        private const string SpaceshipImagePath = "../Resources/Images/spaceship.png";
        private const string MartianImagePath = "../Resources/Images/martian.png";
        private const string SithImagePath = "../Resources/Images/sith.png";
        private const string GunganImagePath = "../Resources/Images/gungan.png";
        private const string HadoukenImagePath = "../Resources/Images/hadouken.png";
        private const string MegaHadoukenImagePath = "../Resources/Images/megahadouken.png";
        private const string GameoverImagePath = "../Resources/Images/gameover.png";
        private const int GameoverWidth = 350;
        private const int GameoverHeight = 120;

        private readonly int GameoverLeft;
        private readonly int GameoverTop;

        private Canvas canvas;
        private GameWindow gameWindow;
        private bool isAllowedToFire;
        private Random randomGenerator;

        public event EventHandler<KeyDownEventArgs> KeyDownEvent;

        public WpfGameRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.gameWindow = (this.canvas.Parent as GameWindow);
            this.isAllowedToFire = true;
            this.randomGenerator = new Random();

            this.GameoverLeft = (this.Width / 2) - (GameoverWidth / 2);
            this.GameoverTop = (this.Height / 2) - (GameoverHeight / 2);
        }

        public int Width
        {
            get
            {
                return (int)this.canvas.Width;
            }
        }

        public int Height
        {
            get
            {
                return (int)this.canvas.Height;
            }
        }

        public bool IsInBounds(Position possiiton)
        {
            return 0 <= possiiton.Left && possiiton.Left <= this.Width &&
                   0 <= possiiton.Top && possiiton.Top <= this.Height;
        }

        public void Clear()
        {
            this.canvas.Children.Clear();
        }

        public void Draw(IGameObject gameObject)
        {
            string imagePath = String.Empty;

            if (gameObject is ISpaceshipGameObject)
            {
                imagePath = SpaceshipImagePath;
            }
            else if (gameObject is IAlienGameObject)
            {
                imagePath = this.GetAlienImagePath(gameObject);
            }
            else if (gameObject is IMegaHadoukenGameObject)
            {
                imagePath = MegaHadoukenImagePath;
            }
            else if (gameObject is IHadoukenGameObject)
            {
                imagePath = HadoukenImagePath;
            }

            this.AddToCanvas(imagePath, gameObject.Position, gameObject.Size);
        }

        public void Stop()
        {
            this.gameWindow.KeyDown -= this.HandleMoveShipEvent;
            this.gameWindow.KeyDown -= this.HandleShipStartsFireEvent;
            this.gameWindow.KeyUp -= this.HandleShipStopsFireEvent;
            this.gameWindow.KeyDown += this.HandleRestartGameEvent;

            //Draw GameOver
            var gameOverPosition = new Position(this.GameoverLeft, this.GameoverTop);
            var gameOverSize = new Size(GameoverWidth, GameoverHeight);
            this.AddToCanvas(GameoverImagePath, gameOverPosition, gameOverSize);
        }

        public void Start()
        {
            this.gameWindow.KeyDown -= this.HandleRestartGameEvent;
            this.gameWindow.KeyDown += this.HandleMoveShipEvent;
            this.gameWindow.KeyDown += this.HandleShipStartsFireEvent;
            this.gameWindow.KeyUp += this.HandleShipStopsFireEvent;
        }

        private string GetAlienImagePath(IGameObject gameObject)
        {
            var imagePath = String.Empty;

            var alien = gameObject as IAlienGameObject;
            switch (alien.Species)
            {
                case Species.Martian:
                    imagePath = MartianImagePath;
                    break;
                case Species.Sith:
                    imagePath = SithImagePath;
                    break;
                case Species.Gungan:
                    imagePath = GunganImagePath;
                    break;
            }

            return imagePath;
        }

        private void AddToCanvas(string imagePath, Position position, Size size)
        {
            var image = this.CreateImageForCanvas(imagePath, position, size);
            this.canvas.Children.Add(image);
        }

        private Image CreateImageForCanvas(string path, Position position, Size size)
        {
            Image image = new Image();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            image.Source = bitmap;
            image.Width = size.Width;
            image.Height = size.Height;

            Canvas.SetLeft(image, position.Left);
            Canvas.SetTop(image, position.Top);

            return image;
        }

        private void HandleMoveShipEvent(object sender, KeyEventArgs args)
        {
            //two separate ifs for reason (specific move of the ship)
            if (Keyboard.IsKeyDown(Key.Up))
            {
                this.KeyDownEvent(this, new KeyDownEventArgs(Command.MoveSpaceshipUp));
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                this.KeyDownEvent(this, new KeyDownEventArgs(Command.MoveSpaceshipDown));
            }
        }

        private void HandleShipStartsFireEvent(object sender, KeyEventArgs args)
        {
            //Holding space is not allowed
            if (Keyboard.IsKeyDown(Key.Space) && this.isAllowedToFire)
            {
                this.KeyDownEvent(this, new KeyDownEventArgs(Command.FireWithSpaceship));
                this.isAllowedToFire = false;
            }
        }

        private void HandleShipStopsFireEvent(object sender, KeyEventArgs args)
        {
            if (Keyboard.IsKeyUp(Key.Space))
            {
                this.isAllowedToFire = true;
            }
        }

        private void HandleRestartGameEvent(object sender, KeyEventArgs args)
        {
            this.KeyDownEvent(this, new KeyDownEventArgs(Command.RestartGame));
        }
    }
}