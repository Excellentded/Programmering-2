#region Using Statements
using Microsoft.Xna.Framework;              // tillagd
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SpaceShooter
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game1, klassens konstruktor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
        }


        protected override void Initialize()
        {
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
        }
        protected override void UnloadContent() // rensar spelet
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (GameElements.currentState)
            {
                case GameElements.State.Run:        // Kör själva spelet
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.HighScore:  // Highscorelistan
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;

                case GameElements.State.Quit:       // Avsluta spelet
                    this.Exit();
                    break;

                default:                            // Menyn
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Rensa skärmen
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Använd spriteBatch för att rita ut saker på skärmen
            spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements.State.Run:        // Kör själva spelet
                    GameElements.RunDraw(spriteBatch);
                    break;

                case GameElements.State.HighScore:  // Highscorelistan
                    GameElements.HighScoreDraw(spriteBatch);
                    break;

                case GameElements.State.Quit:       // Avsluta spelet
                    this.Exit();
                    break;

                default:                            // Menyn
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
