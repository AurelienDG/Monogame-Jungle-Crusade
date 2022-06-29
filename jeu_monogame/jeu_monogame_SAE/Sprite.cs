using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;

namespace jeu_monogame_SAE
{
    public class Sprite
    {
        const int RANGE_PANGO = 440;
        const int RANGE_BAT = 246;
        const double DUREE_VIE = 1.5;           

        private AnimatedSprite _spritePerso;
        private Vector2 _spritePosition;                //position du sprite
        private Vector2 _velocity = new Vector2(2, 0);  //vitesse de déplacement (perso et monstres)
        private string animSprite;
        private string _animSpriteperso = "right";
        private bool _hasJumped;                        //pour savoir si le perso a sauté
        //vie
        private double _chronoLife = 1.5;        
        private int _persoLife = 3;                     //initialisation vie

        private int _deplacement;                       //afin de récupérer la position de départ d'un monstre
        private bool _touchePango = false;              //si l'epee a touché le Pango (si il est mort)
        private bool _toucheBat = false;                //si l'epee a touché la Pango (si elle est morte)

        public static Rectangle rectPango;
        public static Rectangle rectBat;
        public static Rectangle rectPerso;
        public static Rectangle rectEpee;
        private Rectangle _rectanglePangoJeu;
        private Rectangle _rectangleBatJeu;

        public Sprite(AnimatedSprite spritePerso, Vector2 spritePosition)
        {
            this.SpritePerso = spritePerso;
            this.SpritePosition = spritePosition;
        }

        public AnimatedSprite SpritePerso
        {
            get
            {
                return this._spritePerso;
            }

            set
            {
                this._spritePerso = value;
            }
        }

        public Vector2 SpritePosition
        {
            get
            {
                return this._spritePosition;
            }

            set
            {
                this._spritePosition = value;
            }
        }

        public Vector2 Position()
        {
            return SpritePosition;
        }

        public void UpdatePango(GameTime gameTime, Vector2 posEpee, Vector2 posPerso)
        {
            MouseState mouseState = Mouse.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Game1.niveau == 1)
            {
                _deplacement = (int)Jeu._posPango.X;//position de départ du pango
                _rectanglePangoJeu = Jeu.rectZonePango;
            }
            else if (Game1.niveau == 2)
            {
                _deplacement = (int)Jeu2._posPango.X;
                _rectanglePangoJeu = Jeu2.rectZonePango;
            }

            SpritePosition += _velocity;

            //si le pangolin n'as pas été touché par l'epee
            if (!_touchePango)
            {
                //rectangle collision pangolin
                rectPango = new Rectangle((int)SpritePosition.X - 75, (int)SpritePosition.Y - 10, 110, 35);

                // IA si le perso rentre dans la zone du pangolin, et que le pangolin n'est pas en bord de zone
                if (RectPerso(posPerso).Intersects(_rectanglePangoJeu) && SpritePosition.X != _deplacement - RANGE_PANGO && SpritePosition.X != _deplacement + RANGE_PANGO)
                {
                    //si pangolin à gauche du perso, il se dirige vers la droite 
                    if (SpritePosition.X < posPerso.X)
                    {
                        _velocity.X = 2;
                    }
                    //sinon il se déplace vers la gauche
                    else
                        _velocity.X = -2;
                }
                //si le perso n'est pas dans la zone, le pangolin fait des allers retours
                else if (SpritePosition.X == _deplacement - RANGE_PANGO || SpritePosition.X == _deplacement + RANGE_PANGO)
                {
                    _velocity = -_velocity;
                }
                //gestion des animations en fonction de la valeur velocity
                if (_velocity.X < 0)
                    animSprite = "left";
                else
                    animSprite = "right";
            }
            //gestion de l'epee
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (RectEpee(posEpee).Intersects(rectPango))
                {
                    _velocity.X = 0;
                    animSprite = "death";
                    rectPango = new Rectangle(0, -50, 110, 35);//on fait sortir le rectangle de collision de la map pour ne plus prendre de dégats
                    _touchePango = true;
                }
            }

