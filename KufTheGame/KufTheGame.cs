using System;
using System.Collections.Generic;
using System.Linq;
using KufTheGame.Core;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Models.Game.Models.Obsticles;
using KufTheGame.Models.Structures;
using KufTheGame.Properties;
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
        public const int EnemyKillScore = 10;
        public const int EnemyStartX = 1100;
        public const int EnemyStartY = 500;
        public const int PlayerStartX = 150;
        public const int PlayerStartY = 500;
        public const int StandartWidth = 57;
        public const int StandartHeight = 100;
        public const int ScorePerKill = 10;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public KufTheGame()
        {
            // Creating Game Screen And Setting Initial Size
            this.graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1000,
                PreferredBackBufferHeight = 800,
                IsFullScreen = true
            };

            this.Content.RootDirectory = "Content";
        }

        public static List<Item> Drops { get; set; }

        public static Player Player { get; set; }

        private List<int> TopScores { get; set; }

        private List<Enemy> Enemies { get; set; }

        private List<Obsticle> Objects { get; set; }

        private FrameHandler FrameHandlerVariable { get; set; }

        private bool LevelChanged { get; set; }

        private bool IsPlaying { get; set; }

        private int BackgroundSection { get; set; }

        private int BarSize { get; set; }

        private int Slider { get; set; }

        private int FadeInCounter { get; set; }

        private int FadeOutCounter { get; set; }

        private Song BackgroundSong { get; set; }

        private SoundEffect KickSound { get; set; }

        private SoundEffect DropSound { get; set; }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.BackgroundSection = 0;
            this.Enemies = new List<Enemy>();
            Drops = new List<Item>();
            Player = new Player(PlayerStartX, PlayerStartY, StandartWidth, StandartHeight, "Pesho");

            this.Enemies = GameLevel.InitializeEnemies();

            this.BarSize = (int)Math.Floor(167.0 / this.Enemies.Count) - 2;
            this.Objects = new List<Obsticle>
            {
                new Boundary(0, ScreenHeight - FieldHeight, FieldWidth, 10),
                new Boundary(ScreenWidth, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight - FieldHeight, 10, FieldHeight),
                new Boundary(0, ScreenHeight, FieldWidth, 10)
            };

            this.Slider = 0;
            this.FadeInCounter = 0;
            this.FadeOutCounter = 255;

            //this.IsPlaying = true;

            this.IsPlaying = false;

            this.LevelChanged = false;

            this.FrameHandlerVariable = new FrameHandler();

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

            // Load Sounds
            this.KickSound = this.Content.Load<SoundEffect>(Resources.Sound_PlayerKickSound);
            this.DropSound = this.Content.Load<SoundEffect>(Resources.Sound_DropSound);

            // Load background music
            this.BackgroundSong = this.Content.Load<Song>(Resources.Sound_GameLoopSound);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Nothing to Unload
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (this.IsPlaying)
            {
                // Background is changing only if all enemies are killed
                if (this.Enemies.Count == 0)
                {
                    if (!this.LevelChanged)
                    {
                        this.BackgroundSection = (this.BackgroundSection + 1) % 3;
                        this.LevelChanged = true;
                    }

                    if (this.Slider < 1000 * this.BackgroundSection)
                    {
                        this.Slider = (int)(this.Slider + 1.5F);
                        Player.Velocity = (Player.Velocity.X > 100)
                            ? new Vector2(Player.Velocity.X - 1, Player.Velocity.Y)
                            : new Vector2(Player.Velocity.X + 1, Player.Velocity.Y);
                        Player.Velocity = (Player.Velocity.Y > 500)
                            ? new Vector2(Player.Velocity.X, Player.Velocity.Y - 1)
                            : new Vector2(Player.Velocity.X, Player.Velocity.Y + 1);
                    }
                    else
                    {
                        Drops.Clear();
                        this.LevelChanged = false;

                        this.Enemies = GameLevel.InitializeEnemies();

                        this.BarSize = (167 / this.Enemies.Count) - (this.Enemies.Count * 2);
                    }
                }

                if (this.Enemies.Count > 0)
                {
                    var movingEnemy = this.Enemies[0];
                    movingEnemy.Intersect(Player);
                    this.Enemies[0].Move();
                    this.Enemies[0].ResetDirections();
                }

                // Check for Intersection between Player and Enemies
                foreach (var enemy in this.Enemies)
                {
                    Player.Intersect(enemy);
                }

                // Enemy attacks when Player is in range
                foreach (var enemyAttack in from enemy in this.Enemies where enemy.InAttackRange(Player) select enemy.Attack())
                {
                    Player.RespondToAttack(enemyAttack);
                }

                // Check for Intersection between Player and Objects
                foreach (var obj in this.Objects)
                {
                    Player.Intersect(obj);
                }

                Player.Move();
                Player.ResetDirections();
                var attack = Player.Attack();

                if (attack != null)
                {
                    this.KickSound.Play();

                    for (var i = 0; i < this.Enemies.Count; i++)
                    {
                        var enemy = this.Enemies[i];
                        {
                            if (!Player.InAttackRange(enemy))
                            {
                                continue;
                            }
                        }

                        enemy.RespondToAttack(attack);

                        if (enemy.IsAlive())
                        {
                            continue;
                        }

                        enemy.AddDrops();
                        Scoreboard.AddScore(ScorePerKill);

                        foreach (var drop in enemy.Drops)
                        {
                            drop.Drop();
                            this.DropSound.Play();
                        }

                        this.Enemies.Remove(enemy);
                    }
                }

                // Collecting dropped item
                for (var i = 0; i < Drops.Count; i++)
                {
                    if (!Drops[i].Contains(Player))
                    {
                        continue;
                    }

                    var item = Drops[i];

                    item.Use(Player);
                    Drops.Remove(item);
                }

                if ((this.FrameHandlerVariable.PlayerAttackFrames == 1) &&
                    (Player.State == State.YodaStrikePunch || Player.State == State.TeethOfTigerThrow ||
                     Player.State == State.WingedHorseKick))
                {
                    this.FrameHandlerVariable.PlayerAttackFrames = 0;
                    Player.State = State.Idle;
                }

                foreach (var enemy in this.Enemies.Where(enemy => (this.FrameHandlerVariable.EnemyAttackFrames == 1) &&
                    (enemy.State == State.YodaStrikePunch || enemy.State == State.TeethOfTigerThrow || enemy.State == State.WingedHorseKick)))
                {
                    this.FrameHandlerVariable.EnemyAttackFrames = 0;
                    enemy.State = State.Moving;
                }
            }
            else
            {
                if (this.FrameHandlerVariable.FrameIndex % 2 == 0)
                {
                    this.FadeInCounter++;
                    this.FadeOutCounter = (this.FadeInCounter != 600) ? this.FadeOutCounter - 1 : 255;

                    if ((this.FadeInCounter == 855) && Player.IsAlive())
                    {
                        this.IsPlaying = true;
                        MediaPlayer.Play(this.BackgroundSong);
                        MediaPlayer.IsRepeating = true;
                    }
                }
            }

            if (this.IsPlaying && ((Player.Lives == 0) || (this.Enemies.Count == 0 && this.BackgroundSection == 3)))
            {
                MediaPlayer.Stop();

                this.IsPlaying = false;

                Scoreboard.AddNewToScoreboard(Scoreboard.Score);

                this.TopScores = Scoreboard.GetScoreboard();
            }

            this.FrameHandlerVariable.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Characters' sprite center coordinates
            var characterCenter = new Vector2(40, 70);

            // Drawing pen for Game HUD
            var pen = new Texture2D(this.graphics.GraphicsDevice, 1, 1);
            pen.SetData(new[] { Color.White });

            this.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();

            if (this.IsPlaying)
            {
                /* ------------- Drawing Background -------------*/
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Background_BackgroundTexture), new Rectangle(((-1) * this.Slider), 0, 3072, 800), Color.White);

                // Drawing HUD Background
                this.spriteBatch.Draw(pen, new Rectangle(0, 0, ScreenWidth, 100), new Color(Color.Black, 0.7F));

                // Drawing Dragon HUD
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_DragonTexture), new Rectangle(50, 10, 100, 80), Color.White);

                // Drawing Score 
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Score: ", new Vector2(200, 10), Color.Coral);
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), string.Format("{0:00000000}", Scoreboard.Score), new Vector2(300, 10), Color.Coral);

                // Drawing GameTime
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_ClockTexture), new Rectangle(405, 15, 15, 15), Color.White);
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), string.Format("{0:0000}", (int)gameTime.TotalGameTime.TotalSeconds - 30), new Vector2(425, 10), Color.Coral);

                // Drawing HealthBar
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Health: ", new Vector2(200, 40), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(300, 40, 167, 20), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(305, 42, 157, 16), Color.Black);

                for (var bars = 0; bars < Player.HealthPoints / 4; bars++)
                {
                    var color = (Player.HealthPoints >= Player.BaseHealthPoints * 0.7)
                        ? Color.Green
                        : ((Player.HealthPoints >= Player.BaseHealthPoints * 0.3) ? Color.Yellow : Color.Red);
                    this.spriteBatch.Draw(pen, new Rectangle(307 + (bars * 10) + (bars * 2), 44, 10, 12), color);
                }

                // Drawing Enemies Count Bar
                this.spriteBatch.DrawString(this.Content.Load<SpriteFont>(Resources.Font_GameFont), "Enemies: ", new Vector2(200, 70), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(300, 70, 167, 20), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(305, 72, 157, 16), Color.Black);

                for (var enemy = 0; enemy < this.Enemies.Count; enemy++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(307 + (enemy * this.BarSize) + (enemy * 2), 74, this.BarSize, 12), Color.Coral);
                }

                // Drawing Lives Left
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_HeartTexture), new Rectangle(475, 73, 20, 20), Color.White);
                this.spriteBatch.Draw(pen, new Rectangle(475, 10, 20, 60), Color.Coral);
                this.spriteBatch.Draw(pen, new Rectangle(477, 12, 16, 56), Color.Black);

                var playerLives = (Player.Lives > 3) ? Player.Lives : 3;
                var livesBarHeight = (60 / playerLives) - 4;
                for (var lives = 0; lives < Player.Lives; lives++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(479, (66 - livesBarHeight - (livesBarHeight * lives) - (lives * 2)), 12, livesBarHeight), Color.Red);
                }

                // Drawing Stash Pockets
                for (var item = 0; item < 5; item++)
                {
                    this.spriteBatch.Draw(pen, new Rectangle(500 + (item * 65), 10, 60, 60), Color.Coral);
                    this.spriteBatch.Draw(pen, new Rectangle(502 + (item * 65), 12, 56, 56), Color.Black);
                }

                // Drawing Players Weapon
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

                // Drawing Players Armor
                foreach (var armor in Player.ArmorSet)
                {
                    var position = new Vector2();
                    switch (armor.ArmorType)
                    {
                        case Armors.Helmet: position = new Vector2(570, 70);
                            break;
                        case Armors.Chest: position = new Vector2(635, 70);
                            break;
                        case Armors.Gloves: position = new Vector2(700, 70);
                            break;
                        case Armors.Boots: position = new Vector2(765, 70);
                            break;
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

                // Drawing Plaeyer StatInfo
                this.spriteBatch.DrawString(
                    this.Content.Load<SpriteFont>(Resources.Font_GameFont),
                    string.Format("Damage: {0}", (Player.Weapon != null) ? Player.Weapon.AttackPoints + Player.AttackPoints : Player.AttackPoints),
                    new Vector2(830, 5),
                    Color.Coral);

                this.spriteBatch.DrawString(
                    this.Content.Load<SpriteFont>(Resources.Font_GameFont),
                    string.Format("Armor:    {0:F1}", Player.GetTotalArmor()),
                    new Vector2(830, 30),
                    Color.Coral);

                // Flashing Light When Player is Immortal
                if (Player.ImmortalDuration > 0)
                {
                    if (this.FrameHandlerVariable.FrameIndex % 2 == 0)
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

                // Drawing HUD Frame
                this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.HUD_BorderTexture), new Rectangle(0, 5, 1000, 100), Color.White);

                /* ------------- Drawing Dropped Items -------------*/
                foreach (var drop in Drops)
                {
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(drop.GetTexturePath()), new Rectangle((int)drop.Velocity.X, (int)drop.Velocity.Y, 50, 50), Color.White);
                }

                /* ------------- Drawing Enemies -------------*/
                foreach (var enemy in this.Enemies)
                {
                    this.spriteBatch.Draw(
                    this.Content.Load<Texture2D>(enemy.GetTexturePath()),
                    enemy.Velocity,
                    this.FrameHandlerVariable.GetSpriteFrame(enemy),
                    Color.White,
                    0.0f,
                    characterCenter,
                    1.5f,
                    (enemy.SpriteRotation != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    1.0f);
                }

                /* ------------- Drawing Player -------------*/
                this.spriteBatch.Draw(
                    this.Content.Load<Texture2D>(Player.GetTexturePath()),
                    Player.Velocity,
                    this.FrameHandlerVariable.GetSpriteFrame(Player),
                    Color.White,
                    0.0f,
                    characterCenter,
                    1.5f,
                    (Player.SpriteRotation == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    1.0f);
            }
            else
            {
                // Drawing Game intro
                if (Player.IsAlive())
                {
                    var newColor = ((this.FadeInCounter - 255 < 255) && (this.FadeInCounter < 600))
                        ? this.FadeInCounter - 255
                        : ((this.FadeInCounter < 600) ? this.FadeInCounter - 255 : this.FadeOutCounter);

                    if (this.FadeInCounter < 255)
                    {
                        this.spriteBatch.Draw(
                            this.Content.Load<Texture2D>(Resources.Screen_CompanySplashTexture),
                            new Vector2(150, 335),
                            this.FrameHandlerVariable.GetSplashScreenFrame(),
                            new Color(this.FadeOutCounter, this.FadeOutCounter, this.FadeOutCounter, this.FadeOutCounter));
                    }
                    else
                    {
                        this.spriteBatch.Draw(
                            this.Content.Load<Texture2D>(Resources.Screen_PrePlayTexture), new Vector2(0, 0), new Color(newColor, newColor, newColor, newColor));

                        if ((this.FadeInCounter >= 345) && (this.FadeInCounter < 600))
                        {
                            var innerNewColor = newColor - 90;

                            this.spriteBatch.Draw(
                                this.Content.Load<Texture2D>(Resources.Screen_GameLogoSplashTexture),
                                new Vector2(600, 200),
                                new Color(innerNewColor, innerNewColor, innerNewColor, innerNewColor));
                        }
                    }
                }
                else
                {
                    // Drawing Scoreboard Screen
                    this.spriteBatch.Draw(this.Content.Load<Texture2D>(Resources.Screen_GameOverScreenTexture), new Vector2(50, 0), Color.White);

                    for (var currScore = this.TopScores.Count - 1; currScore >= 0; currScore--)
                    {
                        this.spriteBatch.DrawString(
                            this.Content.Load<SpriteFont>(Resources.Font_GameFontBold),
                            string.Format("{0}.", currScore + 1),
                            new Vector2((currScore < this.TopScores.Count / 2) ? 250 : 550, 500 + (30 * (currScore % (this.TopScores.Count / 2)))),
                            Color.White);

                        this.spriteBatch.DrawString(
                            this.Content.Load<SpriteFont>(Resources.Font_GameFontBold),
                            string.Format("{0}", this.TopScores[currScore]),
                            new Vector2((currScore < this.TopScores.Count / 2) ? 300 : 600, 500 + (30 * (currScore % (this.TopScores.Count / 2)))),
                            (this.TopScores[currScore] == Scoreboard.Score) ? Color.Yellow : Color.White);
                    }
                }
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
