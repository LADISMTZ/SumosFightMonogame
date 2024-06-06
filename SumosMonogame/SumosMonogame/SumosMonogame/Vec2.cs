using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumosMonogame
{
    public class Vec2
    {
        public float X, Y;

        public Vec2(float x, float y)
        {
            this.Y = (float)y;
            this.X = (float)x;

        }


        public static Vec2 operator -(Vec2 v)
        {
            return new Vec2(-v.X, -v.Y);
        }


        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X, a.Y - b.Y);
        }

        public static Vec2 operator *(float a, Vec2 v)
        {
            return new Vec2(a * v.X, a * v.Y);
        }

        public static Vec2 operator *(Vec2 v, float a)
        {
            return new Vec2(a * v.X, a * v.Y);
        }

        public static Vec2 operator /(Vec2 v, float a)
        {
            return new Vec2(v.X / a, v.Y / a);
        }

        public float MagSQR()
        {
            float f = (X * X) + (Y * Y);
            return f;
        }

        public float Length()
        {
            float f = (float)Math.Sqrt((X * X) + (Y * Y));
            return f;
        }


        public float Distance(Vec2 a)
        {
            float f = (float)Math.Sqrt(Math.Pow(X - a.X, 2) + Math.Pow(Y - a.Y, 2));
            return f;
        }


    }
}
