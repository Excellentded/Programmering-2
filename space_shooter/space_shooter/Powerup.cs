using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    // Ett guldmynt, som ger poäng.
    class GoldCoin : PhysicalObject
    {
        double timeToDie; // Hur länge guldmyntet lever kvar

        /// <summary>
        /// Konstruktor för att skapa objektet
        /// </summary>
        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime)
            : base(texture, X, Y, 2f,1f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }

        /// <summary>
        /// Uppdatera, kontrollerar om guldmyntet ska få leva vidare
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Döda guldmyntet om det är för gammalt
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                isAlive = false;
        }
    }
}
