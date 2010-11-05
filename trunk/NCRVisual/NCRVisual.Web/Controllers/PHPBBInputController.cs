using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using NCRVisual.Web.Items;
using System.Collections.ObjectModel;
using NCRVisual.Web.PHPBBForumService;

namespace NCRVisual.Web.Controllers
{
    public class PHPBBInputController
    {
        public PHPBBInputController()
        {
            leadPosts = new List<PostInfoMin>();
            UserList = new List<User>();
            MailList = new Collection<Email>();
        }

        public Collection<Email> MailList { get; set; }

        public List<User> UserList { get; set; }

        public List<ForumPost> ForumPosts { get; set; }

        private List<PostInfoMin> leadPosts;

        public int[][] Relation { get; set; }

        public bool processAndOutputToXML(string clientBinPath)
        {
            // iterate through the posts to know which post reply to which
            // create the data needed for xml output
            User tmpUser;
            Email tmpEmail;
            foreach (ForumPost tmpPost in ForumPosts)
            {
                tmpUser = new User();
                tmpUser.Email = tmpPost.PosterEmailAddr;
                tmpUser.Name = tmpPost.PosterName;
                tmpUser.UserId = tmpPost.PosterId;
                tmpEmail = new Email();
                tmpEmail.MessageId = tmpPost.PostId.ToString();
                tmpEmail.MessageSubject = tmpPost.PostSubject;
                // BEG: processing the timezone of user sending this email
                string tmpTimeZone = (tmpPost.TimeZone < 0 ? "-" : "+") + String.Format("{0:00}", tmpPost.TimeZone) + String.Format("{0:0.00}", tmpPost.TimeZone).Substring(String.Format("{0:0.00}", tmpPost.TimeZone).Length - 2, 2);
                // END: processing the timezone of user sending this email
                tmpEmail.SendDate = String.Format("{0:ddd, d MMM yyyy HH:mm:ss }", new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(tmpPost.PostTime)) + tmpTimeZone;
                tmpEmail.UserId = tmpUser.UserId;
                PostInfoMin postInfoMin = new PostInfoMin(tmpPost.TopicId, tmpPost.PosterId);
                bool found = false;
                foreach (PostInfoMin tmpPIM in leadPosts)
                {
                    if (tmpPIM.TopicId == postInfoMin.TopicId)
                    {
                        tmpEmail.UserTo = tmpPIM.PosterId;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    leadPosts.Add(postInfoMin);
                    tmpEmail.UserTo = tmpPost.PosterId;
                }

                // decide whether to add the user to the list or not (add if not exists in list yet)
                found = false;
                foreach (User tmpU in UserList)
                {
                    if (tmpU.UserId == tmpUser.UserId)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    UserList.Add(tmpUser);
                }

                MailList.Add(tmpEmail);
            }

            // BEGIN: this is to fix the error in internal xml format
            bool[] fixedUserId = new bool[MailList.Count];
            bool[] fixedUserTo = new bool[MailList.Count];

            //for (int i = 0; i < MailList.Count; i++)
            //{
            //    fixedUserId[i] = false;
            //    fixedUserTo[i] = false;
            //}

            int tmpDummyUserId = 1;
            for (int i = 0; i < UserList.Count; i++)
            {
                for (int j = 0; j < MailList.Count; j++)
                {
                    Email tmpE = MailList[j];
                    if (tmpE.UserId == UserList[i].UserId && !fixedUserId[j])
                    {
                        tmpE.UserId = tmpDummyUserId;
                        fixedUserId[j] = true;
                    }
                    if (tmpE.UserTo == UserList[i].UserId && !fixedUserTo[j])
                    {
                        tmpE.UserTo = tmpDummyUserId;
                        fixedUserTo[j] = true;
                    }
                }
                UserList[i].UserId = tmpDummyUserId;
                tmpDummyUserId++;
            }
            // END: this is to fix the error in internal xml format

            // init the relation matrix
            Relation = new int[UserList.Count + 1][];

            Relation[0] = new int[UserList.Count + 1]; // init the first, empty row

            for (int i = 0; i < UserList.Count; i++)
            {
                Relation[i+1] = new int[UserList.Count + 1];
                int count = 0;
                foreach (Email tmpE in MailList)
                {
                    if (tmpE.UserId == UserList[i].UserId)
                    {
                        User tmpU = new User();
                        tmpU.UserId = tmpE.UserTo;
                        Relation[i+1][UserList.IndexOf(tmpU) + 1]++;
                        count++;
                    }
                }
                Relation[i+1][i+1] = count;
            }



            WriteDataToXml((clientBinPath + "\\phpbb_test.xml").Replace("\\\\","\\"));

            return true;
        }

        /// <summary>
        /// Write data in relation to the xml file
        /// The xml file has format
        /// <Relation>
        ///     <Vertex>
        ///         <UserId></UserId>
        ///         <Email></Email>
        ///         <Name></Name>
        ///         <Edge count="1">
        ///             <start></start>
        ///             <end></end>
        ///             <value></value>
        ///             <content count="1">
        ///                 <date></date>
        ///                 <subject></subject>
        ///             </content>
        ///             ..........
        ///             
        ///         </Edge>
        ///         .............
        ///     </Vertex>
        ///     ..............
        /// </relation>
        /// </summary>
        /// <param name="filePath"></param>
        private void WriteDataToXml(String filePath)
        {
            XmlTextWriter writer = new XmlTextWriter(filePath, null);
            writer.WriteStartElement("Relation");
            for (int i = 0; i < UserList.Count; i++)
            {
                writer.WriteWhitespace("\n\t");
                writer.WriteStartElement("Vertex");//start element of each vertex
                writer.WriteWhitespace("\n\t\t");
                writer.WriteElementString("UserId", UserList[i].UserId.ToString());
                writer.WriteWhitespace("\n\t\t");
                writer.WriteElementString("Email", UserList[i].Email);
                writer.WriteWhitespace("\n\t\t");
                writer.WriteElementString("Name", UserList[i].Name);

                int count = 0;
                for (int j = 1; j <= UserList.Count; j++)//write relation data beetween vertex i and the others
                {
                    if (Relation[i + 1][j] > 0)//because the relation array begin with index 1
                    {
                        count++;
                        writer.WriteWhitespace("\n\t\t");
                        writer.WriteStartElement("Edge");
                        writer.WriteAttributeString("count", count.ToString());
                        writer.WriteWhitespace("\n\t\t\t");
                        writer.WriteElementString("Start", UserList[i].UserId.ToString());
                        writer.WriteWhitespace("\n\t\t\t");
                        writer.WriteElementString("End", UserList[j - 1].UserId.ToString());
                        writer.WriteWhitespace("\n\t\t\t");
                        writer.WriteElementString("Value", Relation[i + 1][j].ToString());

                        //email date and email subject of the email which user has sent
                        List<Email> emailCollection = (from oneemail in MailList where (oneemail.UserId == UserList[i].UserId) select oneemail).ToList<Email>();//get email that has the messageid = s[i]
                        try
                        {
                            //Collection<Email> emailCollection = (Collection<Email>)emails;
                            for (int k = 0; k < emailCollection.Count; k++)
                            {
                                writer.WriteWhitespace("\n\t\t\t");
                                writer.WriteStartElement("Content");
                                writer.WriteAttributeString("count", k.ToString());
                                writer.WriteWhitespace("\n\t\t\t\t");
                                writer.WriteElementString("Date", emailCollection[k].SendDate);
                                writer.WriteWhitespace("\n\t\t\t\t");
                                writer.WriteElementString("Subject", emailCollection[k].MessageSubject);
                                writer.WriteWhitespace("\n\t\t\t");
                                writer.WriteEndElement();
                            }
                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                        writer.WriteWhitespace("\n\t\t");
                        writer.WriteEndElement();
                    }
                }
                writer.WriteWhitespace("\n\t");
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();
        }
    }
}