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
        private Player player;
        private SpriteFont gameFont;
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        public KufTheGame()
        {
            //Creating Game Screen And Setting Initial Size
            this.graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1600,
                PreferredBackBufferHeight = 900,
                IsFullScreen = true
            };
            //this.graphics.ApplyChanges();

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
            this.player = new Player(100, 100, "Pesho", 3, 2, 0, 50);
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
            //I did this just to test how HP bar is going to look like /it's crap (puke)/
            if (this.player.HealthPoints >= 1)
            {
                this.player.HealthPoints -= 1;
            }
            else if (this.player.Lives > 1)
            {
                this.player.Lives -= 1;
                this.player.HealthPoints = 50;
            }
            else
            {
                this.player.Lives = 0;
            }

            //TODO Validation for character location
            #region //* ------------- MOVING CHARACTER ------------- *//
           
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
               // Move Up
                this.player.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
               // Move Down
                this.player.Y += 1;
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
               // Move left
                this.player.X -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
               // Move right
                this.player.X += 1;
            }

            #endregion
            
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

            #region //* ------------- CREATING DRAWING PEN ------------- *//

            var pen = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
            pen.SetData(new[] { Color.White });

            #endregion

            #region //* ------------- DRAWING CHARACTER INFO ------------- *//

            /* ------------- Drawing Players' HUD Lives -------------*/  
            this.spriteBatch.DrawString(this.gameFont, "Lives:  " + this.player.Lives, new Vector2(5, 5), Color.White);

            /* ------------- Drawing Players' HUD Background -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 30, 200, 20), Color.White);

            /* ------------- Drawing Players' HUD HealthBar -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 30, 200 - (50 - (int)this.player.HealthPoints) * 4, 20), Color.Red);
            #endregion

            #region //* ------------- DRAWING CHARACTER ------------- *//
            this.spriteBatch.Draw(pen, new Rectangle(this.player.X, this.player.Y, 20, 20), Color.Chocolate);
            #endregion

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
