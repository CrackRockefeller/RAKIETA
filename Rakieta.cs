using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AstroAUW1
{
    class Rakieta
    {
        Texture2D texture;
        Vector2 position;
        int nrklatki=0;
        private int timeSinceLastFrame = 0;
        // prędkość animacji ( im mniejsza watość tym szybciej)
        private int milisecondsPerFrame = 100;
        public Rakieta(Texture2D rakieta)
        {
            texture = rakieta;
            position = new Vector2(210, 480);
        }
       
        public Rectangle graczBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width / 6, texture.Height); }
        }
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame>milisecondsPerFrame)
            {
                timeSinceLastFrame -= milisecondsPerFrame;
                ++nrklatki;
                timeSinceLastFrame = 0;
                if (nrklatki == 6)
                    nrklatki = 0;
            }
            
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
           
            int szerokośćKlatki = texture.Width / 6;
            Rectangle klatka = new Rectangle(nrklatki * szerokośćKlatki, 0, szerokośćKlatki, texture.Height);
            Rectangle rectGracza = new Rectangle((int)position.X, (int)position.Y, szerokośćKlatki, texture.Height);
            spriteBatch.Draw(texture, rectGracza, klatka, Color.White);
            
        }
        public Vector2 GetPosition()
        {

            return position;
        }
        public void startPosition()
        {
            position= new Vector2(210, 480);
        }
        public void MoveU()
        {
            position.Y -= 5;
            if (position.Y <= 0)
                position.Y = 0;
        }
        public void MoveD()
        {
            position.Y += 5;
            if (position.Y >= 480)
                position.Y = 480;
        }
        public void MoveL()
        {
            position.X -= 5;
            if (position.X <= 0)
                position.X = 0;
        }
        public void MoveR()
        {
            position.X += 5;
            if (position.X >= 410)
                position.X = 410;
        }
        public Vector2 GetSize()
        {
            return new Vector2(texture.Width / 6, texture.Height);
        }
        


    }
}