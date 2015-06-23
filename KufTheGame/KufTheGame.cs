using System;
using System.Linq;
using System.Collections.Generic;

using KufTheGame.Core;
using KufTheGame.Properties;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Game.Models.Obsticles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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
        public const int ScreenHeight = 850;

        private bool lavelChanged, isPlaying;
        private int backroundPart, barSize, slider, fadeInCounter, fadeOutCounter;
        private Song song;
        private SoundEffect kickSound;
        private SoundEffect dropSound;
        private int counter;
        private FrameHandler frameHandler;
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
            this.backroundPart = 0;
            this.Enemies = new List<Enemy>();
            Drops = new List<Item>();
            Player = new Player(150, 500, 57, 100, "Pesho");
            this.Enemies.Add(new Karateman(1100, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new StickmanNinja(1100, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new StickmanNinja(1100, 500, 57, 100, 10, 10, 100));
            this.Enemies.Add(new Karateman(1100, 500, 57, 100, 10, 10, 100));

            barSize = (int)Math.Floor(167F / Enemies.Count) - 2;
            this.Objects = new List<Obsticle>
            {
                new Boundary(0, ScreenHeight - FieldHeight, FieldWidth, 10),
                new Boundary(ScreenWidth, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight, FieldWidth, 10)
            };

            slider = 0;
            fadeInCounter = 0;
            fadeOutCounter = 255;

            lavelChanged = false;

            frameHandler = new FrameHandler();

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

            //Load Sounds
            //TODO Fix Exception
            //kickSound = Content.Load<SoundEffect>(Resources.Sound_PlayerKickSound);
            //dropSound = Content.Load<SoundEffect>("Sounds/DropSound");
            // Load background music
            song = Content.Load<Song>(Resources.Sound_GameLoopSound);
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
            if (isPlaying)
            {
                if (Enemies.Count == 0) //Background is changing only if all enemies are killed
                {
                    if (!lavelChanged)
                    {
                        this.backroundPart = (this.backroundPart + 1) % 3;
                        lavelChanged = true;
                    }


                    if (slider < 1000 * backroundPart)
                    {
                        slider = (int)(slider + 1.5F);
                        Player.Velocity = (Player.Velocity.X > 100)
                            ? new Vector2((Player.Velocity.X - 1), Player.Velocity.Y)
                            : new Vector2((Player.Velocity.X + 1), Player.Velocity.Y);
                        Player.Velocity = (Player.Velocity.Y > 500)
                            ? new Vector2(Player.Velocity.X, (Player.Velocity.Y - 1))
                            : new Vector2(Player.Velocity.X, (Player.Velocity.Y + 1));
                    }
                    else
                    {
                        Drops.Clear();
                        lavelChanged = false;

                        this.Enemies.Add(new StickmanNinja(1100, 500, 57, 100, 10, 10, 100));
                        this.Enemies.Add(new StickmanNinja(1100, 500, 57, 100, 10, 10, 100));
                        this.Enemies.Add(new StickmanNinja(1100, 500, 57, 100, 10, 10, 100));

                        barSize = 167 / Enemies.Count - Enemies.Count * 2;
                    }
                }

                if (this.Enemies.Count > 0)
                {
                    var movingEnemy = this.Enemies[0];
                    movingEnemy.Intersect(Player);
                    this.Enemies[0].Move();
                    this.Enemies[0].ResetDirections();
                }

                //Check for Intersection between Player and Enemies
                foreach (var enemy in this.Enemies)
                {
                    Player.Intersect(enemy);
                }

                //Enemy attacks when Player is in range
                foreach (
                    var enemyAttack in
                        from enemy in this.Enemies where enemy.InAttackRange(Player) select enemy.Attack())
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
                    if (counter < (int)Frames.YodaStrikePunch)
                    {
                        counter++;
                    }
                    if (counter == 1)
                    {
                        //kickSound.Play();
                    }
                    if (counter == (int)Frames.YodaStrikePunch)
                    {
                        counter = 0;
                    }

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
                            //dropSound.Play();
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

                if ((frameHandler.PlayerAttackFrames == 1) &&
                    (Player.State == State.YodaStrikePunch || Player.State == State.TeethOfTigerThrow ||
                     Player.State == State.WingedHorseKick))
                {
                    frameHandler.PlayerAttackFrames = 0;
                    Player.State = State.Idle;
                }

                foreach (var enemy in Enemies)
                {
                    if ((frameHandler.EnemyAttackFrames == 1) &&
                        (enemy.State == State.YodaStrikePunch || enemy.State == State.TeethOfTigerThrow ||
                         enemy.State == State.WingedHorseKick))
                    {
                        frameHandler.EnemyAttackFrames = 0;
                        enemy.State = State.Moving;
                    }
                }
            }
            else
            {
                if (frameHandler.FrameIndex % 2 == 0)
                {
                    fadeInCounter++;
                    fadeOutCounter = (fadeInCounter != 600) ? fadeOutCounter - 1 : 255;

                    if (fadeInCounter == 855)
                    {
                        isPlaying = true;
                        MediaPlayer.Play(song);
                        MediaPlayer.IsRepeating = true;
                    }
                }
            }

            frameHandler.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var characterCenter = new Vector2(40, 70); //Characters' sprite center coordinates
            var pen = new Texture2D(this.graphics.GraphicsDevice, 1, 1); //Drawing pen for Game HUD
            pen.SetData(new[] { Color.White });

            this.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();

            if (isPlaying)
            {
                /* ------------- Drawing Background -------------*/
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Background_BackgroundTexture), new Rectangle(((-1) * slider), 0, 3072, 800), Color.White);

                //Drawing HUD Background
                this.spriteBatch.Draw(pen, new Rectangle(0, 0, ScreenWidth, 100), new Color(Color.Black, 0.7F));

                //Drawing Dragon HUD
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_DragonTexture), new Rectangle(50, 10, 100, 80), Color.White);

                //Drawing Score 
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Score: ", new Vector2(200, 10), Color.Coral);
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "00000000", new Vector2(300, 10), Color.Coral);

                //Drawing GameTime
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_ClockTexture), new Rectangle(405, 15, 15, 15), Color.White);
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), string.Format("{0:0000}", (int)gameTime.TotalGameTime.TotalSeconds), new Vector2(425, 10), Color.Coral);

                //Drawing HealthBar
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Health: ", new Vector2(200, 40), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(300, 40, 167, 20), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(305, 42, 157, 16), Color.Black);

                for (int bars = 0; bars < Player.HealthPoints / 4; bars++)
                {
                    var color = (Player.HealthPoints >= Player.BaseHealthPoints * 0.7)
                        ? Color.Green
                        : ((Player.HealthPoints >= Player.BaseHealthPoints * 0.3) ? Color.Yellow : Color.Red);
                    this.spriteBatch.Draw(pen, new Rectangle(307 + (bars * 10) + (bars * 2), 44, 10, 12), color);
                }


                //Drawing Enemies Count Bar
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Enemies: ", new Vector2(200, 70), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(300, 70, 167, 20), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(305, 72, 157, 16), Color.Black);

                for (var enemy = 0; enemy < Enemies.Count; enemy++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(307 + (enemy * barSize) + (enemy * 2), 74, barSize, 12), Color.Coral);
                }

                //Drawing Lives Left
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_HeartTexture), new Rectangle(475, 73, 20, 20), Color.White);
                this.spriteBatch.Draw(pen, new Rectangle(475, 10, 20, 60), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(477, 12, 16, 56), Color.Black);

                var playerLives = (Player.Lives > 3) ? Player.Lives : 3;
                var livesBarHeight = (60 / playerLives) - 4;
                for (var lives = 0; lives < Player.Lives; lives++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(479, (66 - livesBarHeight - (livesBarHeight * lives) - (lives * 2)), 12, livesBarHeight), Color.Red);
                }


                //Drawing Stash Pockets
                for (var item = 0; item < 5; item++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(500 + (item * 65), 10, 60, 60), Color.Coral);
                    this.spriteBatch.Draw(pen, new Rectangle(502 + (item * 65), 12, 56, 56), Color.Black);
                }

                //Drawing Players Weapon
                if (Player.Weapon != null)
                {
                    var color = (Player.Weapon.Rarity == Rarities.Common)
                           ? Color.White
                           : ((Player.Weapon.Rarity == Rarities.Magic)
                               ? Color.AliceBlue
                               : ((Player.Weapon.Rarity == Rarities.Rare)
                                   ? Color.Yellow
                                   : ((Player.Weapon.Rarity == Rarities.Epic) ? Color.Green : Color.Pink)));

                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(Player.Weapon.GetTexturePath()), new Rectangle(502, 12, 56, 56), Color.White);
                    this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "   + " + Player.Weapon.AttackPoints, new Vector2(505, 70), color);
                }

                //Drawing Players Armor
                foreach (var armor in Player.ArmorSet)
                {
                    var position = new Vector2();
                    switch (armor.ArmorType)
                    {
                        case Armors.Helmet: position = new Vector2(570, 70); break;
                        case Armors.Chest: position = new Vector2(635, 70); break;
                        case Armors.Gloves: position = new Vector2(700, 70); break;
                        case Armors.Boots: position = new Vector2(765, 70); break;
                    }

                    var color = (armor.Rarity == Rarities.Common)
                        ? Color.White
                        : ((armor.Rarity == Rarities.Magic)
                            ? Color.AliceBlue
                            : ((armor.Rarity == Rarities.Rare)
                                ? Color.Yellow
                                : ((armor.Rarity == Rarities.Epic) ? Color.Green : Color.Pink)));

                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(armor.GetTexturePath()), new Rectangle((int)position.X - 2, 12, 54, 54), Color.White);
                    this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), string.Format(" +{0:F1}", armor.DefencePoints), position, color);
                }

                //Drawing Plaeyer StatInfo
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont),
                    string.Format("Damage: {0}", (Player.Weapon != null) ? Player.Weapon.AttackPoints + Player.AttackPoints : Player.AttackPoints),
                    new Vector2(830, 5),
                    Color.Coral
                );

                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont),
                    string.Format("Armor:    {0:F1}", Player.GetTotalArmor()),
                    new Vector2(830, 30),
                    Color.Coral
                );

                if (Player.ImmortalDuration > 0)
                {
                    if (frameHandler.FrameIndex % 2 == 0)
                    {
                        this.spriteBatch.Draw(pen, new Rectangle(830, 60, 150, 25), Color.Green);
                        this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Immune", new Vector2(865, 60), Color.White);
                    }
                    else
                    {
                        this.spriteBatch.Draw(pen, new Rectangle(830, 60, 150, 25), Color.White);
                        this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Immune", new Vector2(865, 60), Color.Green);
                    }
                }

                //Drawing HUD Frame
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_BorderTexture), new Rectangle(0, 5, 1000, 100), Color.White);

                /* ------------- Drawing Dropped Items -------------*/
                foreach (var drop in Drops)
                {
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(drop.GetTexturePath()), new Rectangle((int)drop.Velocity.X, (int)drop.Velocity.Y, 50, 50), Color.White);
                }

                /* ------------- Drawing Enemies -------------*/
                foreach (var enemy in this.Enemies)
                {
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(enemy.GetTexturePath()), enemy.Velocity, frameHandler.GetSpriteFrame(enemy), Color.White,
                    0.0f, characterCenter, 1.5f, (enemy.SpriteRotation != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
                }

                /* ------------- Drawing Player -------------*/
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Player.GetTexturePath()), Player.Velocity, frameHandler.GetSpriteFrame(Player), Color.White,
                    0.0f, characterCenter, 1.5f, (Player.SpriteRotation == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
            }
            else
            {
                if (Player.IsAlive())
                {
                    var newColor = ((fadeInCounter - 255 < 255) && (fadeInCounter < 600))
                        ? fadeInCounter - 255
                        : ((fadeInCounter < 600) ? fadeInCounter - 255 : fadeOutCounter);

                    if (fadeInCounter < 255)
                    {
                        this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Screen_CompanySplashTexture),
                            new Vector2(150, 335),
                            frameHandler.GetSplashScreenFrame(),
                            new Color(fadeOutCounter, fadeOutCounter, fadeOutCounter, fadeOutCounter));
                    }
                    else
                    {
                        this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Screen_PrePlayTexture),
                            new Vector2(0, 0), new Color(newColor, newColor, newColor, newColor));

                        if ((fadeInCounter >= 345) && (fadeInCounter < 600))
                        {
                            var innerNewColor = newColor - 90;

                            this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Screen_GameLogoSplashTexture),
                                new Vector2(600, 200),
                                new Color(innerNewColor, innerNewColor, innerNewColor, innerNewColor));
                        }
                    }
                }
                else
                {
                    //TODO Score screen
                }
                
                
            }


            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
