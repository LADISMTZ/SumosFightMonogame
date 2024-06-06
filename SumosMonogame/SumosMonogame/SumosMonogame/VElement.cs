using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumosMonogame
{
    public class VElement
    {

        public List<VPoint> VPoints;
        public List<VPole> VPoles;
        public float xMin, xMax, yMin, yMax;
        public float width, height;
        public int headCenterId;
        public int bodyCenterId;
        public bool knocked;
        public float maxTimeKnocked;
        public float timeKnocked;
        public bool defeated;
        
        List<Vec2> bodyPoints;
        private GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;




        //Brush brush;

        public VElement(Vec2 center, int height, int width, GraphicsDevice graphicsDevice)
        {
            
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);

            VPoints = new List<VPoint>();
            VPoles = new List<VPole>();
            xMin = center.X - width;
            xMax = center.X + width;
            yMin = center.Y + height;
            yMax = center.Y - height;
            this.width = width;
            this.height = height;
            headCenterId = 0;
            bodyCenterId = 0;
            knocked = false;
            timeKnocked = 0;
            maxTimeKnocked = 70;
            defeated = false;
            bodyPoints = new List<Vec2>();
            //this.brush = brush;
            //sumoNormalHead = Resource1.sumoNormal;
            //sumoSleepHead = Resource1.sumoSleep;
        }

        public void addPoint(float x, float y, int id, bool pin, int sizeOfRadius, float mass, Vec2 gravity)
        {
            VPoints.Add(new VPoint(x, y, id, pin, sizeOfRadius, mass, gravity));
        }

        public void addPole(int i1, int i2, float length, float stiffness)
        {
            VPoles.Add(new VPole(VPoints[i1], VPoints[i2], length, stiffness));

        }
        public void Render(int Canvasw, int Canvash, Texture2D brush, Texture2D sumoNormal, Texture2D sumoSleep, Texture2D sumoBody)
        {

            

            bodyPoints = new List<Vec2>();
            //render points
            for (int i = 0; i < VPoints.Count; i++)
            {
                //TEMPORAL PARA CAMBIAR EL COLOR CUANDO KNOCKEADO
                if (knocked && i <= headCenterId)
                {
                    //VPoints[i].brush = new SolidBrush(Color.White);
                }
                else
                {
                    //VPoints[i].brush = new SolidBrush(Color.Orange);
                }
                VPoints[i].Render(Canvasw, Canvash, spriteBatch, VPoints);
                if (VPoints[i].inFloor)
                {
                    defeated = true;
                }
                //Posicion cabeza con respecto a punto del centro
                xMin = VPoints[0].pos.X - width;
                xMax = VPoints[0].pos.X + width;
                yMin = VPoints[0].pos.Y + height;
                yMax = VPoints[0].pos.Y - height;


                if (i > bodyCenterId && i < headCenterId)
                {
                    bodyPoints.Add(VPoints[i].pos);
                }


            }
            //AUN NO SE COMO DIBUJAR EL POLIGONO PODRIA SER CON UN SPRITE

            spriteBatch.Begin();
            //spriteBatch.Draw(redSumo, new Vector2(200, 200), Color.White);
            spriteBatch.Draw(sumoBody, new Rectangle((int)VPoints[bodyCenterId].pos.X-60, (int)VPoints[bodyCenterId].pos.Y - 60, 110, 110), Color.White);
            spriteBatch.End();


            /*
            
            Vector2[] points = bodyPoints.Select(p => new Vector2((int)p.X, (int)p.Y)).ToArray();
            List<Vector2> triangulos = new List<Vector2>();
            for (int i = 1; i < points.Length - 1; i++)
            {
                triangulos.Add(points[0]);
                triangulos.Add(points[i]);
                triangulos.Add(points[i + 1]);
            }

            Vector2[] triangulosArray = triangulos.ToArray();

            spriteBatch.Begin();


            if (triangulosArray.Length >= 3 && triangulosArray.Length % 3 == 0)
            {
                // Dibuja los triángulos si el tamaño del arreglo es válido
                //graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangulosArray, 0, triangulosArray.Length / 3);
            }

            spriteBatch.End();
            
           */
            



            //render poles
            for (int i = 0; i < VPoles.Count; i++)
            {
                VPoles[i].Render(spriteBatch, Canvasw, Canvash);

            }


            //SE VA A CAMBIAR DE LUGAR
            
            spriteBatch.Begin();
            //if knocked check if it can be not knocked
            if (knocked)
            {
                VPoints[headCenterId].pos.X = VPoints[bodyCenterId].pos.X;
                VPoints[headCenterId].pos.Y = VPoints[bodyCenterId].pos.Y - VPoints[bodyCenterId].radius - 30;
                timeKnocked++;
                if (timeKnocked >= maxTimeKnocked)
                {
                    knocked = false;
                    timeKnocked = 0;
                }
                //g.DrawImage(sumoSleepHead, VPoints[headCenterId].pos.X - 30, VPoints[headCenterId].pos.Y - 30, 60, 60);

                spriteBatch.Draw(sumoSleep, new Rectangle((int)VPoints[headCenterId].pos.X - 40, (int)VPoints[headCenterId].pos.Y - 35, 70, 70), Color.White);
            }
            else
            {
                VPoints[headCenterId].pos.X = VPoints[bodyCenterId].pos.X;
                VPoints[headCenterId].pos.Y = VPoints[bodyCenterId].pos.Y - VPoints[bodyCenterId].radius - 40;
                //g.DrawImage(sumoNormalHead, VPoints[headCenterId].pos.X - 30, VPoints[headCenterId].pos.Y - 30, 60, 60);
                spriteBatch.Draw(sumoNormal, new Rectangle((int)VPoints[headCenterId].pos.X - 40, (int)VPoints[headCenterId].pos.Y - 35, 70, 70), Color.White);
            }
            spriteBatch.End();
            
        }
    }
}
