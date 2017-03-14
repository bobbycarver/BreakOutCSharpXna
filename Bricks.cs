using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Break_Out
{
    class Bricks
    {
        
        Texture2D redbrickimg;
        Texture2D blueBrickimg;
        Texture2D greenBrickimg;
        Texture2D pinkBrickimg;
        Texture2D aquaBrickimg;
        int[,] brickXPos = new int [12,12];
        int[,] brickYPos = new int [12,12];
        int[,] colourBrick  = new int[12, 12];
        bool[,] brickAlive = new bool[12, 12];
        public int playerscore {get; private set; } 
        SpriteFont gamefont;
        int game_progress;
  
        
       
        
    
      
        public Bricks(
            Texture2D Redbricks,
            Texture2D blueBricks,
            Texture2D greenBricks,
            Texture2D pinkBricks,
            Texture2D aquaBricks,
            SpriteFont NewGameFont
            )
        {
            redbrickimg  = Redbricks;
            blueBrickimg = blueBricks;
            greenBrickimg = greenBricks;
            pinkBrickimg = pinkBricks;
            aquaBrickimg = aquaBricks;
            gamefont = NewGameFont;
            
            // rectangles for sides
 
        
        }

        public void Initialize()
        {
            //gameball.Initialize();
            playerscore = 0;
            game_progress = 0;
            
            for (int j = 0; j < 12; j++)
            {

                for (int i = 0; i < 12; i++)
                {
                    brickXPos[j,i] = 1 + i * redbrickimg.Width;
                    brickYPos[j,i] = 1 + j * redbrickimg.Height;
                    brickAlive[j, i] = true;
                    
                    colourBrick[j,i] = j/2;
                }                
            }
            
            
            
        }
        




        public void Update( int Xballpos, int Yballpos)
        {
            int xballmin, xballmax, yballmin, yballmax;
            int xbrickmin, xbrickmax, ybrickmin, ybrickmax;
     

       

            xballmin = Xballpos;
            xballmax = xballmin + 30;
            yballmin = Yballpos;
            yballmax = yballmin + 30;

            for (int row = 0; row < 10; row++)
            {

                for (int col = 0; col < 12; col++)
                {
                    if( brickAlive[row,col] == true )
                    {

                     

                         
                    
                         xbrickmin = brickXPos[row, col];
                         xbrickmax = xbrickmin + blueBrickimg.Width;
                         ybrickmin = brickYPos[row, col];
                         ybrickmax = ybrickmin + blueBrickimg.Height;
                        
            
                        if  (  
                                    (
                                          ( xballmin >= xbrickmin  &&  xballmin  <= xbrickmax )
                                      ||  ( xballmax >= xbrickmin  &&  xballmax  <= xbrickmax )
                                                                                                 )
                                &&  (
                                          ( yballmin >= ybrickmin  && yballmin   <= ybrickmax)
                                      ||  ( yballmax >= ybrickmin  && yballmax   <= ybrickmax)   )
                                                                                                         )

                     
                        {
                            brickAlive[row, col] = false;
                            playerscore = playerscore + 150;
                            if (brickAlive[row, col] == false)
                            {
                                game_progress = game_progress + 1;
                            }
                            
                            
                           

                        }
                        
                    }
                }
            }
           
         
        }
        public int gameprog()
        {

            return game_progress;
        }
        
            
        
        public void Draw(SpriteBatch spritebatch)

        {
            string score = string.Format("Score = {0}", playerscore);
            spritebatch.DrawString(gamefont, score, new Vector2(10,700), Color.White);
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (brickAlive[j,i] == true)
                    {
                        if (colourBrick[j, i] == 0)
                        {
                            spritebatch.Draw(redbrickimg, new Vector2(brickXPos[j, i], brickYPos[j, i]), Color.White);
                        }
                        else if (colourBrick[j, i] == 1)
                        {
                            spritebatch.Draw(blueBrickimg, new Vector2(brickXPos[j, i], brickYPos[j, i]), Color.White);
                        }
                        else if (colourBrick[j, i] == 2)
                        {
                            spritebatch.Draw(greenBrickimg, new Vector2(brickXPos[j, i], brickYPos[j, i]), Color.White);
                        }
                        else if (colourBrick[j, i] == 3)
                        {
                            spritebatch.Draw(pinkBrickimg, new Vector2(brickXPos[j, i], brickYPos[j, i]), Color.White);
                        }
                        else if (colourBrick[j, i] == 4)
                        {
                            spritebatch.Draw(aquaBrickimg, new Vector2(brickXPos[j, i], brickYPos[j, i]), Color.White);
                        }
                    }
                }
            }
        }
    }
}
