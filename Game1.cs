using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace AstroAUW1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //nowe pola prywatne
        Rakieta gracz;
        Meteor enemy,enemy2;
        Pocisk strzal;
        SoundEffect wybuch;
        Texture2D rakieta, control, niebo ,meteor,pocisk, gameover;
        SpriteFont counter;
        bool koniec_gry = false;
        int pkt = 0;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
            //ApplicationView.GetForCurrentView().TryResizeView(new Size(480, 800));
            //graphics.IsFullScreen = false; 
            //graphics.PreferredBackBufferWidth = 480; 
            //graphics.PreferredBackBufferHeight = 800; 
            //graphics.ApplyChanges();
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            // TODO: use this.Content to load your game content here
            rakieta = this.Content.Load<Texture2D>("AnimRakiety");
            control = this.Content.Load<Texture2D>("control");
            niebo = this.Content.Load<Texture2D>("niebo");
            meteor = this.Content.Load<Texture2D>("meteor");
            pocisk = this.Content.Load<Texture2D>("pocisk2D");
            wybuch = this.Content.Load<SoundEffect>("wybuch");
            gameover = this.Content.Load<Texture2D>("gameover");
            counter = this.Content.Load<SpriteFont>("counter");
            enemy = new Meteor(meteor);
            enemy2 = new Meteor(meteor);
            gracz = new Rakieta(rakieta);
            strzal = new Pocisk(pocisk);
            
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                gracz.MoveL();
               
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                
                gracz.MoveR();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                
                gracz.MoveU();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                gracz.MoveD();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !strzal.wystrzelony)
            {
                strzal.strzal();
            }
            if (!strzal.wystrzelony)
            {
                strzal.setPosition(gracz.GetPosition());
            }
            else
            {
                strzal.Update(gameTime);
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && koniec_gry==true)
            {
                koniec_gry = !koniec_gry;
                
                pkt = 0;
                gracz.startPosition();
                enemy.startPosition();
                enemy2.startPosition();
                strzal.wystrzelony = false;
            }
            enemy.Update(gameTime);
            enemy2.Update(gameTime);
            gracz.Update(gameTime);
            detekcjaKolizji();
            base.Update(gameTime);
        }
        public void detekcjaKolizji()
        {
            if (gracz.graczBox.Intersects(enemy.meteorBox)||gracz.graczBox.Intersects(enemy2.meteorBox))
            {
                wybuch.Play();
                if (koniec_gry==false)
                koniec_gry = !koniec_gry;
                
            }
            if (strzal.pociskBox.Intersects(enemy.meteorBox) && strzal.wystrzelony)
            {
                strzal.strzal();
                enemy.respawn();
                pkt++;
            }
            if(strzal.pociskBox.Intersects(enemy2.meteorBox) && strzal.wystrzelony)
            {
                strzal.strzal();
                enemy2.respawn();
                pkt++;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Rectangle rectGracza = new Rectangle((int)gracz.GetPosition().X,
            //    (int)gracz.GetPosition().Y, rakieta.Width, rakieta.Height);
            //spriteBatch.Draw(rakieta, rectGracza, Color.White);

            if(!koniec_gry)
            {
                spriteBatch.Draw(niebo, new Vector2(0, 0), Color.White);
                gracz.Draw(spriteBatch);
                enemy.Draw(spriteBatch);
                enemy2.Draw(spriteBatch);
                strzal.Draw(spriteBatch);
                spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
                spriteBatch.DrawString(counter,"Punkty : "+pkt, new Vector2(300,583), Color.Red);
            }else
            {
                spriteBatch.Draw(gameover, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(counter, "Your store : " + pkt, new Vector2(300, 300), Color.White);
            }


            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}