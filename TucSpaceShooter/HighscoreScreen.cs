using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class HighscoreScreen
    {
        private Button backButton;
        private static bool keyPressed = false;
        static string PATH = "scores.json";

        public HighscoreScreen(Texture2D backButtonTexture, Rectangle backButtonBounds)
        {
            backButton = new Button(backButtonTexture, backButtonBounds);
            //saveButton = new Button(backButtonTexture, backButtonBounds);
        }
        
        public void ClickButton(bool victory) 
        {
            MouseState mouseState = Mouse.GetState(); ;

            if (backButton.IsClicked(mouseState))
            {
                Game1.CurrentState = GameStates.Menu;
                victory = false;
            }
        }

        public void DrawBackbutton(SpriteBatch spriteBatch)
        {
            backButton.Draw(spriteBatch);
        }
        public static string EnterPlayerName(string userInput, Keys[] keys, KeyboardState keyboardstate)
        {
            // Kolla om någon knapp är nedtryckt
            if (keyboardstate.GetPressedKeys().Length > 0)
            {
                if (!keyPressed)
                {
                    foreach (Keys key in keys)
                    {
                        if (key >= Keys.A && key <= Keys.Z)
                        {
                            if (userInput.Length < 3)
                            {
                                userInput += key.ToString();
                            }
                        }
                    }
                    if (keyboardstate.IsKeyDown(Keys.Back) && userInput.Length > 0)
                    {
                        userInput = userInput.Remove(userInput.Length - 1);
                    }
                    keyPressed = true;
                }
            }
            else
            {
                keyPressed = false;
            }

            return userInput;
        }
        static void AddStringToJson(string score, List<string> highscores)
        {
            highscores = ReadJson();
            highscores.Add(score);
            string jsonText = JsonSerializer.Serialize(highscores);
            File.WriteAllText(PATH, jsonText);
        }
        public static List<string> ReadJson()
        {
            if (File.Exists(PATH))
            {
                string jsonText = File.ReadAllText(PATH);
                return JsonSerializer.Deserialize<List<string>>(jsonText);
            }
            else
            {
                return new List<string>();
            }
        }

        public void Save(Player player, string userInput, List<string> highscores, Button saveButton)
        {
            MouseState mouseState = Mouse.GetState();

            if (saveButton.IsClicked(mouseState))
            {
                AddStringToJson($"{player.points.GetCurrentPoints()} - {userInput} \n", highscores);
                Game1.CurrentState = GameStates.Highscore;
                
            }

        }
       
    }
}
