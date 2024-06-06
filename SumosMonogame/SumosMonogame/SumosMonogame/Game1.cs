using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SumosMonogame.Controls;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SumosMonogame
{
    public class Game1 : Game
    {
        KeyboardState state;
        private GraphicsDeviceManager g;
        public SpriteBatch _spriteBatch;

        SoundEffect golpe, knock, salto;
        SoundEffectInstance instGolpe, instKnock, instSalto;
        Texture2D iglus, starNight, cmpch, palmera,clouds, heart, Temples, sumoSleep, sumoNormal, jumpAtivated, jumpDesactivated, brick, button, redSumo, blueSumo, gameOver, arrow;
        SpriteFont titles;
  
        private float templesSpeed = 3.5f;
        private float cloudsSpeed = 0.5f;
        Vector2 cloudsPosition,cloudsPosition2;
        Vector2 templesPosition, templesPosition2;
        MouseState mState;
        Canvas canvas;
        Scene scene;
        Juego juego;
        Map map;
        DateTime lastWKeyPressTime;
        DateTime lastIKeyPressTime;
        int timeAfterLoosingSumo1;
        int timeAfterLoosingSumo2;
        int lifes = 3;
        float delta;
        Texture2D brush;
        GraphicsDevice graphicsDevice;
        float angle;
        Vector2 origin;
        public Vector2 pos;
        int timeUntilNextRound;

        bool aKey, wKey, dKey, upKey, leftKey, rightKey;
       


        SpriteBatch spriteBatchBackground;
        SpriteBatch spriteBatchForeground;

        private List<Component> _components;



        TimeSpan delayTime = TimeSpan.FromSeconds(1.5);

        int width, height;
        
        



        public Game1()
        {
            g = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //FULL SCREEN
            g.IsFullScreen = true;
            width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int div = 1;
            g.PreferredBackBufferWidth = width / div;
            g.PreferredBackBufferHeight = height / div;
            g.ApplyChanges();


            wKey = true; dKey = true; aKey = true; upKey = true; leftKey = true; rightKey = true;


            



        }

        protected override void Initialize()
        {

            graphicsDevice = GraphicsDevice;

            //TEXTURES
            brush = new Texture2D(GraphicsDevice, 1, 1);
            brush.SetData(new Color[] { Color.Gray });

            //CLASES
            canvas = new Canvas(width, height, _spriteBatch, graphicsDevice,1);
            scene = new Scene(width, height, graphicsDevice);
            scene.AddSumo(new Vec2(720, 200), 60);
            scene.AddSumo(new Vec2(1080, 200), 60);
            juego = new Juego(lifes, graphicsDevice);
            delta = 0;

            timeAfterLoosingSumo1 = 0;
            timeAfterLoosingSumo2 = 0;
            timeUntilNextRound = 300;
            
            base.Initialize();
        }






        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //TEXTURAS
            cmpch = Content.Load<Texture2D>("CampecheSky");
            palmera = Content.Load<Texture2D>("palmeras");
            clouds = Content.Load<Texture2D>("Clouds");
            heart = Content.Load<Texture2D>("heart");
            Temples = Content.Load<Texture2D>("Temples");
            iglus = Content.Load<Texture2D>("starNight");
            starNight = Content.Load<Texture2D>("startNight");
            sumoSleep = Content.Load<Texture2D>("sumoSleep");
            sumoNormal = Content.Load<Texture2D>("sumoNormal");
            jumpAtivated = Content.Load<Texture2D>("jumpActivated");
            brick = Content.Load<Texture2D>("brick");
            jumpDesactivated = Content.Load<Texture2D>("jumpDesactivated");
            button = Content.Load<Texture2D>("buttonStock1h");
            redSumo = Content.Load<Texture2D>("redSumo1");
            blueSumo = Content.Load<Texture2D>("blueSumo1");
            gameOver = Content.Load<Texture2D>("GO6");
            arrow = Content.Load<Texture2D>("arrow");
            titles = Content.Load<SpriteFont>("galleryFont");
            _components = new List<Component>();


            //SONIDOS
            golpe = Content.Load<SoundEffect>("golpe");
            instGolpe = golpe.CreateInstance();

            knock = Content.Load<SoundEffect>("knock3");
            instKnock = knock.CreateInstance();

            salto = Content.Load<SoundEffect>("salto2");
            instSalto = salto.CreateInstance();

     


            //VARIABLES
            angle = 0f;
            origin = new Vector2(0, 0);
            pos = new Vector2(0, 0);

            cloudsPosition = new Vector2(0, 0);
            cloudsPosition2 = new Vector2(-width, 0);
            templesPosition = new Vector2(0, 300);
            templesPosition2 = new Vector2(-width, 300);


            //SPRITES
            spriteBatchBackground = new SpriteBatch(GraphicsDevice);
            spriteBatchForeground = new SpriteBatch(GraphicsDevice);


            spriteBatchBackground.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            spriteBatchBackground.End();

            spriteBatchForeground.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            spriteBatchForeground.End();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //mState = Mouse.GetState();

            //PARALAX
            cloudsPosition.X += cloudsSpeed;
            cloudsPosition2.X += cloudsSpeed;
            // Si las nubes se salen del lado derecho de la pantalla, reaparécelos en el lado izquierdo
            if (cloudsPosition.X > width)
            {
                cloudsPosition.X = -width; // Ajusta la posición utilizando el módulo
            }
            if (cloudsPosition2.X > width)
            {
                cloudsPosition2.X = -width; // Ajusta la posición utilizando el módulo
            }
            templesPosition.X += templesSpeed;
            templesPosition2.X += templesSpeed;
            // Si los templos se salen del lado derecho de la pantalla, reaparécelos en el lado izquierdo
            if (templesPosition.X > width)
            {
                templesPosition.X = -width; // Ajusta la posición utilizando el módulo
            }
            if (templesPosition2.X > width)
            {
                templesPosition2.X = -width; // Ajusta la posición utilizando el módulo
            }
            //UPDATE SUMOS
            inputSumo1();
            inputSumo2();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            Texture2D p1;
            Texture2D p2;

            //DIBUJAR PARALAX EN EL FONDO
            spriteBatchBackground.Begin();
            float scale = (float)width / Temples.Width;
            // spriteBatchBackground.Draw(clouds, pos, new Rectangle(0, 0, screenWidth, screenHeight), Color.White, angle, Vector2.Zero, 1, SpriteEffects.None, 0.9f); 
            switch (canvas.map.level)
            {
                case 1:
                    spriteBatchBackground.Draw(clouds, new Rectangle((int)cloudsPosition.X, (int)cloudsPosition.Y, width, height), Color.White);
                    spriteBatchBackground.Draw(clouds, new Rectangle((int)cloudsPosition2.X, (int)cloudsPosition2.Y, width, height), Color.White);
                    

                    spriteBatchBackground.Draw(Temples, templesPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    spriteBatchBackground.Draw(Temples, templesPosition2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    break;
                case 2:
                    spriteBatchBackground.Draw(cmpch, new Rectangle((int)cloudsPosition.X, (int)cloudsPosition.Y, width, height), Color.White);
                    spriteBatchBackground.Draw(cmpch, new Rectangle((int)cloudsPosition2.X, (int)cloudsPosition2.Y, width, height), Color.White);
                    scale = (float)width / palmera.Width;

                    spriteBatchBackground.Draw(palmera, templesPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    spriteBatchBackground.Draw(palmera, templesPosition2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    break;
                default:
                    spriteBatchBackground.Draw(starNight, new Rectangle((int)cloudsPosition.X, (int)cloudsPosition.Y, width, height), Color.White);
                    spriteBatchBackground.Draw(starNight, new Rectangle((int)cloudsPosition2.X, (int)cloudsPosition2.Y, width, height), Color.White);
                    scale = (float)width / iglus.Width;

                    spriteBatchBackground.Draw(iglus, templesPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    spriteBatchBackground.Draw(iglus, templesPosition2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);
                    break;
            }
            
            spriteBatchBackground.End();
            


            //DIBUJAR CANVAS

            _spriteBatch.Begin();
            canvas.Render(scene, delta, juego, lastWKeyPressTime, lastIKeyPressTime, delayTime, brush, heart, arrow, brick, sumoNormal, sumoSleep, blueSumo, redSumo, scene.Elements[0].VPoints[scene.Elements[0].headCenterId].pos, scene.Elements[1].VPoints[scene.Elements[1].headCenterId].pos);
            _spriteBatch.End();

            
           
            //NO FUNCIONAN BIEN LAS VIDAS
            
            delta += 0.001f;
            //check if a sumo lost the round
            if (scene.Elements[0].defeated)
            {
                //set a timer to initiate the game again
                timeAfterLoosingSumo1++;
                if (timeAfterLoosingSumo1 >= 100)
                {
                    //despues agregar encapsulacion
                    juego.decreaseLifesSumo1();
                    if (juego.getLifesSumo1() > 0 && juego.getLifesSumo2() > 0)
                    {
                        juego.nextRound();
                        scene.Elements = new List<VElement>();
                        scene.AddSumo(new Vec2(720, 200), 60);
                        scene.AddSumo(new Vec2(1080, 200), 60);
                        timeAfterLoosingSumo1 = 0;
                        timeAfterLoosingSumo2 = 0;

                    }
                    else
                    {
                        spriteBatchBackground.Begin();  
                        spriteBatchBackground.Draw(clouds, new Rectangle(0, 0, width, height), Color.White); ;


                        //spriteBatchBackground.DrawString(titles, "FIN DEL JUEGO", new Vector2(450, height / 2), Color.Black);

                        spriteBatchBackground.Draw(gameOver, new Vector2(350, 0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);

                        spriteBatchBackground.End();
                        timeUntilNextRound--;
                        if (timeUntilNextRound <= 0)
                        {
                            scene.Elements = new List<VElement>();
                            scene.AddSumo(new Vec2(720, 200), 60);
                            scene.AddSumo(new Vec2(1080, 200), 60);
                            timeAfterLoosingSumo1 = timeAfterLoosingSumo2 = 0;
                            canvas.map.changeLevel();
                            juego = new Juego(lifes, graphicsDevice);
                            timeAfterLoosingSumo1 = 0;
                            timeAfterLoosingSumo2 = 0;
                            timeUntilNextRound = 300;

                        }

                        //return;
                    }

                }
            }

            if (scene.Elements[1].defeated)
            {
                timeAfterLoosingSumo2++;
                if (timeAfterLoosingSumo2 >= 100)
                {
                    juego.decreaseLifesSumo2();
                    if (juego.getLifesSumo1() > 0 && juego.getLifesSumo2() > 0)
                    {

                        juego.nextRound();
                        scene.Elements = new List<VElement>();
                        scene.AddSumo(new Vec2(720, 200), 60);
                        scene.AddSumo(new Vec2(1080, 200), 60);
                        timeAfterLoosingSumo1 = 0;
                        timeAfterLoosingSumo2 = 0;

                    }
                    else
                    {
                        spriteBatchBackground.Begin();
                        spriteBatchBackground.Draw(clouds, new Rectangle(0, 0, width, height), Color.White); ;


                        //spriteBatchBackground.DrawString(titles, "FIN DEL JUEGO", new Vector2(450, height / 2), Color.Black);

                        spriteBatchBackground.Draw(gameOver, new Vector2(350, 0), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);

                        spriteBatchBackground.End();
                        timeUntilNextRound--;
                        if (timeUntilNextRound<=0)
                        {
                            scene.Elements = new List<VElement>();
                            scene.AddSumo(new Vec2(720, 200), 60);
                            scene.AddSumo(new Vec2(1080, 200), 60);
                            timeAfterLoosingSumo1 = timeAfterLoosingSumo2 = 0;
                            canvas.map.changeLevel();
                            juego = new Juego(lifes, graphicsDevice);
                            timeAfterLoosingSumo1 = 0;
                            timeAfterLoosingSumo2 = 0;
                            timeUntilNextRound = 300;

                        }

                        //return;
                    }
                }
            }
            

           

            base.Draw(gameTime);
        }




        public void inputSumo1()
        {
            state = Keyboard.GetState();

            if (state.IsKeyUp(Keys.W)) { wKey = true; }
            if (state.IsKeyUp(Keys.A)) { aKey = true; }
            if (state.IsKeyUp(Keys.D)) { dKey = true; }

            if (!scene.Elements[0].knocked)
            {
                



                if (state.IsKeyDown(Keys.W))
                {
                    if (wKey && DateTime.Now - lastWKeyPressTime > delayTime)
                    {

                        scene.Elements[0].VPoints[0].old = new Vec2(scene.Elements[0].VPoints[0].pos.X, scene.Elements[0].VPoints[0].pos.Y + 350);
                        wKey = false;
                        lastWKeyPressTime = DateTime.Now;
                        instSalto.Pan = 1;
                        instSalto.Volume = 0.5f;
                        instSalto.Play();
                    }

                    //SoundManagercs.soundEffect.Play(.5f,0,-1);
                    
                }

                if (state.IsKeyDown(Keys.A))
                {
                    if (aKey) { scene.Elements[0].VPoints[0].old = new Vec2(scene.Elements[0].VPoints[0].pos.X + 50, scene.Elements[0].VPoints[0].pos.Y); aKey = false;
                        instGolpe.Pan = 1;
                        instGolpe.Volume = 0.5f;
                        instGolpe.Play();
                    }
                    
                }

                if (state.IsKeyDown(Keys.D))
                {
                    if (dKey) { scene.Elements[0].VPoints[0].old = new Vec2(scene.Elements[0].VPoints[0].pos.X - 50, scene.Elements[0].VPoints[0].pos.Y); dKey = false;
                        instGolpe.Pan = 1;
                        instGolpe.Volume = 0.5f;
                        instGolpe.Play();
                    }
                }


            }
            else {
                instKnock.Pan = 1;
                instKnock.Volume = 0.5f;
                instKnock.Play();
            }

  
        }



        public void inputSumo2()
        {
            state = Keyboard.GetState();

            if (state.IsKeyUp(Keys.Up)) { upKey = true; }
            if (state.IsKeyUp(Keys.Left)) { leftKey = true; }
            if (state.IsKeyUp(Keys.Right)) { rightKey = true; }

            if (!scene.Elements[1].knocked)
            {
                if (state.IsKeyDown(Keys.Up))
                {
                    if (upKey && DateTime.Now - lastIKeyPressTime > delayTime)
                    {
                        scene.Elements[1].VPoints[0].old = new Vec2(scene.Elements[1].VPoints[0].pos.X, scene.Elements[1].VPoints[0].pos.Y + 350);
                        upKey = false;
                        lastIKeyPressTime = DateTime.Now;
                        instSalto.Pan = 1;
                        instSalto.Volume = 0.5f;
                        instSalto.Play();
                    }

                    //SoundManagercs.soundEffect.Play(.5f,0,-1);

                }

                if (state.IsKeyDown(Keys.Left))
                {
                    if (leftKey) { scene.Elements[1].VPoints[0].old = new Vec2(scene.Elements[1].VPoints[0].pos.X + 50, scene.Elements[1].VPoints[0].pos.Y); leftKey = false;
                        instGolpe.Pan = 1;
                        instGolpe.Volume = 0.5f;
                        instGolpe.Play();
                    }
                }

                if (state.IsKeyDown(Keys.Right))
                {
                    if (rightKey) { scene.Elements[1].VPoints[0].old = new Vec2(scene.Elements[1].VPoints[0].pos.X - 50, scene.Elements[1].VPoints[0].pos.Y); rightKey = false;
                        instGolpe.Pan = 1;
                        instGolpe.Volume = 0.5f;
                        instGolpe.Play();
                    }
                }


            }
            else {
                instKnock.Pan = 1;
                instKnock.Volume = 0.5f;
                instKnock.Play();
            }


        }
    }
}
