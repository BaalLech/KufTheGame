using System;
using System.Collections.Generic;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models.Characters;
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
        private Texture2D background, weapon;
        private float time, frameTime = 0.1f, frameIndex;
        private int timer;
        private int backroundPart;
        private Player player;
        private List<Enemy> enemies;
        private SpriteFont gameFont;
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


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
            this.enemies = new List<Enemy>();
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
            this.player = new Player(Content.Load<Texture2D>("Characters/Players/PlayerSprite"), 100, 750, "Pesho");
            this.enemies.Add(new Mage(800, 500, 10, 10, 100));
            this.enemies.Add(new Mage(880, 700, 10, 10, 100));
            this.enemies.Add(new Mage(800, 640, 10, 10, 100));
            this.enemies.Add(new Mage(880, 900, 10, 10, 100));
            this.enemies.Add(new Mage(800, 550, 10, 10, 100));
            this.enemies.Add(new Mage(880, 600, 10, 10, 100));
            this.enemies.Add(new Mage(900, 800, 10, 10, 100));

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
            this.weapon = this.Content.Load<Texture2D>("Items/Weapons/Sword");
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
            if (this.player.HealthPoints >= 1)
            {
                this.player.HealthPoints -= 1;
            }
            else if (this.player.Lives > 1)
            {
                this.player.RemoveLive();
                this.player.HealthPoints = 50;
            }
            else
            {
                this.player.Lives = 0;
            }

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
            //    this.player.X += 1;
            //}

            player.Move();

            #endregion

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                // Play the next frame in the SpriteSheet
                frameIndex += 1;

                // reset elapsed time
                time = 0f;
            }

            frameIndex %= 9;
            

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
            this.spriteBatch.DrawString(this.gameFont, "Lives:  " + this.player.Lives, new Vector2(5, 25), Color.White);

            /* ------------- Drawing Players' HUD Background -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200, 20), Color.White);

            /* ------------- Drawing Players' HUD HealthBar -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200 - (int)(this.player.BaseHealthPoints - (int)this.player.HealthPoints) * 4, 20), Color.Red);

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

            foreach (var enemy in this.enemies)
            {
                this.spriteBatch.Draw(pen, new Rectangle((int)enemy.Velocity.X, (int)enemy.Velocity.Y, 50, 50), Color.Red);
            }

            Rectangle source = new Rectangle((int)((int)frameIndex * 54), 0, 57, 100);
            Vector2 origin = new Vector2(29 , 50);
            spriteBatch.Draw(player.Texture, this.player.Velocity, source, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
