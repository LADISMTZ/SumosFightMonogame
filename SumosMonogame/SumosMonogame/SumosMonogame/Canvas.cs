using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace SumosMonogame
{
    internal class Canvas
    {

        private GraphicsDevice graphicsDevice;

        int Width, Height;
        byte[] bits;
        public float camerax;
        int pixelFormatSize, stride;
        int bytesPerPixel, heightInPixels, widthInBytes;
        public SpriteBatch g;



        public Camera camera;
        public Map map;

        public Canvas(int width, int height, SpriteBatch graphics, GraphicsDevice graphicsDevice, int level)
        {
            this.graphicsDevice = graphicsDevice;
            Width = width;
            Height = height;    
            camera = new Camera(1000, 0);
            map = new Map(graphicsDevice,level);
            g = graphics;
        }





        
        public void Render(Scene scene, float deltaTime, Juego juego, DateTime lastWKeyPressTime, DateTime lastIKeyPressTime, TimeSpan delayTime, Texture2D brush, Texture2D heart, Texture2D arrow, Texture2D brick, Texture2D sumoNormal, Texture2D sumoSleep, Texture2D blueSumo, Texture2D redSumo, Vec2 pos1, Vec2 pos2)
        {


            scene.Render(Width, Height, brush, sumoNormal, sumoSleep, blueSumo, redSumo);

            camera.previousPoint.X = camera.point.X;
            camera.point.X = scene.getMiddlePoint(0, 1);
            camerax=camera.point.X;
            int[,] matriz = map.Draw(camera.point, Width, Height, brick);
            scene.extractLimits(matriz);
            juego.Render(Width, Height, lastWKeyPressTime, lastIKeyPressTime, delayTime, heart, arrow, pos1, pos2);

            //los bloques

        }

        
    }
}
