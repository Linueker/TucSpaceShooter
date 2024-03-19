using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TucSpaceShooter
{
    public class Player : Entity
    {
        public Player(Vector2 position, GraphicsDeviceManager graphics) : base(position)
        {
            this.position.X = graphics.PreferredBackBufferWidth/2 - 30;
            this.position.Y = graphics.PreferredBackBufferHeight-70; 
        }

        public static void MoveUp(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.Y != 10)
            {
                player.position.Y -= 2;
            }
        }
        public static void MoveDown(Player player, GraphicsDeviceManager graphics)
        {
            if(player.position.Y != graphics.PreferredBackBufferHeight - 50)
            player.position.Y += 2;
        }
        public static void MoveLeft(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.X != -10)
            {
                player.position.X -= 2;
            }
        }
        public static void MoveRight(Player player, GraphicsDeviceManager graphics)
        {
            if (player.position.X != graphics.PreferredBackBufferWidth - 50)
            {
                player.position.X += 2;
            }
        }

        public static void PlayerMovement(Player player, GraphicsDeviceManager graphics)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Player.MoveUp(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Player.MoveDown(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Player.MoveLeft(player, graphics);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Player.MoveRight(player, graphics);
            }
        }
    }
}
