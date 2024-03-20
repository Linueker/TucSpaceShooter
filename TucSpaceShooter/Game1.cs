using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TucSpaceShooter
{
    public enum GameStates
    {
        Menu,
        Play,
        Highscore
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
       
        private GameStates currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentState = GameStates.Menu;//Ska göra så att man startar i menyn, byt ut Menu för att starta i annan state
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (currentState)
            {
                case GameStates.Menu:
                    //Kod för meny
                    break;
                case GameStates.Play:
                    //kod för Play
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch (currentState)
            {
                case GameStates.Menu:
                    //Kod för meny
                    
                    break;
                case GameStates.Play:
                    //kod för Play
                    
                    break;
                case GameStates.Highscore:
                    //kod för highscore
                    
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
