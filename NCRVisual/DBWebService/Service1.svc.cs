using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using DBWebService;

namespace DBWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class PHPBBForumService : IPHPBBForumService
    {
        private const string queryCommandShowTables = "SHOW Tables"; // Show all table in a database
        private const string queryCommandGetPosts = "SELECT * FROM ";
        private const string postTableNamePart = "posts";

        private const string TOPIC_ID_COL_HEADER = "topic_id";
        private const string POSTER_ID_COL_HEADER = "poster_id";
        private const string POST_TIME_COL_HEADER = "post_time";

        private const string USER_ID_COL_HEADER = "user_id";
        private const string USER_NAME_COL_HEADER = "username";

        private const int TOPIC_ID_POS = 1;
        private const int POSTER_ID_POS = 3;
        private const int POST_TIME_POS = 6;

        #region IPHPBBForumService Members
        
        public List<ForumPost> GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password)
        {
            // Construct the connection string using input arguments
            string connectionString = "SERVER=" + dbServerAddr + ";" +
                "DATABASE=" + dbName + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";

            // Find out what table have the posts
            string postTableName = null;
            {
                // Create the connection and command
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = connection.CreateCommand();

                // Set the query
                command.CommandText = queryCommandShowTables;

                // Init the connection with mysql db server
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string tmpStr = null;
                while (reader.Read())
                {
                    tmpStr = reader.GetValue(0).ToString();
                    if (tmpStr.EndsWith(postTableNamePart))
                    {
                        postTableName = tmpStr;
                        break;
                    }
                }
                connection.Close();
            }

            // Get all the posts in phpbb database
            List<ForumPost> postList = new List<ForumPost>(100);
            if (postTableName != null)
            {
                // Create the connection and command
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = connection.CreateCommand();

                // Set the query
                command.CommandText = queryCommandGetPosts + postTableName;

                // Init the connection with mysql db server
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                ForumPost tmpFP = null;
                while (reader.Read()) // Iterate through all records
                {
                    tmpFP = new ForumPost();
                    tmpFP.PosterId = Convert.ToInt32(reader.GetValue(POSTER_ID_POS).ToString());
                    tmpFP.TopicId = Convert.ToInt32(reader.GetValue(TOPIC_ID_POS).ToString());
                    tmpFP.PostTime = Convert.ToInt32(reader.GetValue(POST_TIME_POS).ToString());
                    postList.Add(tmpFP);
                }
                connection.Close();
            }
            return postList;
        }

        #endregion
    }
}
