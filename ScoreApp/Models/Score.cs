using System.Collections;
using System.Collections.Generic;

namespace ScoreApp.Models
{
    //-- Model object to hold players score
    public class Score : IEnumerable
    {
        public int id { get; set; }
        public string playerName { get; set; }
        public int score { get; set; }

        public Score()
        {
        }

        public Score(int scoreId, string pName, int playerScore)
        {
            id = scoreId;
            playerName = pName;
            score = playerScore;
        }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }


    //-- Model object to hold a list of all scores
    public class Scores
    {
        public List<Score> scores { get; set; }

        public Scores()
        {
            scores = new List<Score>();
        }
    }
}
