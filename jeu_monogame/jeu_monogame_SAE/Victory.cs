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
    public class Victory : GameScreen
    {
        private Game1 _myGame;

        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMapVictory;
        private TiledMapRenderer _tiledMapRendererVictory;


        private MouseState posSouris;

        private Point mousePosition;
        public Victory(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        { }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMapVictory = Content.Load<TiledMap>("Victory");
            _tiledMapRendererVictory = new TiledMapRenderer(GraphicsDevice, _tiledMapVictory);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            posSouris = Mouse.GetState();
            mousePosition = new Point(posSouris.X, posSouris.Y);

            if (posSouris.LeftButton == ButtonState.Pressed)
            {
                if (mousePosition.Y >= 710 && mousePosition.Y <= 840 && mousePosition.X >= 645 && mousePosition.X <= 975)
                {
                    Console.WriteLine("Home");
                    _myGame.LoadScreen3();
                    _myGame._currentScreen = Ecran.Jeu;
                }
            }


        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow);

            _spriteBatch.Begin();
            _tiledMapRendererVictory.Draw();
            _spriteBatch.End();
        }
    }
}