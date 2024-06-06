using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumosMonogame
{
    public class VPoint
    {

        public Vec2 pos;
        public Vec2 old;
        float friction;
        float groundFriction;
        Vec2 gravity;
        public float radius, diameter;
        Color color;
        public float mass;
        //public Brush brush;
        public bool IsPinned;
        public int Id;
        public bool inFloor;
        //4 limits * visible platforms

        public VPoint(float x, float y, int Id, bool pin, int sizeOfBall, float mass, Vec2 gravity)
        {
            Init(x, y, Id, pin, sizeOfBall, mass, gravity);
        } //end VPoint

        public void Init(float x, float y, int Id, bool pin, int sizeOfBall, float m, Vec2 gravity)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y - 2);
            friction = 0.97f;
            groundFriction = 0.7f;
            this.gravity = gravity;
            radius = sizeOfBall;
            diameter = radius * 2;
            color = Color.Orange;
            //brush = new SolidBrush(color);
            mass = m;
            this.Id = Id;
            IsPinned = pin;
            inFloor = false;
        } //end Init

        public void Update(int CANVAS_WIDTH, int CANVAS_HEIGHT)
        {

            if (!IsPinned)
            {
                Vec2 vel = pos - old;
                vel = vel * (this.friction);
                // if point touches ground set groundFriction
                if (pos.Y >= CANVAS_HEIGHT - radius && vel.MagSQR() > 0.000001)
                {
                    var m = vel.MagSQR();
                    vel.X /= m;
                    vel.Y /= m;
                    vel = vel * (m * this.groundFriction);
                }
                old = new Vec2(this.pos.X, this.pos.Y);
                pos = pos + vel;
                pos = pos + gravity;
            }

        }

        public void Constraint(int CANVAS_WIDTH, int CANVAS_HEIGHT, List<VPoint> pts)
        {
            checkOtherParticleConstraint(pts);

            if (pos.X > CANVAS_WIDTH - radius)
            {
                pos.X = CANVAS_WIDTH - radius;
            }
            if (pos.X < 0)
            {
                pos.X = radius;
            }
            if (pos.Y > CANVAS_HEIGHT - radius)
            {
                pos.Y = CANVAS_HEIGHT - radius;
                inFloor = true;
            }
            if (pos.Y < 0)
            {
                pos.Y = radius;
            }
        }



        public void Render(int CANVAS_WIDTH, int CANVAS_HEIGHT, SpriteBatch g, List<VPoint> pts)
        {
            Update(CANVAS_WIDTH, CANVAS_HEIGHT);
            //regresa el los aqui pero realmente está bien?
            Constraint(CANVAS_WIDTH, CANVAS_HEIGHT, pts);
            /*if (Id == 13)
            {
                g.FillEllipse(brush, pos.X - radius, pos.Y - radius, diameter, diameter);
            }*/
            //g.FillEllipse(brush, pos.X - radius, pos.Y - radius, diameter, diameter);

        }


        public void checkOtherParticleConstraint(List<VPoint> pts)
        {

            int s = this.Id;
            for (int p = this.Id; p < pts.Count; p++)
            {
                VPoint p1 = pts[s];
                VPoint p2 = pts[p];
                checkCollisionBetweenPoints(p1, p2, true);
            }

        }

        public static bool checkCollisionBetweenPoints(VPoint p1, VPoint p2, bool sameElement)
        {
            if (p1.Id == p2.Id && sameElement)// BY ID
            {
                return false;
            }
            if (p1.IsPinned && p2.IsPinned)
                return false;
            Vec2 axis = p1.pos - p2.pos; // vector de direccion
            float dis = axis.Length(); // magnitud
            if (dis < (p1.radius + p2.radius))//COLLISION DETECTED
            {// dividir la fuerza para repartir entre ambas colisiones
                float dif = (dis - (p1.radius + p2.radius)) * .5f;
                Vec2 normal = axis / dis; // normalizar la direccion para tener el vector unitario
                Vec2 res = (dif * normal); // vector resultante
                if (!p1.IsPinned)
                    if (p2.IsPinned)
                        p1.pos -= res * 1.5f;
                    else
                        p1.pos -= res;
                if (!p2.IsPinned)
                    if (p1.IsPinned)
                        p2.pos += res * 1.5f;

                    else
                        p2.pos += res;
                return true;
            }
            return false;
        }
    }
}
