using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TucSpaceShooter
{
    public class Powerup : Entity
    {
        public enum PowerupType
        {
            Jetpack,
            Shield,
            Repair,
            DoublePoints,
            TriplePoints
        }

        public PowerupType Type { get; private set; }
        public bool IsActive { get; private set; }
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public Texture2D Texture { get; private set; }

        public Powerup(Vector2 position, PowerupType type, float duration, Texture2D texture) : base(position)
        {
            Type = type;
            IsActive = false;
            Duration = duration;
            ElapsedTime = 0;
            Texture = texture;
        }

        public void Activate()
        {
            IsActive = true;
            ElapsedTime = 0;
        }

        public void Deactivate()
        {
            IsActive = false;
            ElapsedTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ElapsedTime >= Duration)
                {
                    Deactivate();
                }
            }
        }

        public void ApplyPowerup(Player player)
        {
            switch (Type)
            {
                case PowerupType.Jetpack:
                    player.ActivateJetpack();
                    break;
                case PowerupType.Shield:
                    player.ActivateShield();
                    break;
                case PowerupType.Repair:
                    player.ActivateRepair();
                    break;
                case PowerupType.DoublePoints:
                    player.ActivateDoublePoints();
                    break;
                case PowerupType.TriplePoints:
                    player.ActivateTriplePoints();
                    break;
                default:
                    break;
            }
            Deactivate();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
