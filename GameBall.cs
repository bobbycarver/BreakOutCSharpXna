using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Break_Out
{
    class GameBall
    {
        
        public int ballXDir { get; private set; }
        public int ballYDir { get; private set; }
        public int ballXPos { get; private set; }
        public int ballYPos { get; private set; }
        public int setBallYPos { get; private set; }
        public int setBallXPos { get; private set; }
        public int ballSpeed { get; private set; }
        public int gamelives { get; private set; }
        public Texture2D ballimg {get; private set; }
        public Rectangle PositionRectangle_ball { get; private set; }
        bool ballFired;
        public bool Ballvisible { get; private set; }
        bool Is_hit;
        int PaddleResetValue;
        SpriteFont gamefont;
        bool spacebarstate;
         bool LeftHitLeft;
         bool   LeftHitRight ;
       bool RightHitLeft;
      bool RightHitRight;
      int ballBounce;
      
       
 

        public GameBall(Texture2D NewTexture, SpriteFont NewGameFont)
        {
            // getting the image and game font from breakout class so they can be set in gameball class.
            ballimg = NewTexture;
            gamefont = NewGameFont;
            
            PositionRectangle_ball = new Rectangle(ballXPos, ballYPos, ballimg.Width, ballimg.Height); //  creating rectangle for ball
        }
       
 
        public void Initialize()
        {
         
                // setting default values for the gameball and lives.
                ballSpeed = 3;
                ballXDir = -1;
                ballYDir = -1;
                ballXPos = 450;
                ballYPos = 620;
                ballFired = false;
                Ballvisible = true;
                Is_hit = false;
                gamelives = 3;
                PaddleResetValue = 0 ; //  true or false value which gets passed to the breakout class when paddle needs to be reset

           
                
             
               
         
            
        }

        public bool BallFire()
        {
            return ballFired; //  returns the ball fired value to start the game 
        }

        public void CheckCollision(bool leftleft, bool leftright, bool rightleft, bool rightright)
        {
            //  checks if there has been a collision from the main breakout class 
            LeftHitLeft = leftleft;
            LeftHitRight = leftright;
            RightHitLeft = rightleft;
            RightHitRight = rightright;


        }
        public void getspacebarstate(bool spacebar)
        {
            spacebarstate = spacebar; // gets the space bar value from the mian breakout class
        }
        public void brickBounceGet(int hasballBounce)
        {
            ballBounce = hasballBounce;
        }



        public void  Update()
        {
            bool live;

           
            PositionRectangle_ball = new Rectangle(ballXPos, ballYPos, ballimg.Width, ballimg.Height); // rectangle for ball 

            /*if (Is_hit == true && ballFired == true) // checks if the ball has been fired and if there has been a collision 
            {
                // if there has been a collision the X and Y directions are reversed to create a bounce 
               
                //  ballXDir = +1;
                  //ballYDir = -1;
               Is_hit = false; //  resets the hit to false 
            }
            */
            if (LeftHitLeft == true && ballFired == true)
            {
                ballYDir = -1;
                ballXDir = +1;
               
                LeftHitLeft = false;
                live = true;

            }
            if (LeftHitRight == true && ballFired == true)
            {
                ballYDir = -1;
              
                LeftHitRight = false;
                live = true;

            }
            if (RightHitLeft == true && ballFired == true)
            {
                ballYDir = -1;
               
                RightHitLeft = false;
                live = true;

            }
            if (RightHitRight == true && ballFired == true)
            {
                ballYDir = -1;
                ballXDir = -1;
            
                RightHitRight = false;
                live = true;

            }


            if (ballBounce == 1)
            {
                ballXDir = ballXDir * -1;
                ballYDir = ballYDir * -1;

            }



            if (Ballvisible == false)// once the ball is invisble reset the following values 
            {

                ballSpeed = 3;
                ballXDir = -1;
                ballYDir = -1;
                ballXPos = 450;
                ballYPos = 590;
                ballFired = false;
                Ballvisible = true;
                gamelives = gamelives - 1; //  takes off 1 life 
                PaddleResetValue = 1; // sets paddle reset to 1 so that the paddle can be reset in the paddle class once its passed 
             
            }
            
          
        }
            
          
           public int paddleReset()
           {
               return PaddleResetValue;// returns the paddle reset to main class 
           }
         
          public void moveBall(int amount)
          {
              ballXPos = ballXPos + amount; //  allows the ball to follow the paddle 
          }


          

        
        public void Draw(SpriteBatch spritebatch)
        {
            string lives = string.Format("Number of lives Left: = {0}", gamelives);
            spritebatch.DrawString(gamefont, lives, new Vector2(200, 700), Color.White); //  draws lives onto screen 
            if (Ballvisible == true)
            {

                spritebatch.Draw(ballimg, new Vector2(ballXPos, ballYPos),//PositionRectangle_ball,
                    Color.White); // draws ball onto screen
            }
        }

    }
}