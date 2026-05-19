using System.IO;

namespace SpaceShooter
{
    public class HighScore
    {
        string filePath = "highscore.txt";
        int highScore = 0;

        /// <summary>
        /// Konstruktor – laddar highscore från fil
        /// </summary>
        public HighScore()
        {
            Load();
        }

        /// <summary>
        /// Returnerar nuvarande highscore
        /// </summary>
        public int Score
        {
            get { return highScore; }
        }

        /// <summary>
        /// Kollar om spelaren slog rekord och sparar om det behövs
        /// </summary>
        public void CheckForNewHighScore(int playerScore)
        {
            if (playerScore > highScore)
            {
                highScore = playerScore;
                Save();
            }
        }

        /// <summary>
        /// Sparar highscore till fil
        /// </summary>
        void Save()
        {
            File.WriteAllText(filePath, highScore.ToString());
        }

        /// <summary>
        /// Laddar highscore från fil
        /// </summary>
        void Load()
        {
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                int.TryParse(text, out highScore);
            }
            else
            {
                highScore = 0;
                Save();
            }
        }
    }
}
