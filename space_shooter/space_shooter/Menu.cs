using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SpaceShooter
{
    /// <summary>
    /// används för att skapa en meny, lägga till menyval i menyn
    /// samt att ta emot tangenttryckningar för olika menyval
    /// </summary>
    class Menu
    {
        List<MenuItem> menu; // Lista på menuItems
        int selected = 0; // Första valet i listan är valt
        float currentHeight = 0;
        double lastChange = 0;
        int defaultMenuState;

        /// <summary>
        /// Konstruktor som skapar Listan med MenuItems
        /// </summary>
        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        /// <summary>
        /// Lägg till ett menyval i listan
        /// </summary>
        public void AddItem(Texture2D itemTexture, int state)
        {
            float X = 0;
            float Y = 0 + currentHeight;

            currentHeight += itemTexture.Height + 20;

            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        /// <summary>
        /// Update(), kollar om användaren tryckt någon tangent.
        /// </summary>
        public int Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    if (selected >= menu.Count)
                        selected = 0;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    if (selected < 0)
                        selected = menu.Count - 1;
                }

                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
                return menu[selected].State;

            return defaultMenuState;
        }

        /// <summary>
        /// Draw(), ritar ut menyn
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == selected)
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                else
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
            }
        }
    }

    /// <summary>
    /// MenuItem, container-klass för ett menyval
    /// </summary>
    class MenuItem
    {
        Texture2D texture;
        Vector2 position;
        int currentState;

        /// <summary>
        /// MenuItem, konstruktor som skapar ett nytt objekt
        /// </summary>
        public MenuItem(Texture2D texture, Vector2 position, int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;
        }

        /// <summary>
        /// Get-properties för MenuItem
        /// </summary>
        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get { return position; } }
        public int State { get { return currentState; } }
    }
}
