using System.Collections.Generic;
using System.IO;
using System.Linq;
using KufTheGame.Models.Structures;

namespace KufTheGame.Core
{
    public static class Scoreboard
    {
        private const string ScoreboardPath = "../../../ScoreBoard.txt";

        public static int Score { get; private set; }

        public static void AddScore(int value)
        {
            Score += value * GameLevel.Level;
        }

        public static void AddNewToScoreboard(int value)
        {
            var scores = GetScoreboard();
            scores.Add(value);
            scores.Sort();
            if (scores.Count > 10)
            {
                WriteScoreboard(scores);
            }
            else
            {
                var topTen = scores.Take(10);
                WriteScoreboard(topTen);
            }
        }

        public static List<int> GetScoreboard()
        {
            var scores = new List<int>();
            try
            {
                using (var reader = new StreamReader(ScoreboardPath))
                {
                    var line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        scores.Add(int.Parse(line.Trim()));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                using (var writer = new StreamWriter(ScoreboardPath))
                {
                    writer.WriteLine();
                }

                GetScoreboard();
            }

            return (scores.Count > 10) ? scores.GetRange(0, 10) : scores;
        }

        private static void WriteScoreboard(IEnumerable<int> scores)
        {
            using (var writer = new StreamWriter(ScoreboardPath))
            {
                foreach (var score in scores.Reverse())
                {
                    writer.WriteLine(score.ToString());
                }
            }
        }
    }
}
