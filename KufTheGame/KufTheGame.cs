using System.Collections.Generic;
using System.Linq;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Game.Models.Obsticles;
using KufTheGame.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KufTheGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class KufTheGame : Game
    {
        public const int ItemSize = 35;
        public const int FieldWidth = 1050;
        public const int FieldHeight = 400;
        public const int ScreenWidth = 1050;
        public const int ScreenHeight = 800;
        private const float FrameTime = 0.1f;

        private float time;
        private int frameIndex, attackFrames;
        private int backroundPart;
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle playerSpriteFrame, enemySpriteFrame;

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
            backroundPart = 0;
            this.Enemies = new List<Enemy>();
            Drops = new List<Item>();
            Player = new Player(100, 500, 57, 100, "Pesho");
            this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));

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
            if (Enemies.Count == 0) //Background is changing only if all enemies are killed
            {
                this.backroundPart = (this.backroundPart + 1) % 3;
                this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));
                this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));
                this.Enemies.Add(new StickmanNinja(1000, 500, 57, 100, 10, 10, 100));
            }

            //Check for Intersection between Player and Enemies
            foreach (var enemy in this.Enemies) 
            {
                Player.Intersect(enemy);
            }

            //Enemy attacks when Player is in range
            foreach (var enemyAttack in from enemy in this.Enemies where enemy.InAttackRange(Player) select enemy.Attack())
            {
                Player.RespondToAttack(enemyAttack);
            }

            //Check for Intersection between Player and Objects
            foreach (var obj in Objects)
            {
                Player.Intersect(obj);
            }

            Player.Move();
            Player.ResetDirections();
            var attack = Player.Attack();

            if (attack != null)
            {
                for (var i = 0; i < this.Enemies.Count; i++)
                {
                    var enemy = this.Enemies[i];
                    if (!Player.InAttackRange(enemy)) continue;

                    enemy.RespondToAttack(attack);
                    //using (var writer = new StreamWriter("../../../result.txt"))
                    //{
                    //    writer.WriteLine(enemy.HealthPoints);
                    //}

                    if (enemy.IsAlive()) continue;

                    enemy.AddDrops();
                    foreach (var drop in enemy.Drops)
                    {
                        drop.Drop();
                    }

                    this.Enemies.Remove(enemy);
                }
            }

            //Collecting dropped item
            for (var i = 0; i < Drops.Count; i++)
            {
                if (!Drops[i].Contains(Player)) continue;
                var item = Drops[i];
                //using (var writer = new StreamWriter("../../../result.txt"))
                //{
                //    writer.WriteLine(item.ToString());
                //}

                item.Use(Player);
                Drops.Remove(item);
            }

            if (this.Enemies.Count > 0)
            {
                var movingEnemy = this.Enemies[0];
                movingEnemy.Intersect(Player);
                this.Enemies[0].Move();
                this.Enemies[0].ResetDirections();
            }

            time += (float)gameTime.ElapsedGameTime.TotalSeconds; //Adding Elapsed Time From Last Loop
            while (time > FrameTime) //Checks is the Elapsed time higher than 0.1 seconds
            {
                frameIndex += 1;
                if (attackFrames > 0) //Checks is the Attack Animation is Loaded
                {
                    attackFrames--;

                    if (attackFrames == 0) //If the Attack Animaniton is over return Player to Idle State
                    {
                        Player.State = State.Idle;
                    }
                }

                time = 0F; // Reset Elapsed Time
            }

            frameIndex %= int.MaxValue; //Ensure That frameIndex will not overflow

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
            }

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

                playerSpriteFrame = new Rectangle((frameIndex % animationFrames) * 80, 140 * (int)Player.State, 80, 140);
            }
            else
            {
                playerSpriteFrame = new Rectangle(frameIndex % ((Player.State == State.Idle) ? (int)Frames.Idle : (int)Frames.Moving) * 80, 140 * (int)Player.State, 80, 140);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            const int stashItemSize = 35, stashSize = 5;
            var characterCenter = new Vector2(40, 70); //Characters' sprite center coordinates
            var pen = new Texture2D(this.graphics.GraphicsDevice, 1, 1); //Drawing pen for Game HUD
            pen.SetData(new[] { Color.White });

            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            /* ------------- Drawing Background -------------*/
            this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Background_BackgroundTexture), new Rectangle((this.backroundPart * (-1000)), 0, 3072, 800), Color.White);

            /* ------------- Drawing Players' HUD Lives -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(0, 0, 220, 120), new Color(Color.Black, 0.8F));

            this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "KUF THE WARRIOR", new Vector2(5, 5), Color.Red);
            this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Lives:  " + Player.Lives, new Vector2(5, 25), Color.White);

            /* ------------- Drawing Players' HUD Background -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200, 20), Color.White);

            /* ------------- Drawing Players' HUD HealthBar -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 50, 200 - (int)(Player.BaseHealthPoints - (int)Player.HealthPoints) * 4, 20), Color.Red);

            /* ------------- Drawing Players' HUD Stash Background -------------*/
            this.spriteBatch.Draw(pen, new Rectangle(5, 73, 200, 40), Color.Yellow);

            /* ------------- Drawing Players' HUD Stash Pockets -------------*/
            for (var stashPocket = 0; stashPocket < stashSize; stashPocket++)
            {
                this.spriteBatch.Draw(pen, new Rectangle(7 + (stashPocket * stashItemSize) + (stashPocket * 5), 75, stashItemSize, stashItemSize), Color.Black);
            }

            /* ------------- Drawing Players' Weapon -------------*/          
            if (Player.Weapon != null)
            {
                this.spriteBatch.Draw(Content.Load<Texture2D>(Player.Weapon.GetTexturePath()), new Rectangle(7, 75, stashItemSize, stashItemSize), Color.White);
            }

            /* ------------- Drawing Players' Armor Items -------------*/  
            if (Player.ArmorSet.Count != 0)
            {
                for (var itemNumber = 0; itemNumber < Player.ArmorSet.Count; itemNumber++)
                {
                    this.spriteBatch.Draw(Content.Load<Texture2D>(Player.ArmorSet[itemNumber].GetTexturePath()),
                        new Rectangle(47 + (itemNumber * stashItemSize) + (itemNumber * 5), 75, stashItemSize, stashItemSize), Color.White);
                }
            }

            /* ------------- Drawing Enemies -------------*/  
            foreach (var enemy in this.Enemies)
            {
                spriteBatch.Draw(Content.Load<Texture2D>(enemy.GetTexturePath()), enemy.Velocity, playerSpriteFrame, Color.White,
                0.0f, characterCenter, 1.5f, (enemy.SpriteRotation != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
            }

            /* ------------- Drawing Dropped Items -------------*/  
            foreach (var drop in Drops)
            {
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(drop.GetTexturePath()), new Rectangle((int)drop.Velocity.X, (int)drop.Velocity.Y, 50, 50), Color.White);
            }

            /* ------------- Drawing Players -------------*/  
            spriteBatch.Draw(this.Content.Load<Texture2D>(Player.GetTexturePath()), Player.Velocity, playerSpriteFrame, Color.White,
                0.0f, characterCenter, 1.5f, (Player.SpriteRotation == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
