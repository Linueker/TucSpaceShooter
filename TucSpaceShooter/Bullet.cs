using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TucSpaceShooter
{
    public class Bullet
    {
        private Vector2 position;
        private readonly float speed;
        private bool isActive;

        public bool IsActive { get => isActive; set => isActive = value; }
        public Vector2 Position { get => position; set => position = value; }

        public Bullet(Vector2 initialPosition)
        {
            position = initialPosition;
            speed = 10; // You can adjust this speed as necessary.
            isActive = true;
        }

        public void Update()
        {
            // Bullets move straight up
            position.Y -= speed;


            // Deactivate the bullet if it goes off the screen
            if (position.Y < 0) isActive = false;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bulletTexture)
        {
            if (isActive)
            {
                spriteBatch.Draw(bulletTexture, position, Color.White);
            }
        }
    }
}
