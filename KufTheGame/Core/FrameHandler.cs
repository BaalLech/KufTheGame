using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using Microsoft.Xna.Framework;

namespace KufTheGame.Core
{
    /// <summary>
    /// Keeps the proper animation frame.
    /// </summary>
    public class FrameHandler
    {
        private const float FrameTime = 0.06F;

        public FrameHandler()
        {
            this.FrameIndex = 0;
            this.EnemyAttackFrames = 0;
            this.PlayerAttackFrames = 0;
        }

        public int FrameIndex { get; private set; }

        public int PlayerAttackFrames { get; set; }

        public int EnemyAttackFrames { get; set; }

        private float ElapsedTime { get; set; }

        public Rectangle GetSpriteFrame(Character character)
        {
            var attackFrames = this.GetAnimationFrames(character);

            if ((attackFrames != 0) && ((character.State == State.YodaStrikePunch) || (character.State == State.WingedHorseKick) || (character.State == State.TeethOfTigerThrow)))
            {
                int animationFrames;
                switch (character.State)
                {
                    case State.YodaStrikePunch:
                        animationFrames = (int)Frames.YodaStrikePunch;
                        break;
                    case State.WingedHorseKick:
                        animationFrames = (int)Frames.WingedHorseKick;
                        break;
                    case State.TeethOfTigerThrow:
                        animationFrames = (int)Frames.TeethOfTigerThrow;
                        break;
                    default:
                        animationFrames = 0;
                        break;
                }

                return new Rectangle((this.FrameIndex % animationFrames) * 80, 140 * (int)character.State, 80, 140);
            }

            return
                new Rectangle(this.FrameIndex % ((character.State == State.Idle) ? (int)Frames.Idle : (int)Frames.Moving) * 80, 140 * (int)character.State, 80, 140);
        }

        public Rectangle GetSplashScreenFrame()
        {
            return new Rectangle(0, (this.FrameIndex % 27) * 82, 701, 82);
        }

        public void Update(GameTime gameTime)
        {
            // Adding Elapsed Time From Last Loop
            this.ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Checks is the Elapsed time higher than 0.1 seconds
            while (this.ElapsedTime > FrameTime) 
            {
                this.FrameIndex += 1;

                // Checks is the Attack Animation is Loaded
                if (this.PlayerAttackFrames > 0) 
                {
                    this.PlayerAttackFrames = ((this.PlayerAttackFrames - 1) > 0) ? this.PlayerAttackFrames - 1 : 0;
                }

                // Checks is the Attack Animation is Loaded
                if (this.EnemyAttackFrames > 0) 
                {
                    this.EnemyAttackFrames = ((this.EnemyAttackFrames - 1) > 1) ? this.EnemyAttackFrames - 1 : 1;
                }

                // Reset Elapsed Time
                this.ElapsedTime = 0F; 
            }

            this.FrameIndex %= int.MaxValue;
        }

        private int GetAnimationFrames(Character character)
        {
            var attackFrames = (character is Player) ? this.PlayerAttackFrames : this.EnemyAttackFrames;

            if ((character.State == State.YodaStrikePunch) || (character.State == State.WingedHorseKick) ||
                (character.State == State.TeethOfTigerThrow) || (attackFrames > 0))
            {
                if ((attackFrames == 0) && ((character.State != State.Idle) || (character.State != State.Moving)))
                {
                    switch (character.State)
                    {
                        case State.YodaStrikePunch:
                            attackFrames = (int)Frames.YodaStrikePunch;
                            break;
                        case State.WingedHorseKick:
                            attackFrames = (int)Frames.WingedHorseKick;
                            break;
                        case State.TeethOfTigerThrow:
                            attackFrames = (int)Frames.TeethOfTigerThrow;
                            break;
                        default:
                            attackFrames = 0;
                            break;
                    }
                }
            }

            if (character is Player)
            {
                this.PlayerAttackFrames = attackFrames;
            }
            else
            {
                this.EnemyAttackFrames = attackFrames;
            }

            return attackFrames;
        }
    }
}
