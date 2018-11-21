using ScoreApp.Models;
using System.Data.SQLite;

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
        public Scores GetScores()
        {
            string sql = "select id, playerName, score from scores order by score desc";
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
    }
}