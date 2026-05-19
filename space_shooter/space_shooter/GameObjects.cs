using Microsoft.Xna.Framework;          // tillagd
using Microsoft.Xna.Framework.Graphics; // tillagd

namespace SpaceShooter
{
    /// <summary>
    /// GameObject, en (basklass) för att kunna skapa olika spelobjekt.
    /// Klassen innehåller ett spelobjekts bild och position.
    /// </summary>
    public class GameObject
    {
        protected Texture2D texture;     // Rymdskeppets textur
        protected Vector2 vector;        // Rymdskeppets koordinater

        /// <summary>
        /// Konstruktor för att skapa objektet
        /// </summary>
        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
        }

        /// <summary>
        /// Rita ut bilden på skärmen
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }

        // Egenskaper (properties) för GameObject
        // ============================================
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
    }

    /// <summary>
    /// Klass för objekt som rör sig
    /// </summary>
    public abstract class MovingObject : GameObject
    {
        protected Vector2 speed; // Hastigheten på objektet

        /// <summary>
        /// Konstruktor för att skapa objektet
        /// Anrop bas-klassens konstruktor efter ":"
        /// </summary>
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
    }
}
