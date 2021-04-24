using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeasonsPlatformer
{
    public class Player
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds;//For the collisions
        public bool isIdle=false;
        public float gravity = 3f;
        public bool isFalling = true;
        public Player()
        {
            
            
            pos = new Vector2(100, 50);
            playerBounds = new Rectangle((int)pos.X/*centered at centre*/,(int)pos.Y,8,8);

        }
        public void Load(SpriteSheet[] spriteSheets)
        {
            playerSprite = new AnimatedSprite[spriteSheets.Length];
            for (int i =0; i<spriteSheets.Length;i++)
            {
                sheet = spriteSheets[i];
                playerSprite[i] = new AnimatedSprite(sheet);
            }


        }

        public void Update(GameTime gameTime)
        {
            if (isFalling)
                pos.Y += gravity;
            
            bool isIdle = true;
            var keyboardstate = Keyboard.GetState();
            if(keyboardstate.IsKeyDown(Keys.D))//Move right
            {
                pos.X += moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.A))//Move right
            {       
                pos.X -= moveSpeed;
                isIdle = false;
                
           
            }
          if(!isIdle)
            {
                playerSprite[0].Play("walk");
                playerSprite[0].Update(gameTime);
            }
          else
            {
                playerSprite[0].Play("idle");
                playerSprite[0].Update(gameTime);
            }
            playerBounds.X = (int)pos.X-4;//Apparently by default the rectangle gets centred at the player's centre when using monogame extended's draw function.
            playerBounds.Y = (int)pos.Y-4;
            
        }
        public void Draw(SpriteBatch spriteBatch,Matrix matrix)

        {
            spriteBatch.Begin(//All of these need to be here :(
                SpriteSortMode.Deferred,
                samplerState:SamplerState.PointClamp,
                effect:null,
                blendState:null,
                rasterizerState:null,
                depthStencilState:null,
                transformMatrix:matrix/*<-This is the main thing*/);
           // if (isIdle)
              //  spriteBatch.Draw(playerSprite[0], pos);
           // else
                spriteBatch.Draw(playerSprite[0], pos);
            spriteBatch.End();
        }
    }
}
