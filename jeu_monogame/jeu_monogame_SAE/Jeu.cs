using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace jeu_monogame_SAE
{
    public class Jeu : GameScreen

    {
        private SpriteBatch _spriteBatch;

        public static Vector2 _posPango;
        public static Vector2 _posBat;
        public static bool _pangoTouche;
        public static bool _collisionZonePango;
        public static Rectangle rectZonePango;
        public static Rectangle rectZoneBat;
        public static Rectangle rectPorte;

        //Sprites
        private Sprite _pangolin;
        private Sprite _bat;
        public Sprite _perso;
        private Sprite _vie;
        private Sprite _epeee;
        //map
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;

        private Game1 _myGame;


        public Jeu(Game1 game) : base(game)
        {
            _myGame = game;
            Content.RootDirectory = "Content";
        }

        public override void Initialize()
        {
            _posPango = new Vector2(1026, 104);
            _posBat = new Vector2(655, 807);

            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("LePerso.sf", new JsonContentLoader());
            SpriteSheet spriteSheetPango = Content.Load<SpriteSheet>("pangolin.sf", new JsonContentLoader());
            SpriteSheet spriteSheetlabat = Content.Load<SpriteSheet>("labat.sf", new JsonContentLoader());
            SpriteSheet spriteSheetLife = Content.Load<SpriteSheet>("coeur.sf", new JsonContentLoader());
            SpriteSheet spriteSheetepee = Content.Load<SpriteSheet>("epee.sf", new JsonContentLoader());
            _pangolin = new Sprite(new AnimatedSprite(spriteSheetPango), _posPango);
            _bat = new Sprite(new AnimatedSprite(spriteSheetlabat), _posBat);
            _perso = new Sprite(new AnimatedSprite(spriteSheet), new Vector2(50, 700));
            _vie = new Sprite(new AnimatedSprite(spriteSheetLife), new Vector2(32, 700));
            _epeee = new Sprite(new AnimatedSprite(spriteSheetepee), new Vector2(32, 700));

            //charger la map
            _tiledMap = Content.Load<TiledMap>("LaMAP");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");

            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            rectZonePango = new Rectangle(512, 32, 990, 95);
            rectPorte = new Rectangle(1564, 632, 36, 96);
            rectZoneBat = new Rectangle(383, 768, 545, 95);

            if (_vie.InfoVie() <= 0 || _perso.Position().Y > 900)
                _myGame.LoadScreen2();

            if (_perso.RectPerso(_perso.Position()).Intersects(rectPorte))
            {
                _myGame.LoadScreen4();
                _myGame._currentScreen = Ecran.Jeu2;
            }

            _perso.UpdatePerso(gameTime, _mapLayer, _tiledMap);
            _vie.UpdateCoeur(gameTime, _perso.Position(), _bat.Position(), _pangolin.Position());
            _pangolin.UpdatePango(gameTime, _epeee.Position(), _perso.Position());
            _bat.UpdateBat(gameTime, _epeee.Position(), _perso.Position(), _mapLayer, _tiledMap);
            _epeee.UpdateEpee(gameTime, _perso.Position());

            _tiledMapRenderer.Update(gameTime);

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _epeee.Draw(_spriteBatch);
            _perso.Draw(_spriteBatch);
            _pangolin.Draw(_spriteBatch);
            _bat.Draw(_spriteBatch);
            _vie.Draw(_spriteBatch);
            _tiledMapRenderer.Draw();
            _spriteBatch.End();
        }
    }
}