            SpritePerso.Play(animSprite);
            SpritePerso.Update(deltaSeconds);
        }
        public void UpdateBat(GameTime gameTime, Vector2 posEpee, Vector2 posPerso, TiledMapTileLayer _mapLayer, TiledMap _tiledMap)
        {

            MouseState mouseState = Mouse.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Game1.niveau == 1)
            {
                _deplacement = (int)Jeu._posBat.X; //position de départ de la chauve souris
                _rectangleBatJeu = Jeu.rectZoneBat;
            }
            else if (Game1.niveau == 2)
            {
                _deplacement = (int)Jeu2._posBat.X;
                _rectangleBatJeu = Jeu2.rectZoneBat;
            }

            SpritePosition += _velocity;
            //si la bat n'as pas été touché par l'epee
            if (!_toucheBat)
            {
                //rectangle collision bat
                rectBat = new Rectangle((int)SpritePosition.X - 25, (int)SpritePosition.Y - 30, 50, 52);

                // IA si le perso rentre dans la zone de la bat, et que la bat n'est pas en bord de zone
                if (RectPerso(posPerso).Intersects(_rectangleBatJeu) && SpritePosition.X != _deplacement - RANGE_BAT && SpritePosition.X != _deplacement + RANGE_BAT)
                {
                    //si pangolin à gauche du perso, elle se dirige vers la droite
                    if (SpritePosition.X < posPerso.X)
                    {
                        _velocity.X = 2;
                    }
                    //sinon elle se déplace vers la gauche
                    else
                        _velocity.X = -2;
                }
                //si le perso n'est pas dans la zone, la bat fait des allers retours
                else if (SpritePosition.X == _deplacement - RANGE_BAT || SpritePosition.X == _deplacement + RANGE_BAT)
                {
                    _velocity = -_velocity;
                }
                //gestion des animations en fonction de la valeur velocity
                if (_velocity.X < 0)
                    animSprite = "left";
                else
                    animSprite = "right";
            }
            //gestion de l'epee
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (RectEpee(posEpee).Intersects(rectBat))
                {
                    _velocity.X = 0;
                    animSprite = "death";
                    rectBat = new Rectangle(0, -50, 110, 35);//on fait sortir le rectangle de collision de la map pour ne plus prendre de dégats
                    _toucheBat = true;
                }
            }
            //gestion de la chute de la bat quand elle est morte
            ushort tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight + 0.1);
            if (animSprite == "death")
                _velocity.Y = 5;
            if(IsCollision(tx, ty, _mapLayer))
                _velocity.Y = 0;

            SpritePerso.Play(animSprite);
            SpritePerso.Update(deltaSeconds);
        }
        public void UpdatePerso(GameTime gameTime, TiledMapTileLayer _mapLayer, TiledMap _tiledMap)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            string animation = "idle" + _animSpriteperso;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            //                                                                              ***Gestion Déplacements***
            ushort tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth - 0.1);
            ushort ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight + 1.2);
            ushort ty2 = (ushort)(SpritePosition.Y / _tiledMap.TileHeight + 1);

            float i = 1;

            if (!IsCollision(tx, ty, _mapLayer))
            {
                _velocity.Y += 0.15f * i;
            }
            else
                _velocity.Y = 0f;

            SpritePosition += _velocity;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _animSpriteperso = "right";
                animation = "run" + _animSpriteperso;

                tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth + 0.5);
                ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _mapLayer) && !IsCollision(tx, ty2, _mapLayer))
                {
                    _velocity.X = 3f;
                }
                else
                    _velocity.X = 0f;

            }

            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                _animSpriteperso = "left";
                animation = "run" + _animSpriteperso;

                tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth - 0.7);
                ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _mapLayer) && !IsCollision(tx, ty2, _mapLayer))
                {
                    _velocity.X = -3f;
                }
                else
                    _velocity.X = 0f;
            }
            else
                _velocity.X = 0;

            tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth - 0.1);
            ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight - 1);
            //sol
            ushort tx4 = (ushort)(SpritePosition.X / _tiledMap.TileWidth - 0.1);
            ushort ty4 = (ushort)(SpritePosition.Y / _tiledMap.TileHeight + 1.2);
            if (IsCollision(tx, ty, _mapLayer) && !IsCollision(tx4, ty4, _mapLayer))
            {
                _velocity.Y = 0;
                _spritePosition.Y += 1f;
                _hasJumped = true;

            }
            //                                                                              ***Gestion du Saut***
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                animation = "jump" + _animSpriteperso;
                if (_hasJumped == false)
                {
                    _spritePosition.Y -= 10f;
                    _velocity.Y = -6f;  //reglage hauteur du saut
                    _hasJumped = true;
                }

            }

            tx = (ushort)(SpritePosition.X / _tiledMap.TileWidth - 0.1);
            ty = (ushort)(SpritePosition.Y / _tiledMap.TileHeight + 1.2);

            if (_hasJumped == true || !IsCollision(tx, ty, _mapLayer))
            {
                _velocity.Y += 0.15f * i;
            }

            if (IsCollision(tx, ty, _mapLayer))
                _hasJumped = false;



            SpritePerso.Play(animation);
            SpritePerso.Update(deltaSeconds);

        }
        public void UpdateEpee(GameTime gameTime, Vector2 pos)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            SpritePosition = pos;
            animSprite = "void";
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                animSprite = "attack";

            }
            SpritePerso.Play(animSprite);
            SpritePerso.Update(deltaSeconds);


        }
        public void UpdateCoeur(GameTime gameTime, Vector2 posPerso, Vector2 posBat, Vector2 posPango)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            SpritePosition = new Vector2(posPerso.X, posPerso.Y - 40);

            if (RectPerso(posPerso).Intersects(rectPango) || RectPerso(posPerso).Intersects(rectBat))
            {
                if (_chronoLife == DUREE_VIE)
                {
                    _persoLife -= 1;
                }
                _chronoLife -= deltaSeconds;
                if (_chronoLife <= 0)
                {
                    _persoLife -= 1;
                    _chronoLife = DUREE_VIE - 0.001; //pour qu'il ne reparde pas une vie tout de suite à cause de la conddition au dessus
                }

            }
            else
                _chronoLife = DUREE_VIE;

            animSprite = _persoLife.ToString();

            SpritePerso.Play(animSprite);
            SpritePerso.Update(deltaSeconds);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpritePerso, SpritePosition);


        }

        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer _mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (_mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public int InfoVie()
        {
            return _persoLife;
        }
        public Rectangle RectPerso(Vector2 pos)
        {
            Rectangle rectPerso = new Rectangle((int)pos.X - 25, (int)pos.Y - 35, 46, 72);
            return rectPerso;
        }
        public Rectangle Rectpango(Vector2 pos)
        {
            Rectangle rectpango;

            rectpango = new Rectangle((int)pos.X - 75, (int)pos.Y - 10, 110, 35);


            return rectpango;

        }

        public Rectangle Rectbat(Vector2 pos)
        {
            Rectangle rectBat = new Rectangle((int)pos.X - 25, (int)pos.Y - 30, 50, 52);
            return rectBat;
        }
        public Rectangle RectEpee(Vector2 pos)
        {
            Rectangle rectEpee = new Rectangle((int)pos.X - 65, (int)pos.Y - 65, 130, 130);
            return rectEpee;
        }
    }
}