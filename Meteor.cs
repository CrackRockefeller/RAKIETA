using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AstroAUW1
{
    class Meteor
    {
        Texture2D texture;
        Vector2 position;
        int nrklatki=0;
        private int timeSinceLastFrame = 0;
        Random generujLL = new Random();
        // prędkość animacji ( im mniejsza watość tym szybciej)
        private int milisecondsPerFrame = 1000;
        public Meteor(Texture2D meteor)
        {
            
            texture = meteor;
            position = new Vector2(generujLL.Next(0,300),0);
        }
        public Rectangle meteorBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width / 3, texture.Height); }
        }
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame>milisecondsPerFrame)
            {
                timeSinceLastFrame -= milisecondsPerFrame;
                ++nrklatki;
                timeSinceLastFrame = 0;
                if (nrklatki == 3)
                    nrklatki = 0;
                position.Y += generujLL.Next(10,80);
                if (position.Y >= 480 + texture.Height)
                    respawn();
            }
            
            
        }
        public void respawn()
        {
            position = new Vector2(generujLL.Next(0, 300), -texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
           
            int szerokośćKlatki = texture.Width / 3;
            Rectangle klatka = new Rectangle(nrklatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectMeteor = new Rectangle((int)position.X, (int)position.Y, szerokośćKlatki, texture.Height);
            spriteBatch.Draw(texture, rectMeteor, klatka, Color.White);
            
        }
        public Vector2 GetPosition()
        {

            return position;
        }
        public void startPosition()
        {
            position= new Vector2(generujLL.Next(0, 300), 0);
            Thread.Sleep(1000);
        }
        public Vector2 GetSize() 
        { 
            return new Vector2(texture.Width / 3, texture.Height); 
        }




    }
}