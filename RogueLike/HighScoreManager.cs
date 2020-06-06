using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace RogueLike
{
    /// <summary>
    /// Creates, saves or prints high score tables
    /// </summary>
    sealed public class HighScoreManager
    {
        List<HighScore> scores;

        /// <summary>
        /// HighScoreManager constructor
        /// </summary>
        public HighScoreManager()
        {
            scores = new List<HighScore>();
            System.IO.Directory.CreateDirectory(@"RogueLike\Scores");
        }

        /// <summary>
        /// Saves scores to a new file or a file that already exists
        /// </summary>
        /// <param name="level">Level Number</param>
        public void SaveScore(int levelScore)
        {
            if (File.Exists(
            $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_HighScores.txt"))
                SaveFile(levelScore);
            else
            {
                CreateFile();
                SaveFile(levelScore);
            }
        }

        /// <summary>
        /// Saves a file with level information
        /// </summary>
        /// <param name="levelScore">Number of level</param>
        public void SaveFile(int levelScore)
        {
            Input input         = new Input();
            Renderer print      = new Renderer();
            int count           = 0;
            const char space    = ' ';
            string s;
            ////////////////////////////////////////////////////////////////////
            // Reader
            StreamReader scoreR = new StreamReader(
            $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_HighScores.txt");
            
            using (scoreR)
            {   // While readline isn't null
                while ((s = scoreR.ReadLine()) != null)
                {
                    // splits the characters
                    string[] split  = s.Split(space);
                    string name     = split[0];
                    int score       = Convert.ToInt32(split[1]);
                    // Adds a new high score to the list
                    scores.Add(new HighScore(name, score));
                } 
            }
            
            ////////////////////////////////////////////////////////////////////
            // Sorts List
            scores.Sort();
            // Adds high score to list if the high score is higher
            // If player's score is higher than last value
            if (levelScore > scores.Last().Score)
            {   // Asks for player name and adds it to the list
                print.InsertHighScore();
                string name = input.InsertName();
                scores.Add(new HighScore(name, levelScore));
            }

            
            // Sorts List
            scores.Sort();
            ////////////////////////////////////////////////////////////////////
            // Writer
            StreamWriter scoreW = new StreamWriter(
            $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_HighScores.txt");
            // Writes players to file
            
            using (scoreW)
            {
                foreach (HighScore score in scores)
                {   // Keeps high score list to a limit of 10
                    if (count < 10)
                    {
                        scoreW.WriteLine(score.Name + space + score.Score);
                        count++;
                    }
                }
            }

            PrintScore();
        }

        /// <summary>
        /// Creates a file with unknown names
        /// </summary>
        public void CreateFile()
        {
            int count = 0;
            StreamWriter scoreW = new StreamWriter(
            $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_HighScores.txt");
                
            using(scoreW) 
            {
                ////////////////////////////////////////////////////////////
                // Fills the file with 10 unknown players
                string[] names = new string[] {"Khalid", "Minsc", "Jaheira", 
                        "Edwin", "Gorion", "Yoshimo", "Drizzt", "Sarevok",
                        "Tazok", "Kivan"};
                
                foreach (string name in names)
                {
                    scores.Add(new HighScore(name, count));
                    count++;
                }
            }
        }

        /// <summary>
        /// Prints high score
        /// </summary>
        public void PrintScore()
        {
            Renderer print = new Renderer();

            string s;
            const char space = ' ';
            if (File.Exists(
            $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_HighScores.txt"))
            {
                StreamReader scoreR = new StreamReader(
                $@"RogueLike\Scores\{Game.rows}_x_{Game.columns}_" +
                "HighScores.txt");

                Console.WriteLine("\n** HIGH SCORE **");
                Console.WriteLine($"  🙌 {Game.rows} x {Game.columns} 🙌 ");
                Console.WriteLine("_________________");
                while ((s = scoreR.ReadLine()) != null)
                {
                    //Console.WriteLine(scoreR.ReadLine());
                    string[] split  = s.Split(space);
                    string name     = split[0];
                    int score       = Convert.ToInt32(split[1]);
                    Console.WriteLine($" {name,-12}{score,+3}");
                }
                Console.WriteLine("_________________");
                scoreR.Close();
            } 
            else print.NoHighScores();
        }
    }
}