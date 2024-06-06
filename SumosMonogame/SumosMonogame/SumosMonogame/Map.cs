using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SumosMonogame
{
    public class Map
    {

        int yOfPlatforms = 12;
        int divs = 3;
        public int nTileWidth = 40;
        public int nTileHeight = 40;
        int nLevelWidth, nLevelHeight;
        private List<Vector2> blockPositions;
        private string sLevel1, sLevel2, sLevel3;
        public int level;

        byte[] bits;

        private GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;




        


        
        //MAPA1(ORIGINAL)      
        public Map(GraphicsDevice graphicsDevice, int level)
        {
            this.graphicsDevice = graphicsDevice;
            blockPositions = new List<Vector2>();
            this.level = level;
            sLevel1 = "";

            sLevel1 += "............................................................";
            sLevel1 += "............................................................";

            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";

            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";

            sLevel1 += "...ppppppppppp.....pppppppppppppppppppppp.....ppppppppppp...";
            sLevel1 += "...ppppppppppp.....pppppppppppppppppppppp.....ppppppppppp...";

            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";
            sLevel1 += "............................................................";


            sLevel2 = "";

            sLevel2 += "............................................................";
            sLevel2 += "............................................................";

            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";

            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";

            sLevel2 += "...pppppppp.....ppppppppppp.....pppppppppppp.....pppppppp...";
            sLevel2 += "...pppppppp.....ppppppppppp.....pppppppppppp.....pppppppp...";

            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";
            sLevel2 += "............................................................";

            sLevel3 = "";

            sLevel3 += "............................................................";
            sLevel3 += "............................................................";

            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";

            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";

            sLevel3 += ".........pppppppppppppppppp.....ppppppppppppppppppp.........";
            sLevel3 += ".........pppppppppppppppppp.....ppppppppppppppppppp.........";

            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";
            sLevel3 += "............................................................";


            nLevelWidth = 60;
            nLevelHeight = 17;

        }




        public int[,] Draw(PointF cameraPos, int width, int height, Texture2D brick)
        {

            spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin();
            //g = Graphics.FromImage(bmp);
            //g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            //g.SmoothingMode = SmoothingMode.HighSpeed;



            //NO SE COMO VAMOS A HACER ESTO
            int nVisibleTilesX = width / nTileWidth;
            int nVisibleTilesY = height / nTileHeight;

            // Calculate Top-Leftmost visible tile
            float fOffsetX = cameraPos.X - (float)nVisibleTilesX / 2.0f;
            float fOffsetY = cameraPos.Y - (float)nVisibleTilesY / 2.0f;

            // Clamp camera to game boundaries
            if (fOffsetX < 0) fOffsetX = 0;
            if (fOffsetY < 0) fOffsetY = 0;
            if (fOffsetX > nLevelWidth - nVisibleTilesX) fOffsetX = nLevelWidth - nVisibleTilesX;
            if (fOffsetY > nLevelHeight - nVisibleTilesY) fOffsetY = nLevelHeight - nVisibleTilesY;

            float fTileOffsetX = (fOffsetX - (int)fOffsetX) * nTileWidth;
            float fTileOffsetY = (fOffsetY - (int)fOffsetY) * nTileHeight;




            //Draw visible tile map//*--------------------DRAW------------------------------
            char c;
            float stepX, stepY;
            bool previousOnePlatform = false;
            bool previousOneEmpty = true;
            int indexLeftLimits = 0;
            int indexRightLimits = 0;
            int yAppearance = -1;
            int[,] horizontalLimits = {
             {-1, -1, -1, -1},
             {-1, -1, -1, -1},

             };
            
            blockPositions.Clear();
            for (int y = -1; y < nVisibleTilesY + 2; y++)
            {
                for (int x = -1; x < nVisibleTilesX + 2; x++)
                {
                    c = GetTile(x + (int)fOffsetX, y + (int)fOffsetY);
                    stepX = x * nTileWidth - fTileOffsetX;
                    stepY = y * nTileHeight - fTileOffsetY;



                    switch (c)
                    {
                        case '.':
                            //g.FillRectangle(Brushes.Black, stepX, stepY, nTileWidth, nTileHeight);
                            if (previousOnePlatform && (indexLeftLimits < 4 || indexRightLimits<4))
                            {
                                horizontalLimits[1, indexRightLimits] = (int)stepX;
                                indexRightLimits++;
                                //means the previous x is a right border of a platform
                            }
                            previousOnePlatform = false;
                            previousOneEmpty = true;
                            break;

                            //DIBUJAR BLOQUES YO CREO QUE CON UN SPRITE
                        case 'p':

                            // g.Draw(brick, new Rectangle(10, 60 + heart.Height, nTileWidth, nTileWidth), Color.White);
                            blockPositions.Add(new Vector2(stepX, stepY));

                            


                            if ((previousOneEmpty || x == -1) && (indexLeftLimits < 4))
                            {
                                //means that it encountered a left limit
                                horizontalLimits[0, indexLeftLimits] = (int)stepX;
                                indexLeftLimits++;
                            }
                            if (x == nVisibleTilesX + 1 && indexRightLimits < 4)
                            {
                                horizontalLimits[1, indexRightLimits] = (int)stepX;
                                indexRightLimits++;
                                //last right
                            }

                            previousOnePlatform = true;
                            previousOneEmpty = false;

                            break;

                        default:
                            /*
                            g.FillRectangle(Brushes.Black, stepX, stepY, nTileWidth, nTileHeight);
                            g.FillRectangle(Brushes.DarkRed, stepX + 1, stepY + 1, nTileWidth - 2, nTileHeight - 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight / 2, stepX + nTileHeight, stepY + nTileHeight - 3);
                            g.DrawLine(Pens.Maroon, stepX + nTileHeight / 2, 2 + stepY + nTileHeight / 2, 1 + stepX + nTileHeight, stepY + nTileHeight - 2);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, 1 + stepX + nTileHeight / 2, stepY + 1, 2 + stepX + nTileHeight / 2, 3 + stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Maroon, 2 + stepX + nTileHeight / 2, stepY, 1 + stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3);
                            g.DrawLine(Pens.Black, stepX + nTileHeight / 2, stepY + nTileHeight * 2 / 3, stepX, stepY + nTileHeight / 3);
                            g.DrawRectangle(Pens.Black, stepX + nTileHeight / 2, stepY, nTileWidth, nTileHeight - 1);
                            g.DrawRectangle(Pens.Gray, stepX, stepY, nTileWidth, nTileHeight - 1);*/
                            break;


                            
                    }
                }

                

            }

            foreach (Vector2 position in blockPositions)
            {
                spriteBatch.Draw(brick, new Rectangle((int)position.X, (int)position.Y, nTileWidth, nTileHeight), Color.White);
            }
            spriteBatch.End();

            //regresar plataformas actuales
            return horizontalLimits;

            //player.MainSprite.posX = (player.fPlayerPosX - fOffsetX) * nTileWidth;
            //player.MainSprite.posY = (player.fPlayerPosY - fOffsetY) * nTileHeight;
        }

        public void SetTile(float x, float y, char c)//changes the tile
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
            {
                int index = (int)y * nLevelWidth + (int)x;
                if (level == 1)
                    sLevel1 = sLevel1.Remove(index, 1).Insert(index, c.ToString());
                if (level == 2)
                    sLevel2 = sLevel2.Remove(index, 1).Insert(index, c.ToString());
                if (level == 3)
                    sLevel3= sLevel3.Remove(index, 1).Insert(index, c.ToString());


            }
        }

        public char GetTile(float x, float y)
        {
            if (x >= 0 && x < nLevelWidth && y >= 0 && y < nLevelHeight)
                switch (level)
                {
                    case 1:
                        return sLevel1[(int)y * nLevelWidth + (int)x];
                    case 2:
                        return sLevel2[(int)y * nLevelWidth + (int)x];
                    default:
                        return sLevel3[(int)y * nLevelWidth + (int)x];
                }

            else
                return ' ';
        }

        public void changeLevel()
        {
            if (level != 3)
            {
                level++;
            }
            else {
                level = 1;
            }
        }


    }
}
