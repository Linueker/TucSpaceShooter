using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class MenuScreen
    {
        private Button startButton;
        private Button highscoreButton;
        private Button quitButton;
        //private Texture2D background;

        public MenuScreen(Texture2D startButtonTexture, Rectangle startButtonBounds, Texture2D highscoreButtonTexture, Rectangle highscoreButtonBounds, Texture2D quitButtonTexture, Rectangle quitButtonBounds/*, Texture2D background*/)
        {
            startButton = new Button(startButtonTexture, startButtonBounds);
            highscoreButton = new Button(highscoreButtonTexture, highscoreButtonBounds);
            quitButton = new Button(quitButtonTexture, quitButtonBounds);
            //this.background = background;
        }

        public void MainMenu()
        {
            MouseState mouseState = Mouse.GetState();

            if (startButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Play;
            }
            if(highscoreButton.IsClicked(mouseState)) 
            { 
                Game1.CurrentState = GameStates.Highscore;
            }
            if (quitButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Quit;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            startButton.Draw(spriteBatch);
            highscoreButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
        }
    }
}
