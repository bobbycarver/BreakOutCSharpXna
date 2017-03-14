using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Break_Out
{
    public class Paddle
    {
        // declaring veriables 
        Texture2D PaddleImg; // stores image
        public int PaddleXPos { get; private set; } // x pos for paddle
        public int PaddleYPos { get; private set; } // y pos for paddle 
        bool iskeyleftPaddle; //checks if the left key is pressed
        bool iskeyrightPaddle; //checks if the right key is pressed 
        int movedBy; // holds value of how much paddle has moved 
        public int Paddle_Reset { get; private set; } // 1 = true 0 = false 
        public Rectangle PositionRectangle_paddle_Left { get; private set; } //  paddle rectangle
        public Rectangle PositionRectangle_paddle_Right { get; private set; }
        

 
       
        public Paddle(Texture2D NewTexture,bool iskeyLeft , bool iskeyRight)// askes for paremeters from main class.
        {
            PaddleImg  = NewTexture; // sets the paddle image passed from the main class to the PaddleImg veriable 
            iskeyleftPaddle = iskeyLeft; // sets parameter key left to the new veriable 
            iskeyrightPaddle = iskeyRight; // sets parameter key right to the new veriable 

        }


        public void Initialize()
        {
            PaddleXPos = 420; // default x position of paddle
            PaddleYPos = 650; // defualt y position of paddle 
            Paddle_Reset = 0; // default for paddle reset 
            
        }

        public void paddlereset(int reset)
        {
            Paddle_Reset = reset; // gets paddle reset value from main breakout class 
        }
        

        public int Update()
        {
            if (Paddle_Reset == 1)// if the ball is invisible the paddle reset gets sent is 1 which will reset the paddle 
            {
                PaddleXPos = 420;
            }
            Paddle_Reset = 0; // resets the paddle reset so the paddle is free to move again


            PositionRectangle_paddle_Left = new Rectangle(PaddleXPos, PaddleYPos, 55, 3); // sets the rectangle based on the x position and y position of the paddle and also the image height to the left 
            PositionRectangle_paddle_Right = new Rectangle(PaddleXPos + 56, PaddleYPos, 54, 3); //  sets the rectangle on the x and y of the padle to the right 
      

            movedBy = 0; // sets move By to 0 
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right)) // if the left arrow key is pressed run the following :
            {
                PaddleXPos = PaddleXPos - 10;
                movedBy = -10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left)) // if the right arrow key is pressed run the following:
            {
                PaddleXPos = PaddleXPos + 10;
               movedBy = + 10;
            }
            // checks if the paddle hits the end of the screen and moves it back onto the screen 
            if (PaddleXPos < 10)
            {
                PaddleXPos = 10;
            }
            if (PaddleXPos > 850)
            {
                PaddleXPos = 850;
            }
            return movedBy;
            
        }

        // draw method of paddle 
        public void Draw(SpriteBatch spritebatch) // spritebatch paramater 
        {
            spritebatch.Draw(PaddleImg, new Vector2(PaddleXPos,PaddleYPos),Color.White); // sets the spritebatch to draw the image based on the x and y Positions
        }
}// end of public class 
}// end of namespace


   