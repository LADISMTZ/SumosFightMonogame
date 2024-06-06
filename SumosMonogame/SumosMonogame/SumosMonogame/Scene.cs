using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SumosMonogame
{
    public class Scene
    {
        public int rightLimitPlatform1, leftLimitPlatform1, rightLimitPlatform2, leftLimitPlatform2, rightLimitPlatform3, leftLimitPlatform3, leftLimitPlatform4, rightLimitPlatform4;
        int ySuperiorLimit;
        int yInferiorLimit;


        private GraphicsDevice graphicsDevice;
        

        public List<VElement> Elements { get; set; }

        public Scene(int Wight, int Height, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            ySuperiorLimit = Height - 290;
            yInferiorLimit = Height - 190;
            Elements = new List<VElement>();


        }
        public void AddSumo(Vec2 center, int radius)
        {
            this.AddElement(new VElement(center, radius, radius, graphicsDevice));
            int countId = 0;
            //Sumo center point
            //THE CENTER POINT WILL BE 0
            Elements[Elements.Count - 1].bodyCenterId = countId;
            Elements[Elements.Count - 1].addPoint(center.X, center.Y, countId, false, 55, 0.5f, new Vec2(0, 12));
            countId++;
            //body of sumo traversing the center by using degrees
            for (int i = 0; i < 360; i = i + 30)
            {
                double radians = i * (Math.PI / 180);
                Elements[Elements.Count - 1].addPoint(center.X + radius * (float)Math.Cos(radians), center.Y + radius * (float)Math.Sin(radians), countId, false, 5, 0.4f, new Vec2(0, 0));
                countId++;
                Elements[Elements.Count - 1].addPole(0, countId - 1, 0, 0.6f);
                //The first vpoint of the body wont need to traverse the previous ones
                if (i != 0)
                {

                    for (int j = 1; j < countId - 1; j++)
                    {
                        Elements[Elements.Count - 1].addPole(j, countId - 1, 0, 0.3f);
                    }

                }
            }
            Elements[Elements.Count - 1].addPole(0, countId - 1, 0, 0.6f); ;
            for (int j = 1; j < countId - 1; j++)
            {
                Elements[Elements.Count - 1].addPole(j, countId - 1, 0, 0.3f);
            }

            //Sumo head center point
            //THE SUMO CENTER HEAD POINT WILL BE 13
            Vec2 headCenter = new Vec2(center.X, center.Y - radius - 25);
            Elements[Elements.Count - 1].addPoint(headCenter.X, headCenter.Y, countId, false, 20, 0.1f, new Vec2(0, 1));
            Elements[Elements.Count - 1].headCenterId = countId;
            countId++;
            //Lil head of baby gurl traversing the center by using degrees
            


        }

        public void AddElement(VElement element)
        {
            Elements.Add(element);
        }

        public void checkCollisionBetweenElements(int e1, int e2)
        {
            VElement element1 = Elements[e1];
            VElement element2 = Elements[e2];
            Color color;

            VPoint sumo1Body = element1.VPoints[element1.bodyCenterId];
            VPoint sumo2Body = element2.VPoints[element2.bodyCenterId];
            VPoint sumo1Head = element1.VPoints[element1.headCenterId];
            VPoint sumo2Head = element2.VPoints[element2.headCenterId];

            bool headCol1, headCol2 = false;

            //COLLISION BETWEEN BODY
            VPoint.checkCollisionBetweenPoints(sumo1Body, sumo2Body, false);
            //COLLISION BETWEEN BODY SUMO 1 AND HEAD SUMO 2
            headCol2 = VPoint.checkCollisionBetweenPoints(sumo1Body, sumo2Head, false);
            //COLLISION BETWEEN BODY SUMO 2 AND HEAD SUMO 1
            headCol1 = VPoint.checkCollisionBetweenPoints(sumo2Body, sumo1Head, false);

            //if there is a head collision get sumo to sleep
            if (headCol1)
            {
                element1.knocked = true;

            }
            if (headCol2)
            {
                element2.knocked = true;

            }

           


        }


        public void extractLimits(int[,] matriz)
        {

            leftLimitPlatform1 = matriz[0, 0];
            rightLimitPlatform1 = matriz[1, 0];

            leftLimitPlatform2 = matriz[0, 1];
            rightLimitPlatform2 = matriz[1, 1];


            leftLimitPlatform3 = matriz[0, 2];
            rightLimitPlatform3 = matriz[1, 2];

            leftLimitPlatform4 = matriz[0, 3];
            rightLimitPlatform4 = matriz[1, 3];

        }

        public void checkCollisionOfSumoWithPlatforms(int e)
        {


            VElement sumo = Elements[e];
            Vec2 center = sumo.VPoints[sumo.bodyCenterId].pos;
            float radius = sumo.VPoints[sumo.bodyCenterId].radius;
            int option = checkBoundingBoxesPlatform(e);
            switch (option)
            {

                case 1:

                    if (center.Y < ySuperiorLimit)
                    {
                        center.Y = ySuperiorLimit - radius;
                    }
                    if (center.Y > yInferiorLimit)
                    {
                        center.Y = yInferiorLimit + radius;
                    }
                    if (center.X > rightLimitPlatform1)
                    {
                        center.X = rightLimitPlatform1 + radius;
                    }
                    if (center.X < leftLimitPlatform1)
                    {
                        center.X = leftLimitPlatform1 - radius;
                    }


                    break;



                case 2:

                    if (center.Y < ySuperiorLimit)
                    {
                        center.Y = ySuperiorLimit - radius;
                    }
                    if (center.Y > yInferiorLimit)
                    {
                        center.Y = yInferiorLimit + radius;
                    }
                    if (center.X > rightLimitPlatform2)
                    {
                        center.X = rightLimitPlatform2 + radius;
                    }
                    if (center.X < leftLimitPlatform2)
                    {
                        center.X = leftLimitPlatform2 - radius;
                    }


                    break;



                case 3:

                    if (center.Y < ySuperiorLimit)
                    {
                        center.Y = ySuperiorLimit - radius;
                    }
                    if (center.Y > yInferiorLimit)
                    {
                        center.Y = yInferiorLimit + radius;
                    }
                    if (center.X > rightLimitPlatform3)
                    {
                        center.X = rightLimitPlatform3 + radius;
                    }
                    if (center.X < leftLimitPlatform3)
                    {
                        center.X = leftLimitPlatform3 - radius;
                    }


                    break;

                case 4:

                    if (center.Y < ySuperiorLimit)
                    {
                        center.Y = ySuperiorLimit - radius;
                    }
                    if (center.Y > yInferiorLimit)
                    {
                        center.Y = yInferiorLimit + radius;
                    }
                    if (center.X > rightLimitPlatform4)
                    {
                        center.X = rightLimitPlatform4 + radius;
                    }
                    if (center.X < leftLimitPlatform4)
                    {
                        center.X = leftLimitPlatform4 - radius;
                    }


                    break;


                case 0:
                    break;

                default:
                    break;


            }



        }

        public int checkBoundingBoxesPlatform(int e)
        {
            VElement sumo = Elements[e];
            Vec2 center = sumo.VPoints[sumo.bodyCenterId].pos;
            float radius = sumo.VPoints[sumo.bodyCenterId].radius;

            if (((ySuperiorLimit < center.Y + radius && yInferiorLimit > center.Y + radius) //check up
                    ||
                    (ySuperiorLimit < center.Y - radius && yInferiorLimit > center.Y - radius)) //check down intersection
                    &&
                    ((rightLimitPlatform1 > center.X - radius && leftLimitPlatform1 < center.X - radius) //check right
                    ||
                    (rightLimitPlatform1 > center.X + radius && leftLimitPlatform1 < center.X + radius)))
            {
                return 1;
            }


            if (((ySuperiorLimit < center.Y + radius && yInferiorLimit > center.Y + radius) //check up
                   ||
                   (ySuperiorLimit < center.Y - radius && yInferiorLimit > center.Y - radius)) //check down intersection
                   &&
                   ((rightLimitPlatform2 > center.X - radius && leftLimitPlatform2 < center.X - radius) //check right
                   ||
                   (rightLimitPlatform2 > center.X + radius && leftLimitPlatform2 < center.X + radius)))
            {
                return 2;
            }


            if (((ySuperiorLimit < center.Y + radius && yInferiorLimit > center.Y + radius) //check up
                   ||
                   (ySuperiorLimit < center.Y - radius && yInferiorLimit > center.Y - radius)) //check down intersection
                   &&
                   ((rightLimitPlatform3 > center.X - radius && leftLimitPlatform3 < center.X - radius) //check right
                   ||
                   (rightLimitPlatform3 > center.X + radius && leftLimitPlatform3 < center.X + radius)))
            {
                return 3;
            }

            if (((ySuperiorLimit < center.Y + radius && yInferiorLimit > center.Y + radius) //check up
                   ||
                   (ySuperiorLimit < center.Y - radius && yInferiorLimit > center.Y - radius)) //check down intersection
                   &&
                   ((rightLimitPlatform4 > center.X - radius && leftLimitPlatform4 < center.X - radius) //check right
                   ||
                   (rightLimitPlatform4 > center.X + radius && leftLimitPlatform4 < center.X + radius)))
            {
                return 4;
            }

            return 0;

        }

        public float getMiddlePoint(int e1, int e2)
        {
            VElement sumo1 = Elements[e1];
            VElement sumo2 = Elements[e2];
            Vec2 center1 = sumo1.VPoints[sumo1.bodyCenterId].pos;
            Vec2 center2 = sumo2.VPoints[sumo2.bodyCenterId].pos;

            return ((center1.X + center2.X) / 2) / 28;

        }



        public void Render(int Width, int Height, Texture2D brush, Texture2D sumoNormal, Texture2D sumoSleep, Texture2D blueSumo, Texture2D redSumo)
        {

            for (int s = 0; s < Elements.Count; s++)
            {
                checkCollisionOfSumoWithPlatforms(s);
                for (int t = s + 1; t < Elements.Count; t++)
                {
                    checkCollisionBetweenElements(s, t);
                }
                if (s == 0)
                {
                    Elements[s].Render(Width, Height, brush, sumoNormal, sumoSleep, redSumo);
                }
                else {
                    Elements[s].Render(Width, Height, brush, sumoNormal, sumoSleep, blueSumo);
                }
                
            }
        }
    }
}
