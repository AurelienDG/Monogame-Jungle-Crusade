using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Text;


namespace jeu_monogame_SAE
{
    public class GameOver : GameScreen
    {
        private Game1 _myGame;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMapGameOver;
        private TiledMapRenderer _tiledMapRendererGameOver;
        private MouseState posSouris;

        private Point mousePosition;
        public GameOver(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        { }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMapGameOver = Content.Load<TiledMap>("gameover");
            _tiledMapRendererGameOver = new TiledMapRenderer(GraphicsDevice, _tiledMapGameOver);
            
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            posSouris = Mouse.GetState();
            mousePosition = new Point(posSouris.X, posSouris.Y);

            if (posSouris.LeftButton == ButtonState.Pressed)
            {
                if (mousePosition.Y >= 655 && mousePosition.Y <= 790 && mousePosition.X >= 605 && mousePosition.X <= 1040)
                {
                    Console.WriteLine("Restart");
                    _myGame.LoadScreen3();
                    _myGame._currentScreen = Ecran.Jeu;
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow);
            
            _spriteBatch.Begin();
            _tiledMapRendererGameOver.Draw();
            _spriteBatch.End();
        }
    }
}

