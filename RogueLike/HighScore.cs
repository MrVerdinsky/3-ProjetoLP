using System;

namespace RogueLike
{ 
    /// <summary>
    /// Class to create high score table
    /// </summary>
    sealed public class HighScore : IComparable<HighScore>
    {
        public string Name { get; }
        public int Score { get; }
        /// <summary>
        /// HighScore constructor
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="score">Player score</param>
        public HighScore(string name, int score)
        {
            Name    = name;
            Score   = score;
        }
        
        /// <summary>
        /// Compares 2 HighScores
        /// </summary>
        /// <param name="otherScore">High score to compare</param>
        /// <returns>Higher, lower or equal number</returns>
        public int CompareTo(HighScore otherScore)
        {
            if (otherScore == null) return - 1;
            return otherScore.Score - Score;
        }
    }
}