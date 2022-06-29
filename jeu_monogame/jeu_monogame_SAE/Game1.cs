using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace jeu_monogame_SAE
{

    public enum Ecran { Accueil, Jeu, Jeu2, GameOver, Victory };
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private Accueil _screenAccueil;
        private GameOver _screenGameOver;
        private Jeu _screenJeu;
        private Jeu2 _screenJeu2;
        private Victory _screenVictory;
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;
        public Ecran _currentScreen;
        public static int niveau;

        private MouseState posSouris;
        private Point mousePosition;

        //music
        private Song _mySound;

        public GraphicsDeviceManager Graphics
        {
            get
            {
                return this._graphics;
            }

            set
            {
                this._graphics = value;
            }
        }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = 1600;
            Graphics.PreferredBackBufferHeight = 900;
            //Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _screenAccueil = new Accueil(this);
            _screenGameOver = new GameOver(this);
            _screenJeu = new Jeu(this);
            _screenJeu = new Jeu(this);
            _screenJeu2 = new Jeu2(this);
            _screenVictory = new Victory(this);
            _screenManager.LoadScreen(_screenAccueil, new FadeTransition(GraphicsDevice, Color.Black));
            _currentScreen = Ecran.Accueil;

            //music
            _mySound = Content.Load<Song>("Open");
            MediaPlayer.Play(_mySound);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            base.LoadContent();

        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            posSouris = Mouse.GetState();
            mousePosition = new Point(posSouris.X, posSouris.Y);


            if (_currentScreen == Ecran.Jeu)
            {
                niveau = 1;

            }
            else if (_currentScreen == Ecran.Jeu2)
                niveau = 2;


            if (posSouris.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine(mousePosition);
                if (_currentScreen == Ecran.GameOver)
                {
                    if (mousePosition.Y >= 810 && mousePosition.Y <= 950 && mousePosition.X >= 730 && mousePosition.X <= 1165)
                    {
                        Console.WriteLine("Restart");
                        LoadScreen3();
                        _currentScreen = Ecran.Jeu;

                    }
                }

                if (_currentScreen == Ecran.Accueil)
                {

                    if (mousePosition.Y >= 550 && mousePosition.Y <= 665 && mousePosition.X >= 680 && mousePosition.X <= 965)
                    {
                        Console.WriteLine("Start");
                        LoadScreen3();
                        _currentScreen = Ecran.Jeu;
                    }
                    if (mousePosition.Y >= 710 && mousePosition.Y <= 825 && mousePosition.X >= 680 && mousePosition.X <= 965)
                    {
                        Console.WriteLine("Exit");
                        Exit();
                    }
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void LoadScreen1()
        {
            _screenManager.LoadScreen(new Accueil(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen2()
        {
            _screenManager.LoadScreen(new GameOver(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen3()
        {
            _screenManager.LoadScreen(new Jeu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen4()
        {
            _screenManager.LoadScreen(new Jeu2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreen5()
        {
            _screenManager.LoadScreen(new Victory(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 1f;
            MediaPlayer.Play(_mySound);
        }
    }


}


