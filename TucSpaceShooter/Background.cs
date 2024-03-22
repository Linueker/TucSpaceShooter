using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TucSpaceShooter
{
    public class Background
    {
        public static void DrawBackground(int counter, SpriteBatch spriteBatch, Texture2D bgr)
        {
            // När bakgrunds-countern är 0 ritas en ny bagrundsbild precis ovanför förnstret som sedan "rullar" nedåt
            if (counter == 0)
            {
                spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter / 3)), Color.White);
            }
            spriteBatch.Draw(bgr, new Vector2(0, counter / 3), Color.White);
            spriteBatch.Draw(bgr, new Vector2(0, (-720 + counter / 3)), Color.White);
        }
    }
}
