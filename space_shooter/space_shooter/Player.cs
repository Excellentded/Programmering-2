using Microsoft.Xna.Framework;               // tillagd
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    /// <summary>
    /// En klass för att skapa ett spelareobjekt. Klassen ska
    /// hantera spelarens rymdskepp (sprite) och ta emot
    /// tangenttryckningar för att ändra rymdskeppets position
    /// </summary>
    public class Player : PhysicalObject
    {
        int points = 0;                       // Spelarens poäng
        List<Bullet> bullets;                 // Alla skott
        Texture2D bulletTexture;              // Skottets bild
        double timeSinceLastBullet = 0;       // I millisekunder

        /// <summary>
        /// Spelarens konstruktor för att skapa spelareobjektet.
        /// </summary>
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture)
            : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        /// <summary>
        /// Update(), tar emot tangenttryckningar och uppdaterar spelarens position
        /// </summary>
        public void Update(GameWindow window, GameTime gameTime)
        {
            // Läs in tangentbordstryckningar
            KeyboardState keyboardState = Keyboard.GetState();

            // Avbryt spelet och gå till menyn
            if (keyboardState.IsKeyDown(Keys.Escape))
                isAlive = false;

            // Flytta rymdskeppet efter tangenttryckningarna (om det inte är på väg ut från kanten)
            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.Up))
                    vector.Y -= speed.Y;
            }

            // Kontrollera ifall rymdskeppet har åkt ut från kanten
            if (vector.X < 0)
                vector.X = 0;
            if (vector.X > window.ClientBounds.Width - texture.Width)
                vector.X = window.ClientBounds.Width - texture.Width;
            if (vector.Y < 0)
                vector.Y = 0;
            if (vector.Y > window.ClientBounds.Height - texture.Height)
                vector.Y = window.ClientBounds.Height - texture.Height;

            // Spelaren vill skjuta
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
                {
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y);
                    bullets.Add(temp);
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // Flytta på alla skott
            foreach (Bullet b in bullets.ToList())
            {
                b.Update();
                if (!b.IsAlive)
                    bullets.Remove(b);
            }
        }

        /// <summary>
        /// Draw, ritar ut bilden på skärmen
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }

        /// <summary>
        /// Reset(), återställer spelaren för ett nytt spel
        /// </summary>
        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;

            bullets.Clear();
            timeSinceLastBullet = 0;

            points = 0;
            isAlive = true;
        }

        /// <summary>
        /// Egenskaper för Player
        /// </summary>
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public List<Bullet> Bullets { get { return bullets; } }

        /// <summary>
        /// Bullet, en klass för att skapa skott
        /// </summary>
        public class Bullet : PhysicalObject
        {
            public Bullet(Texture2D texture, float X, float Y)
                : base(texture, X, Y, 0, 3f)
            {
            }

            public void Update()
            {
                vector.Y -= speed.Y;
                if (vector.Y < 0)
                    isAlive = false;
            }
        }
    }
}
