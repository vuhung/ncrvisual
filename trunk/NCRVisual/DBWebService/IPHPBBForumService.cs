using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DBWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPHPBBForumService
    {
        [OperationContract]
        List<ForumPost> GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ForumPost
    {
        private int posterId;

        //private int postId;

        private int topicId;

        private int postTime;

        //[DataMember]
        //public int PostId
        //{
        //    get { return postId; }
        //    set { postId = value; }
        //}

        [DataMember]
        public int PosterId
        {
            get { return posterId; }
            set { posterId = value; }
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
    }
}
