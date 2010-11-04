using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DBWebService
{
    [ServiceContract]
    public interface IPHPBBForumService
    {
        [OperationContract]
        List<ForumPost> GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password);
    }

    [DataContract]
    public class ForumPost
    {

        #region private variables
        private int posterId;
        private string posterEmailAddr;
        private string posterName;
        private int postId;
        private int topicId;
        private string postSubject;
        private int postTime;
        #endregion

        #region Data members exposed to client

        [DataMember]
        public string PostSubject
        {
            get { return postSubject; }
            set { postSubject = value; }
        }

        [DataMember]
        public string PosterEmailAddr
        {
            get { return posterEmailAddr; }
            set { posterEmailAddr = value; }
        }

        [DataMember]
        public string PosterName
        {
            get { return posterName; }
            set { posterName = value; }
        }

        [DataMember]
        public int PosterId
        {
            get { return posterId; }
            set { posterId = value; }
        }

        [DataMember]
        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        [DataMember]
        public int TopicId
        {
            get { return topicId; }
            set { topicId = value; }
        }

        [DataMember]
        public int PostTime
        {
            get { return postTime; }
            set { postTime = value; }
        }

        #endregion
    }
}
