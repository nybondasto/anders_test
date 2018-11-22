using ScoreApp.Models;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace ScoreApp
{
    public class DL
    {
        //-- Connect to SQLite database
        private SQLiteConnection connect()
        {
            string databasePath = System.AppDomain.CurrentDomain.BaseDirectory + @"App_Data\" + "ScoreAppDB.db";
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;New=False;");
            conn.Open();
            return conn;
        }


        //-- Retrieve scores from database as List of Score-objects
        public Scores GetScores(string order)
        {
            string sql = "select id, playerName, score from scores order by score " + order;
            SQLiteConnection conn = connect();

            using (conn)
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Scores lst = new Scores();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //-- Initialize new score-object with values
                        Score score = new Score
                        {
                            id = int.Parse(reader["id"].ToString()),
                            playerName = reader["playerName"].ToString(),
                            score = int.Parse(reader["score"].ToString())
                        };
                        lst.scores.Add(score);
                    }
                    reader.Close();
                    return lst;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
        }



        public async Task<bool> AddScore(Score s)
        {
            int numInserted;
            string sql = "INSERT INTO scores(playerName, score) VALUES('" + s.playerName + "'," + s.score + ")";
            SQLiteConnection conn = connect();

            using (var transaction = conn.BeginTransaction())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                numInserted = await cmd.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            conn.Close();

            if (numInserted == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}