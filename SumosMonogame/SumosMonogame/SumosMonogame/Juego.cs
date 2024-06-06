using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SumosMonogame
{
    public class Juego
    {

        int lifesSumo1;
        int lifesSumo2;
        int round;
        private GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        //Bitmap heart, jumpA, jumpD;

        public Juego(int lifes, GraphicsDevice graphicsDevice)
        {
            //heart = Resource1.heart;
            //jumpA = Resource1.jumpActivated;
            //jumpD = Resource1.jumpDesactivated;
            this.graphicsDevice = graphicsDevice;
            lifesSumo1 = lifes;
            lifesSumo2 = lifes;
            round = 1;
            
        }

        public void Render(int Canvasw, int Canvash, DateTime lastWKeyPressTime, DateTime lastIKeyPressTime, TimeSpan delayTime, Texture2D heart, Texture2D arrow, Vec2 sumoA, Vec2 sumoB)
        {
            int heartPosSumo1, heartPosSumo2;
            heartPosSumo1 = 10;
            heartPosSumo2 = Canvasw - heart.Width - 10;

            spriteBatch = new SpriteBatch(graphicsDevice);
            //por vidas jugador 1 dibuja x corazones
            spriteBatch.Begin();
            for (int i = 0; i < lifesSumo1; i++)
            {
                //g.DrawImage(heart, heartPosSumo1, 50, heart.Width, heart.Height);
                spriteBatch.Draw(heart, new Rectangle(heartPosSumo1, 50, heart.Width, heart.Height), Color.White);
                //spriteBatch.Draw(heart, new Vector2 (heartPosSumo1,50), new Rectangle(heartPosSumo1, 50, heart.Width, heart.Height), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                heartPosSumo1 += 10 + heart.Width;
            }
            //le llega los segundos y revisa si puede saltar o no y dependiendo de eso dibuja x boton
            if (((DateTime.Now - lastWKeyPressTime) > delayTime))
            {
                //g.DrawImage(jumpA, 10, 60 + heart.Height, jumpA.Width, jumpA.Height);
                spriteBatch.Draw(arrow, new Rectangle((int)sumoA.X-15, (int)sumoA.Y-80, arrow.Width, arrow.Height), Color.White);
            }
            //por vidas jugador 2 dibuja x corazones
            for (int i = 0; i < lifesSumo2; i++)
            {
                //g.DrawImage(heart, heartPosSumo2, 50, heart.Width, heart.Height);
                spriteBatch.Draw(heart, new Rectangle(heartPosSumo2, 50, heart.Width, heart.Height), Color.White);
                heartPosSumo2 -= 10 + heart.Width;
            }
            //le llega los segundos y revisa si puede saltar o no y dependiendo de eso dibuja x boton
            if (((DateTime.Now - lastIKeyPressTime) > delayTime))
            {
                //g.DrawImage(jumpA, 10, 60 + heart.Height, jumpA.Width, jumpA.Height);
                spriteBatch.Draw(arrow, new Rectangle((int)sumoB.X-15, (int)sumoB.Y - 80, arrow.Width, arrow.Height), Color.White);
            }

            spriteBatch.End();
        }

        public int getLifesSumo1()
        {
            return lifesSumo1;
        }
        public int getLifesSumo2()
        {
            return lifesSumo2;
        }
        public void setLifesSumo1(int l)
        {
            lifesSumo1 = l;
        }
        public void setLifesSumo2(int l)
        {
           lifesSumo2 = l;
        }

        public void decreaseLifesSumo1()
        {
            lifesSumo1--;
        }

        public void decreaseLifesSumo2()
        {
            lifesSumo2--;
        }

        public void nextRound()
        {
            round++;
        }

        public int getRound() { return round; }

    }
}
