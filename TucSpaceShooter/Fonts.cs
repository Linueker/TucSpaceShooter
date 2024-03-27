using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TucSpaceShooter
{
    public static class Fonts
    {
        private static SpriteFont gameFont;

        public static void LoadContent(ContentManager content)
        {
            gameFont = content.Load<SpriteFont>("GameFont");
        }

        public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(gameFont, text, position, color);
        }

        // Ny metod för att rita ut spelarens poäng
        public static void DrawPoints(SpriteBatch spriteBatch, Player player, Vector2 position, Color color)
        {
            string pointsText = "Points: " + player.points.GetCurrentPoints();
            spriteBatch.DrawString(gameFont, pointsText, position, color);
        }
    }
}
