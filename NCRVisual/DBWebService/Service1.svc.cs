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
        private const string queryCommandGetPosts = "SELECT poster_id, username, user_email, topic_id, post_subject, post_time, post_id FROM ";
        private const string queryCommandGetPostsJoinCmd = " LEFT JOIN ";
        private const string postTableNamePart = "posts";
        private const string usersTableNamePart = "users";
        private const string notUsersTableNamePart = "acl";

        //private const string TOPIC_ID_COL_HEADER = "topic_id";
        private const string POSTER_ID_COL_HEADER = "poster_id";
        //private const string POST_TIME_COL_HEADER = "post_time";

        private const string USER_ID_COL_HEADER = "user_id";
        //private const string USER_NAME_COL_HEADER = "username";
        //private const string USER_EMAIL_COL_HEADER = "user_email";

        private const int POSTER_ID_POS = 0;
        private const int USER_NAME_POS = 1;
        private const int USER_EMAIL_POS = 2;
        private const int TOPIC_ID_POS = 3;
        private const int POST_SUBJECT_POS = 4;
        private const int POST_TIME_POS = 5;
        private const int POST_ID_POS = 6;

        #region IPHPBBForumService Members
        
        public List<ForumPost> GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password)
        {
            // Construct the connection string using input arguments
            string connectionString = "SERVER=" + dbServerAddr + ";" +
                "DATABASE=" + dbName + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";

            // Find out what table have the posts and what table have user information
            string usersTableName = null;
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
                        // stop the loop if we have already reach the goal
                        if (usersTableName != null && postTableName != null)
                        {
                            break;
                        }
                    }
                    else if (tmpStr.EndsWith(usersTableNamePart) && !tmpStr.Contains(notUsersTableNamePart))
                    {
                        usersTableName = tmpStr;
                        // stop the loop if we have already reach the goal
                        if (usersTableName != null && postTableName != null)
                        {
                            break;
                        }
                    }
                    
                }
                connection.Close();
            }

            // Get all the posts in phpbb database
            List<ForumPost> postList = new List<ForumPost>(100);
            if (postTableName != null && usersTableName != null)
            {
                // Create the connection and command
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = connection.CreateCommand();

                // Set the query
                command.CommandText = queryCommandGetPosts + postTableName + queryCommandGetPostsJoinCmd + usersTableName + " ON " + postTableName + "." + POSTER_ID_COL_HEADER + " = " + usersTableName + "." + USER_ID_COL_HEADER;

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
                    tmpFP.PosterName = reader.GetValue(USER_NAME_POS).ToString();
                    tmpFP.PosterEmailAddr = reader.GetValue(USER_EMAIL_POS).ToString();
                    tmpFP.PostSubject = reader.GetValue(POST_SUBJECT_POS).ToString();
                    tmpFP.PostId = Convert.ToInt32(reader.GetValue(POST_ID_POS).ToString());
                    postList.Add(tmpFP);
                }
                connection.Close();
            }
            return postList;
        }

        #endregion
    }
}
