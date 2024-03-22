using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class Button
    {
        private Texture2D texture;
        private Rectangle bounds;

        public Button(Texture2D texture, Rectangle bounds)
        {
            this.texture = texture;
            this.bounds = bounds;
        }

        public bool IsClicked(MouseState mouseState)
        {
            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            return mouseRectangle.Intersects(bounds) && mouseState.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
