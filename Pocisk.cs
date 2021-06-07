using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AstroAUW1
{
    class Pocisk
    {
        Texture2D texture;
        Vector2 position;
        
        private int timeSinceLastFrame = 0;
        public bool wystrzelony = false;
        
        // prędkość animacji ( im mniejsza watość tym szybciej)
        private int milisecondsPerFrame = 1000;
        public Pocisk(Texture2D pocisk)
        {
            
            texture = pocisk;
            position= new Vector2(600,0);
        }
        
        public Rectangle pociskBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width / 3, texture.Height); }
        }
        public void Update(GameTime gameTime)
        {
            
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame>milisecondsPerFrame)
            {
                timeSinceLastFrame -= milisecondsPerFrame;
                timeSinceLastFrame = 0;
                if (wystrzelony)
                {
                    position.Y -= 100;
                }
                if (position.Y < 0 - texture.Height)
                {
                    strzal();
                }

                    
            }
            
            
            
        }
        public void strzal()
        {
            wystrzelony = !wystrzelony;
        }
        public void setPosition(Vector2 pos)
        {
            if(!wystrzelony)
            position = pos;
        }
      
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectPocisk = new Rectangle((int)position.X, (int)position.Y,texture.Width, texture.Height);
            spriteBatch.Draw(texture, rectPocisk, Color.White);
            
        }
        public Vector2 GetPosition()
        {

            return position;
        }
        public Vector2 GetSize() 
        { 
            return new Vector2(texture.Width, texture.Height); 
        }




    }
}