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
    public class HighscoreScreen
    {
        private Button backButton;

        public HighscoreScreen(Texture2D backButtonTexture, Rectangle backButtonBounds)
        {
            backButton = new Button(backButtonTexture, backButtonBounds);
        }

        public void ClickButton() 
        { 
            MouseState mouseState = Mouse.GetState();

            if (backButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Menu;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            backButton.Draw(spriteBatch);
        }
    }
}
