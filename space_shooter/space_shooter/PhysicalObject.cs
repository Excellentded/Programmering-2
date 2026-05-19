using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceShooter
{
    abstract public class PhysicalObject : MovingObject
    {
        protected bool isAlive = true;
        protected SpriteEffects effects = SpriteEffects.None;
        protected float scale = 1f;


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Vector2(X, Y),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                effects,
                0f
            );
        }

        public bool CheckCollision(PhysicalObject other) // denna är ändrad för dronen behövde scaleas ner och då behövde jag göra så hitboxen följde med
        {
            Rectangle myRect = new Rectangle(
                (int)X,
                (int)Y,
                (int)(Width * scale),
                (int)(Height * scale)
            );

            Rectangle otherRect = new Rectangle(
                (int)other.X,
                (int)other.Y,
                (int)(other.Width * other.scale),
                (int)(other.Height * other.scale)
            );

            return myRect.Intersects(otherRect);
        }


        public bool IsAlive 
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
    }
}
