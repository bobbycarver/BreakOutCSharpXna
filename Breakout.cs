using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Break_Out
{
    public class Breakout : Microsoft.Xna.Framework.Game
    {
   
        // setting veriables 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D BackgroundImg;
        Texture2D StartScreen;
        Texture2D endScreen;
        Texture2D sucsess_screen;
        Bricks bricks; 
        Paddle paddle;
        GameBall gameball;
        bool iskeyLeft = false;
        bool iskeyRight = false;
        bool Flag;
        int moveBy;
        public int gamestate { get; private set; }
       bool collision_paddle;
       SpriteFont gamefont;
       int playscore;
       int game_progress;
       bool spacebar = false;
       int padReset; 
        bool leftleft = false;
             bool   leftright = false;
                bool rightright = false;
                bool rightleft = false;
       
        
        

        public Breakout()
        {
            graphics = new GraphicsDeviceManager(this);
            // perfered screen sizes 
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {

            base.Initialize();
                bricks.Initialize();
                paddle.Initialize();
                gameball.Initialize();

                collision_paddle = false; // sets colision paddle to false 
            
            gamestate = 1;
         
        }

  
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            StartScreen = Content.Load<Texture2D>("splash");
            gamefont = Content.Load<SpriteFont>("gamefont");
            BackgroundImg = Content.Load<Texture2D>("starfield");
            endScreen = Content.Load<Texture2D>("endScreen");
            sucsess_screen = Content.Load<Texture2D>("sucsess_screen");
                
                bricks = new Bricks(
                    Content.Load<Texture2D>("red brick"),
                    Content.Load<Texture2D>("brickblue"),
                    Content.Load<Texture2D>("greenbrick"),
                    Content.Load<Texture2D>("pinkbrick"),
                    Content.Load<Texture2D>("aquaBrick"),
                    Content.Load<SpriteFont>("gamefont")
                    ); //  passes the parameters to bricks class 

                paddle = new Paddle(Content.Load<Texture2D>("Paddle"), iskeyLeft, iskeyRight); // passes paramaeters to paddle 

                gameball = new GameBall(Content.Load<Texture2D>("ball"), Content.Load<SpriteFont>("gamefont"));


                IsMouseVisible = true; // allows the mouse to be shown 
            

            
        }


        protected override void UnloadContent()
        {
        
        }
        protected override void Update(GameTime gameTime)
        {
            playscore = bricks.playerscore;

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gamestate = 2;

            }
            if (gameball.gamelives < 0)
            {
                gamestate = 3;

            }
            
            game_progress = bricks.gameprog();
            if (game_progress == 120)
            {
                gamestate = 4;
            }
            if (gamestate == 2 && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                
                    spacebar = true;
               
            }
            gameball.getspacebarstate(spacebar);
            spacebar = false;
           
            // call update on paddle 
            
                moveBy = paddle.Update();

                // sets a flag to see if the ball has been fired 
                Flag = gameball.BallFire();
                if (Flag == false)
                {
                    gameball.moveBall(moveBy); // if the ball has been fired this will stop the ball following the paddle 


                }

                gameball.Update();
                bricks.Update(gameball.ballXPos, gameball.ballYPos);





                // paddle collison detection 
                if (gameball.PositionRectangle_ball.Intersects(paddle.PositionRectangle_paddle_Left) && gameball.ballXDir == -1) // if the ball hits the left side of the paddle coming down from he left
                {


                    leftleft = true;
                }
                if (gameball.PositionRectangle_ball.Intersects(paddle.PositionRectangle_paddle_Left) && gameball.ballXDir == 1) // if the ball hits the left side of the paddle coming from the right 
                {

                    leftright = true;

                }
                if (gameball.PositionRectangle_ball.Intersects(paddle.PositionRectangle_paddle_Right) && gameball.ballXDir == 1) // if the ball hits the right side coming from the left
                {

                    rightleft = true;

                }
                if (gameball.PositionRectangle_ball.Intersects(paddle.PositionRectangle_paddle_Right) && gameball.ballXDir == -1) // if the ball his the right side coming from the right 
                {

                    rightright = true;

                }

                gameball.CheckCollision(leftleft, leftright, rightleft, rightright); // passes the collision bool value 
                // resets values 
                leftleft = false;
                leftright = false;
                rightright = false;
                rightleft = false;



                base.Update(gameTime);
                padReset = gameball.paddleReset();
                paddle.paddlereset(padReset);
                
        }
            
        


         protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (gamestate == 1)
            {
                spriteBatch.Draw(StartScreen, Vector2.Zero, Color.White);
            }

            if (gamestate == 2)
            {
                // draws all sprites 
                base.Draw(gameTime);
               
                spriteBatch.Draw(BackgroundImg, Vector2.Zero, Color.White);
                bricks.Draw(spriteBatch);
                paddle.Draw(spriteBatch);
                gameball.Draw(spriteBatch);

                
            }
            if (gamestate == 3)
            {
                spriteBatch.Draw(endScreen, Vector2.Zero, Color.White);
                string displayscore = string.Format("{0}", playscore);
                spriteBatch.DrawString(gamefont, displayscore, new Vector2(350, 300), Color.White, 0.0f, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0.0f);
              
                
            }
            if (gamestate == 4)
            {
                spriteBatch.Draw(sucsess_screen, Vector2.Zero, Color.White);
                string displayscore = string.Format("{0}", playscore);
                spriteBatch.DrawString(gamefont, displayscore, new Vector2(350, 300), Color.White, 0.0f, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0.0f);

            }
            spriteBatch.End();
            }
        }
    

}