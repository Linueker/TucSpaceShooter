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
    public class PauseScreen
    {
        private Button resumeButton;
        private Button quitButton;

        public PauseScreen(Texture2D resumeButtonTexture, Rectangle resumeButtonBounds, Texture2D quitButtonTexture, Rectangle quitButtonBounds)
        {
            resumeButton = new Button(resumeButtonTexture, resumeButtonBounds);
            quitButton = new Button(quitButtonTexture, quitButtonBounds);
        }

        public void ClickButton()
        {
            MouseState mouseState = Mouse.GetState();

            if (resumeButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Play;
            }
            if (quitButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Quit;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            resumeButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
        }
    }
}
