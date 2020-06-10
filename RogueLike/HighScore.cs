using System;

namespace RogueLike
{ 
    /// <summary>
    /// Creates high score table
    /// </summary>
    sealed internal class HighScore : IComparable<HighScore>
    {
        internal string Name { get; }
        internal int Score { get; }

        /// <summary>
        /// HighScore constructor
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="score">Player score</param>
        internal HighScore(string name, int score)
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