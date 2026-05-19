using Microsoft.Xna.Framework;            
using Microsoft.Xna.Framework.Content;     
using Microsoft.Xna.Framework.Graphics;     
using Microsoft.Xna.Framework.Input;        
using System.Collections.Generic;
using System.Linq;
using System;

namespace SpaceShooter
{

    static class GameElements
    {

        static float enemySpawnTimer = 0f; //spawning saker
        static float enemySpawnInterval = 2f; // spawn varje 2 sekunder

        static HighScore highScore; //highscore saker
        static int lastScore;

        static Background background;
        static Texture2D menuSprite;
        static Vector2 menuPos;
        static Player player;
        static List<Enemy> enemies;
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;
        static PrintText printText;
        static Menu menu;

        // Olika gamestates
        public enum State { Menu, Run, HighScore, Quit };
        public static State currentState;

        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();
            highScore = new HighScore();
            lastScore = 0;
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            background = new Background(content.Load<Texture2D>("background"), window);

            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.HighScore);
            menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);

            menuSprite = content.Load<Texture2D>("menu/menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

            player = new Player(content.Load<Texture2D>("ship"), 380, 400, 2.5f, 4.5f,
                content.Load<Texture2D>("bullet"));

            // Skapa fiender
            enemies = new List<Enemy>();
            GenerateEnemies(content, window);


            goldCoinSprite = content.Load<Texture2D>("powerups/coin");

            // Ladda fonten
            printText = new PrintText(content.Load<SpriteFont>("spritefont"));
        }

        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }


        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            background.Update(window);

            // Fiende-spawnande under spelets gång
            enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (enemySpawnTimer >= enemySpawnInterval)
            {
                SpawnEnemy(content, window);
                enemySpawnTimer = 0f;
            }

            // Uppdatera spelarens position
            player.Update(window, gameTime);

            // Gå igenom alla fiender
            foreach (Enemy e in enemies.ToList())
            {
                // Kontrollera om fienden kolliderar med ett skott
                foreach (Player.Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b))
                    {
                        e.IsAlive = false;        // Döda fienden
                        player.Points++;           // Ge spelaren poäng

                        player.Bullets.Remove(b); //tar bort bullet
                        break;
                    }
                }

                if (e.IsAlive)
                {
                    // Kontrollera kollision med spelaren
                    if (e.CheckCollision(player))
                        player.IsAlive = false;    // Döda spelaren

                    e.Update(window);              // Flytta på fienden
                }
                else
                    enemies.Remove(e);             // Ta bort fienden
            }

            // Gulmynten ska uppstå slumpmässigt, en chans på 200:
            Random random = new Random();
            int newCoin = random.Next(1, 200);
            if (newCoin == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);

                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }

            // gå igenom hela listan med existerande guldmynt
            foreach (GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive)
                {
                    gc.Update(gameTime);

                    if (gc.CheckCollision(player))
                    {
                        goldCoins.Remove(gc);
                        player.Points++;
                    }
                }
                else
                {
                    goldCoins.Remove(gc);
                }
            }

            if (!player.IsAlive)
            {
                lastScore = player.Points;
                highScore.CheckForNewHighScore(lastScore); // lägger in points i highscore saker

                Reset(window, content);
                return State.Menu;
            }

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            player.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);

            printText.Print("Points: " + player.Points, spriteBatch, 0, 0);
        }

        public static State HighScoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Menu;

            return State.HighScore;
        }

        private static void GenerateEnemies(ContentManager content, GameWindow window)
        {
            Random random = new Random();


            // mine spawn
            Texture2D tmpSprite = content.Load<Texture2D>("enemies/mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Enemy temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }


            // tripod spawn
            tmpSprite = content.Load<Texture2D>("enemies/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Enemy temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }

            // Drone (all extra kod i detta var för att bilden var för stor och orka fixa
            tmpSprite = content.Load<Texture2D>("enemies/drone");

            float droneScale = 0.05f;

            for (int i = 0; i < 5; i++)
            {
                int scaledWidth = (int)(tmpSprite.Width * droneScale);
                int maxX = Math.Max(1, window.ClientBounds.Width - scaledWidth);
                int rndX = random.Next(0, maxX);

                int scaledHeight = (int)(tmpSprite.Height * droneScale);
                int maxY = Math.Max(1, (window.ClientBounds.Height / 2) - scaledHeight);
                int rndY = random.Next(0, maxY);


                Enemy temp = new Drone (tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
        }

        private static void SpawnEnemy(ContentManager content, GameWindow window)// funktion för att spawna random fiender
        {
            Random random = new Random();

            int enemyType = random.Next(0, 3); // 0 = Mine, 1 = Tripod, 2 = Drone
            Texture2D sprite;
            Enemy e;

            switch (enemyType) 
            {
                case 0: // Mine
                    sprite = content.Load<Texture2D>("enemies/mine");
                    e = new Mine(
                        sprite,
                        random.Next(0, window.ClientBounds.Width - sprite.Width),
                        random.Next(0, window.ClientBounds.Height / 2)
                    );
                    break;

                case 1: // Tripod
                    sprite = content.Load<Texture2D>("enemies/tripod");
                    e = new Tripod(
                        sprite,
                        random.Next(0, window.ClientBounds.Width - sprite.Width),
                        random.Next(0, window.ClientBounds.Height / 2)
                    );
                    break;

                default: // Drone
                    sprite = content.Load<Texture2D>("enemies/drone");
                    float droneScale = 0.05f;

                    int scaledWidth = (int)(sprite.Width * droneScale);
                    int scaledHeight = (int)(sprite.Height * droneScale);

                    e = new Drone(
                        sprite,
                        random.Next(0, window.ClientBounds.Width - scaledWidth),
                        random.Next(0, (window.ClientBounds.Height / 2) - scaledHeight)
                    );
                    break;
            }

            enemies.Add(e);
        }

        private static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset(380, 400, 2.5f, 4.5f);
            enemies.Clear();
            GenerateEnemies(content, window);
            goldCoins.Clear();
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            printText.Print("HIGHSCORE", spriteBatch, 300, 100);
            printText.Print("--------------", spriteBatch, 300, 130);

            printText.Print("Last Score: " + lastScore, spriteBatch, 300, 200);
            printText.Print("Best Score: " + highScore.Score, spriteBatch, 300, 250);

            printText.Print("Press Escape to exit", spriteBatch, 300, 350);
        }

    }
}
