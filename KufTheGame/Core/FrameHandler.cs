using Microsoft.Xna.Framework;

using KufTheGame.Models.Enums;
using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Game.Models.Characters;

namespace KufTheGame.Core
{
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

        private int GetAnimationFrames(Character character)
        {
            var attackFrames = (character is Player) ? PlayerAttackFrames : EnemyAttackFrames;

            if ((character.State == State.YodaStrikePunch) || (character.State == State.WingedHorseKick) ||
                (character.State == State.TeethOfTigerThrow) || (attackFrames > 0))
            {
                if ((attackFrames == 0) && ((character.State != State.Idle) || (character.State != State.Moving)))
                {
                    switch (character.State)
                    {
                        case State.YodaStrikePunch:
                            attackFrames = (int) Frames.YodaStrikePunch;
                            break;
                        case State.WingedHorseKick:
                            attackFrames = (int) Frames.WingedHorseKick;
                            break;
                        case State.TeethOfTigerThrow:
                            attackFrames = (int) Frames.TeethOfTigerThrow;
                            break;
                        default:
                            attackFrames = 0;
                            break;
                    }
                }
            }

            if (character is Player)
            {
                PlayerAttackFrames = attackFrames;
            }
            else
            {
                EnemyAttackFrames = attackFrames;
            }

            return attackFrames;
        }

        public Rectangle GetSpriteFrame(Character character)
        {
            var attackFrames = GetAnimationFrames(character);

            if ((attackFrames != 0) && ((character.State == State.YodaStrikePunch) || (character.State == State.WingedHorseKick) || (character.State == State.TeethOfTigerThrow)))
            {
                int animationFrames;
                switch (character.State)
                {
                    case State.YodaStrikePunch:
                        animationFrames = (int) Frames.YodaStrikePunch;
                        break;
                    case State.WingedHorseKick:
                        animationFrames = (int) Frames.WingedHorseKick;
                        break;
                    case State.TeethOfTigerThrow:
                        animationFrames = (int) Frames.TeethOfTigerThrow;
                        break;
                    default:
                        animationFrames = 0;
                        break;
                }

                return new Rectangle((FrameIndex % animationFrames) * 80, 140*(int) character.State, 80, 140);
            }

            return
                new Rectangle( FrameIndex%((character.State == State.Idle) ? (int) Frames.Idle : (int) Frames.Moving)*80, 140*(int) character.State, 80, 140);
        }

        public Rectangle GetSplashScreenFrame()
        {
            return new Rectangle(0, (FrameIndex % 27) * 82, 701, 82);
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime += (float) gameTime.ElapsedGameTime.TotalSeconds; //Adding Elapsed Time From Last Loop
            while (ElapsedTime > FrameTime) //Checks is the Elapsed time higher than 0.1 seconds
            {
                FrameIndex += 1;
                if (PlayerAttackFrames > 0) //Checks is the Attack Animation is Loaded
                {
                    PlayerAttackFrames = ((PlayerAttackFrames - 1) > 0) ? PlayerAttackFrames - 1 : 0;
                }

                if (EnemyAttackFrames > 0) //Checks is the Attack Animation is Loaded
                {
                    EnemyAttackFrames = ((EnemyAttackFrames - 1) > 1) ? EnemyAttackFrames - 1 : 1;
                }

                ElapsedTime = 0F; // Reset Elapsed Time
            }

            FrameIndex %= int.MaxValue;
        }
    }
}
