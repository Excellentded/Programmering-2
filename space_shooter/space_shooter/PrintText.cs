#region Using Statements
using Microsoft.Xna.Framework;              // tillagd
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SpaceShooter
{
    public class PrintText
    {
        SpriteFont font;

        /// <summary>
        /// Konstruktor som tar ett SpriteFont-objekt
        /// </summary>
        public PrintText(SpriteFont font)
        {
            this.font = font;
        }

        /// <summary>
        /// Skriver ut texten på skärmen
        /// </summary>
        public void Print(string text, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
    }
}
