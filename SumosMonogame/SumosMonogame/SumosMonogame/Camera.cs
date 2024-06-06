using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumosMonogame
{
    public class Camera
    {
        public PointF point;
        public PointF previousPoint;

        public Camera(float x, float y)
        {
            point = new PointF(x, y);
            previousPoint = point;
        }

    }
}
