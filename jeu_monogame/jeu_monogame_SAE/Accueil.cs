using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;


namespace jeu_monogame_SAE
{
    
    public class Accueil : GameScreen

    {
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMapAccueil;
        private TiledMapRenderer _tiledMapRendererAccueil;
        private Game _myGame;

        public Accueil(Game game) : base(game)
        {
            _myGame = game;

            Content.RootDirectory = "Content";  // pas obligé
        }

        

        public override void Initialize()
        {            
            base.Initialize();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tiledMapAccueil = Content.Load<TiledMap>("accueilv2");
            _tiledMapRendererAccueil = new TiledMapRenderer(GraphicsDevice, _tiledMapAccueil);
            base.LoadContent();            
        }

        public override void Update(GameTime gameTime)
        {
            _tiledMapRendererAccueil.Update(gameTime);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();            
            _tiledMapRendererAccueil.Draw();
            _spriteBatch.End();
        }
    }
}



