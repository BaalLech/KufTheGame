using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Game.Models.Obsticles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KufTheGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class KufTheGame : Game
    {
        public const int ItemSize = 35;
        public const int FieldWidth = 1000;
        public const int FieldHeight = 450;
        public const int ScreenWidth = 1000;
        public const int ScreenHeight = 750;
        private const float FrameTime = 0.1f;

        private Texture2D background, weapon;
        private float time;
        private int timer, frameIndex, attackFrames;
        private int backroundPart;
        private SpriteFont gameFont;
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static List<Item> Drops { get; set; }

        public static Player Player { get; private set; }

        public List<Enemy> Enemies { get; set; }

        public List<Obsticle> Objects { get; set; }



        public KufTheGame()
        {
            //Creating Game Screen And Setting Initial Size
            this.graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1000,
                PreferredBackBufferHeight = 800,
                //IsFullScreen = true
            };

            this.timer = 500;
            //this.Enemies = new List<Enemy>();
            //this.background = new Texture2D(this.graphics.GraphicsDevice, 800, 800);

            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            timer = 500;
            backroundPart = 0;
            this.Enemies = new List<Enemy>();
            Drops = new List<Item>();
            Player = new Player(Content.Load<Texture2D>("Characters/Players/PlayerSprite"), 100, 500, 57, 100, "Pesho");
            this.Enemies.Add(new Mage(1000, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new Mage(1000, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new Mage(1000, 500, 57, 100, 10, 10, 100));
            //this.Enemies.Add(new Mage(200, 900, 150, 150, 10, 10, 100));
            //this.Enemies.Add(new Mage(200, 550, 150, 150, 10, 10, 100));
            //this.Enemies.Add(new Mage(200, 600, 150, 150, 10, 10, 100));
            //this.Enemies.Add(new Mage(200, 800, 150, 150, 10, 10, 100));

            this.Objects = new List<Obsticle>
            {
                new Boundary(0, ScreenHeight - FieldHeight, FieldWidth, 10),
                new Boundary(ScreenWidth, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight, FieldWidth, 10)
            };






            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.gameFont = this.Content.Load<SpriteFont>("Fonts/GameFont");
            this.background = this.Content.Load<Texture2D>("Backgrounds/background");
            this.weapon = this.Content.Load<Texture2D>("Items/Weapons/WeaponSword");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (this.timer != 0)
            {
                this.timer -= 1;
            }
            else
            {
                this.timer = 500;
                this.backroundPart = (this.backroundPart + 1) % 3;
            }

            //I did this just to test how HP bar is going to look like /it's crap (puke)/
            //if (Player.HealthPoints >= 1)
            //{
            //    Player.HealthPoints -= 1;
            //}
            //else if (Player.Lives > 1)
            //{
            //    Player.RemoveLive();
            //    Player.HealthPoints = 50;
            //}
            //else
            //{
            //    Player.Lives = 0;
            //}

            //TODO Validation for character location
            #region //* ------------- MOVING CHARACTER ------------- *//

            //var keyboardState = Keyboard.GetState();
            //if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            //{
            //    // Move Up
            //    this.player.Y -= 1;
            //}
            //if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            //{
            //    // Move Down
            //    this.player.Y += 1;
            //}
            //if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            //{
            //    // Move left
            //    this.player.X -= 1;
            //}
            //if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            //{
            //    // Move right
            //}
            //Player.Attack();


            foreach (var enemy in this.Enemies)
            {
                Player.Intersect(enemy);
            }

            foreach (var obj in Objects)
            {
                Player.Intersect(obj);
            }

            Player.Move();
            Player.ResetDirections();


            //this.Enemies.ForEach(e => e.Move());

            if (this.Enemies.Count > 0)
            {
                var movingEnemy = this.Enemies[0];
                movingEnemy.Intersect(Player);
                this.Enemies[0].Move();
                this.Enemies[0].ResetDirections();
            }




            #endregion

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > FrameTime)
            {
                // Play the next frame in the SpriteSheet
                frameIndex += 1;
                if (attackFrames > 0)
                {
                    attackFrames--;

                    if (attackFrames == 0)
                    {
                        Player.State = State.Idle;
                    }
                }
                // reset elapsed time
                time = 0F;
            }

            if (frameIndex == int.MaxValue)
            {
                frameIndex = 0;
            }

            if ((Player.State == State.YodaStrikePunch) || (Player.State == State.WingedHorseKick) || (Player.State == State.TeethOfTigerThrow) || (attackFrames > 0))
            {
                if ((attackFrames == 0) && ((Player.State != State.Idle) || (Player.State != State.Moving)))
                {
                    switch (Player.State)
                    {
                        case State.YodaStrikePunch: attackFrames = (int)Frames.YodaStrikePunch; break;
                        case State.WingedHorseKick: attackFrames = (int)Frames.WingedHorseKick; break;
                        case State.TeethOfTigerThrow: attackFrames = (int)Frames.TeethOfTigerThrow; break;
                        default : attackFrames = 0; break;
                    }
                }
                else if (((attackFrames == 0) && ((Player.State == State.Idle) || (Player.State == State.Moving))))
                {
                    Player.State = State.Idle;
                }
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();



            this.spriteBatch.Draw(this.background, new Rectangle((this.backroundPart * (-1000)), 0, 3072, 800), Color.White);

            #region //* ------------- CREATING DRAWING PEN ------------- *//

            var pen = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
            pen.SetData(new[] { Color.White });

            #endregion

            #region //* ------------- DRAWING CHARACTER INFO ------------- *//

            /* ------------- Drawing Players' HUD Lives -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(0, 0, 220, 120), new Color(Color.Black, 0.8F));

            this.spriteBatch.DrawString(this.gameFont, "KUF THE WARRIOR", new Vector2(5, 5), Color.Red);
            this.spriteBatch.DrawString(this.gameFont, "Lives:  " + Player.Lives, new Vector2(5, 25), Color.White);

            /* ------------- Drawing Players' HUD Background -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200, 20), Color.White);

            /* ------------- Drawing Players' HUD HealthBar -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200 - (int)(Player.BaseHealthPoints - (int)Player.HealthPoints) * 4, 20), Color.Red);

            /* ------------- Drawing Players' HUD Stash -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 73, 200, 40), Color.DarkGreen);

            this.spriteBatch.Draw(pen, new Rectangle(7, 75, 35, 35), Color.DarkBlue);
            this.spriteBatch.Draw(pen, new Rectangle(47, 75, 35, 35), Color.DarkBlue);
            this.spriteBatch.Draw(pen, new Rectangle(87, 75, 35, 35), Color.DarkBlue);
            this.spriteBatch.Draw(pen, new Rectangle(127, 75, 35, 35), Color.DarkBlue);
            this.spriteBatch.Draw(pen, new Rectangle(167, 75, 35, 35), Color.DarkBlue);

            /* ------------- Drawing Players' HUD Items -------------*/
            this.spriteBatch.Draw(this.weapon, new Rectangle(7, 75, 35, 35), Color.White);
            #endregion

            #region //* ------------- DRAWING CHARACTER ------------- *//
            //this.spriteBatch.Draw(pen, new Rectangle((int)this.player.Velocity.X, (int)this.player.Velocity.Y, 20, 20), Color.LightGreen);
            #endregion


            var attack = Player.Attack();
            if (attack != null)
            {
                this.spriteBatch.Draw(pen, new Rectangle(500, 100, 150, 150), Color.Red);
                for (int i = 0; i < this.Enemies.Count; i++)
                {
                    var enemy = this.Enemies[i];
                    if (Player.InAttackRange(enemy))
                    {
                        enemy.RespondToAttack(attack);
                        this.spriteBatch.Draw(pen, new Rectangle((int)enemy.Velocity.X - 500, (int)enemy.Velocity.Y, 150, 150), Color.Red);
                        //using (var writer = new StreamWriter("../../../result.txt"))
                        //{
                        //    writer.WriteLine(enemy.HealthPoints);
                        //}

                        if (!enemy.IsAlive())
                        {
                            enemy.AddDrops();
                            foreach (var drop in enemy.Drops)
                            {
                                drop.Drop();
                            }

                            this.Enemies.Remove(enemy);
                        }
                    }
                }
            }

            for (int i = 0; i < Drops.Count; i++)
            {
                if (Drops[i].Contains(Player))
                {
                    var item = Drops[i];
                    //using (var writer = new StreamWriter("../../../result.txt"))
                    //{
                    //    writer.WriteLine(item.ToString());
                    //}

                    item.Use(Player);
                    Drops.Remove(item);
                }

            }

            foreach (var enemy in this.Enemies)
            {
                if (enemy.InAttackRange(Player))
                {
                    var attack1 = enemy.Attack();
                    Player.RespondToAttack(attack1);
                }
            }


            //DRAWING ELEMENTS
            foreach (var enemy in this.Enemies)
            {
                this.spriteBatch.Draw(pen, new Rectangle((int)enemy.Velocity.X, (int)enemy.Velocity.Y, 57, 100),
                    Color.Red);
            }

            foreach (var drop in Drops)
            {
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(drop.GetTexturePath()), new Rectangle((int)drop.Velocity.X, (int)drop.Velocity.Y, 50, 50), Color.White);
            }

            Rectangle source;
            if ((attackFrames != 0) && ((Player.State == State.YodaStrikePunch) || (Player.State == State.WingedHorseKick) || (Player.State == State.TeethOfTigerThrow)))
            {
                int animationFrames;
                switch (Player.State)
                {
                    case State.YodaStrikePunch: animationFrames = (int)Frames.YodaStrikePunch; break;
                    case State.WingedHorseKick: animationFrames = (int)Frames.WingedHorseKick; break;
                    case State.TeethOfTigerThrow: animationFrames = (int)Frames.TeethOfTigerThrow; break;
                    default: animationFrames = 0; break;
                }

                source = new Rectangle((frameIndex % animationFrames) * 80, 140 * (int)Player.State, 80, 140);
            }
            else
            {
                source = new Rectangle(frameIndex % ((Player.State == State.Idle) ? (int)Frames.Idle : (int)Frames.Moving) * 80, 140 * (int)Player.State, 80, 140);
            }

            Vector2 origin = new Vector2(40, 70);
            spriteBatch.Draw(this.Content.Load<Texture2D>(Player.GetTexturePath()),
                Player.Velocity,
                source,
                Color.White,
                0.0f,
                origin,
                1.5f,
                (Player.SpriteRotation == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                1.0f
                );


            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
