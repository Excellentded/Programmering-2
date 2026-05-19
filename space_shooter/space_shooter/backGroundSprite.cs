using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceShooter
{
    // Klass som sköter bakgrundsbilder, turbaserat för en bakgrundsbild i taget.
    public class BackgroundSprite : GameObject
    {
        // Konstruktor
        public BackgroundSprite(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {
        }

        // Uppdatera positionen med avseende på window
        public void Update(GameWindow window, int nrBackgroundsY)
        {
            // Flytta bakgrunden
            if (vector.Y > window.ClientBounds.Height)
            {
                // Flytta bakgrunden bak till start
                vector.Y -= nrBackgroundsY * texture.Height;
            }
        }
    }

    // Klass som sköter alla bakgrunder
    class Background
    {
        BackgroundSprite[,] background;
        int nrBackgroundsX, nrBackgroundsY;

        // Konstruktor
        public Background(Texture2D texture, GameWindow window)
        {
            // Hur många bilder ska vi ha på bredden?
            double tmpX = (double)window.ClientBounds.Width / texture.Width;
            nrBackgroundsX = (int)Math.Ceiling(tmpX);

            // Hur många bilder skall vi ha på höjden?
            double tmpY = (double)window.ClientBounds.Height / texture.Height;
            nrBackgroundsY = (int)Math.Ceiling(tmpY) + 1;

            // Skapa storleken på arrayen
            background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];

            // Fyll på vektorer med BackgroundSprite-objekt
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    int posX = i * texture.Width;
                    int posY = j * texture.Height;
                    background[i, j] = new BackgroundSprite(texture, posX, posY);
                }
            }
        }

        // Uppdatera positionen för samtliga BackgroundSprite-objekt
        public void Update(GameWindow window)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Update(window, nrBackgroundsY);
                }
            }
        }

        // Rita ut samtliga BackgroundSprite-objekt
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
