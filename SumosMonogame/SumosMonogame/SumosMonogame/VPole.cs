using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumosMonogame
{
    public class VPole
    {
        VPoint startPoint;
        VPoint endPoint;
        Vec2 dx;
        Vec2 dy;
        Vec2 dxy;
        Vec2 offset;
        float stiffness;
        float damp;
        float length;
        float tot;
        float m1;
        float m2;
        float dis;
        float diff;
        float dist;
        //Pen brush;


        public VPole(VPoint p1, VPoint p2, float length, float stiffness)
        {

            startPoint = p1;
            endPoint = p2;
            this.stiffness = stiffness;
            damp = 0.05f;
            //length = startPoint.pos.Distance(endPoint.pos);
            //this.brush = brush;
            tot = startPoint.mass + endPoint.mass;
            m1 = endPoint.mass / tot;
            m2 = startPoint.mass / tot;

            if (length == 0)
            {

                this.length = startPoint.pos.Distance(endPoint.pos);

            }
            else
            {

                this.length = length;

            }

        }//end VerletStick

        public void Update()
        {

            float tot;
            // calculate the distance between masses
            dxy = endPoint.pos - startPoint.pos;
            //dx = endPoint.pos.x - startPoint.pos.x;
            //dy = endPoint.pos.y - startPoint.pos.y;

            // pythagoras theoremlet
            dist = dxy.Length();
            //dist = Math.sqrt(dx * dx + dy * dy);

            // calculate the resting distance betwen thedots
            diff = (length - dist) / dist * stiffness;

            // getting the offset of the points
            offset = (dxy * diff);
            //offset.x = dx * diff * 0.5;
            //offset.y = dy * diff * 0.5;

            // calculate mass


            tot = startPoint.mass + endPoint.mass;
            m2 = startPoint.mass / tot;
            m1 = endPoint.mass / tot;


            // and finally apply the offset with calculated mass
            startPoint.pos -= offset * m1;
            endPoint.pos += offset * m2;
            /*
            startPoint.pos.x -= offsetx * m1;
            startPoint.pos.y -= offsety * m1;
            endPoint.pos.x += offsetx * m2;
            endPoint.pos.y += offsety * m2;
            */

        }//end update

        public void Render(SpriteBatch g, double width, double height)
        {

            Update();
            //g.DrawLine(brush, startPoint.pos.X, startPoint.pos.Y, endPoint.pos.X, endPoint.pos.Y);

        }//end Render


    }
}
