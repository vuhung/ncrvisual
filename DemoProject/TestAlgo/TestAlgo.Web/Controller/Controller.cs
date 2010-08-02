using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace NCRV
{
    public class Controller
    {       
        public Collection<Email> MailList { get; set; }

        public Collection<User> UserList { get; set; }

        public Collection<Point> V { get; set; }

        public Collection<Point> V1 { get; set; }

        public int [][] Relation=new int [1000][];

        StreamReader reader;

        public void SolveData(string filePath, string fileName)
        {
            MailList = new Collection<Email>();
            UserList = new Collection<User>();
            for (int i = 0; i < 1000; i++)
            {
                Relation[i] = new int[1000];
            }
            //Read and solve data
            GetDataFromFile(filePath+fileName);

            //Save data (matrix) into file text
            WriteDataToFile(filePath+"output.txt");
        }

        private void WriteDataToFile(String filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            for (int i = 1; i <= UserList.Count; i++)
            {
                for (int j = 1; j <= UserList.Count; j++)
                {
                    writer.Write(string.Format("{0,-1:0}",Relation[i][j])+" ");
                }
                writer.WriteLine();
            }
            writer.Close();
        }
        private string[] GetWords(string line)
        {
            line = Regex.Replace(line, "  ", " ");
            return Regex.Split(line, " ");
        }
        private string GetEmail(string line) 
        {
            string[] s = GetWords(line);
            return s[1] + "@" + s[3];           
        }

        private string GetName(string[] s)
        {
            string name = "";
            if (s.Length > 4)
            {
                for (int i = 3; i < s.Length; i++)
                {
                    name = name + s[i]+" ";
                }
            }
            return name;
        }

        private bool IsMessage(string s)
        {
            return (s!=null)&&(s[0]=='<')&& (s[s.Length-1]=='>');
        }

        private bool SolveReply(string [] s, int userid)
        {
            for (int i = 1; i < s.Length; i++)
            {
                var emails = from oneemail in MailList where oneemail.MessageId.Equals(s[i]) select oneemail;
                try
                {
                    if (emails.Count() > 0)
                    {
                        Email email = emails.First();
                        if (email != null)
                        {
                            Relation[userid][email.UserId]++;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }

            //the case : there are more than one parent email;
            bool IsEnd = false;
            while (IsEnd)
            {
                string line = reader.ReadLine();
                string[] words = GetWords(line);
                if (words.Length == 1)
                {
                    if (IsMessage(words[0]))
                    {
                        var emails = from oneemail in MailList where oneemail.MessageId.Equals(words[0]) select oneemail;
                        if (emails.Count<Email>() > 0)
                        {
                            Email email = emails.First();
                            if (email != null)
                            {
                                Relation[userid][email.UserId]++;
                            }
                        }
                    }
                    else
                    {
                        IsEnd = true;
                    }
                }
                else
                {
                    IsEnd = true;
                }

                //check if this line contain messageid
                if (words.Length > 0)
                {
                    if (words[0].Equals("Message-ID"))
                    {
                        string id=SolveId(words);
                        Email email = new Email();
                        email.MessageId = id;
                        email.UserId = userid;
                        return false;
                    }
                }
            }

            return true;
        }

        private string SolveId(string[] s)
        {
            if (s.Length == 2)
            {
                return s[1];
            }
            else
                return "";
        }

        private int GetUserId(string mail)
        {
            var users = from oneuser in UserList where oneuser.Email.Equals(mail) select oneuser;
            if (users.Count()==0)
            {
                return -1;
            }

            User user = users.First<User>();
            if (user != null)
            {
                return user.UserId;
            }
            else return -1;
        }

        private int[][] GetDataFromFile(string filePath)
        {
            try
            {
                reader = new StreamReader(filePath);
            }
            catch (Exception e)
            {
                int i = 0;
            }
            //bool IsHeader=false;
            while (!reader.EndOfStream)
            {                
                string firstLine = reader.ReadLine();
                if (IsNewMail(firstLine))
                {
                    User user = new User();
                    Email email = new Email();

                    //get value of email adress
                    string mail = GetEmail(firstLine);

                    user.Email = mail;
                    user.UserId = GetUserId(mail);

                    bool inHeader = true;

                    //Get data from header of file
                    while (inHeader)
                    {                        
                        firstLine = reader.ReadLine();                        
                        string[] s=GetWords(firstLine);
                        if (s.Length > 0)
                        {
                            switch (s[0]) 
                            {
                                case "From:":
                                    //Add one into the number of email that the user has sent
                                    user.Name=GetName(s);
                                    if (user.UserId == -1)
                                    {
                                        user.UserId = UserList.Count + 1;
                                        UserList.Add(user);
                                    }
                                    Relation[user.UserId][user.UserId]++;
                                    break;
                                case "Message-ID:":
                                    //The line containt Message-ID is the end of header
                                    email.MessageId=SolveId(s);
                                    email.UserId = user.UserId;
                                    MailList.Add(email);
                                    inHeader = false;
                                    break;
                                case "In-Reply-To:":
                                    //Check if this is reply email and revaluate the matrix
                                    inHeader = SolveReply(s,user.UserId);                                   
                                    break;                                    
                            }
                        }
                        
                    }
                }
                
            }

            return Relation;
        }

        private bool IsDatetime(string line)
        {
            DateTime dt;
            if (DateTime.TryParseExact(line,"ddd MMM d HH:mm:ss yyyy ",new CultureInfo("en-US"),DateTimeStyles.AllowLeadingWhite,out dt))            
                return true;
            else
                return false;

        }
        private bool IsNewMail(string line)
        {
            //check the format of string if it is the beginning of email
            line=Regex.Replace(line,"  "," ");
            String[] s = Regex.Split(line," ");
            if (s.Length == 9)
            {
                if (!s[0].Equals("From"))
                {
                    return false;
                }
                if (!s[2].Equals("at"))
                {
                    return false;
                }
                string dt = "";
                for (int i = 4; i < 9; i++)
                {
                    dt = dt + s[i]+' ';
                }
                if (!IsDatetime(dt))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void SetPositionForData()
        {
            int[] dy = new int[UserList.Count + 1];
            for (int i = 1; i <= UserList.Count; i++)
                for (int j = 1; j <= UserList.Count; j++)
                {
                    if (Relation[i][j] > 0)
                        dy[i]++;
                }            

            //set position
            int[] dx = new int[UserList.Count + 1];
            for (int i=1; i <= UserList.Count; i++)
            {
                Point temp = new Point(dy[i], dx[i]);
                V.Add(temp);
                dx[i]++;
            }

        }

        //public void FruchtermentAlgorithm()
        //{
        //    const int MAX_ITERATION = 5;
        //    const int T = 5;
        //    const int W = 300;
        //    const int L = 400;
        //    for (int number = 0; number < MAX_ITERATION; number++)
        //    {
        //        //calculate repulsive forces
        //        for (int i = 1; i <= UserList.Count; i++)
        //        {
        //            Point temp = new Point(0, 0);
        //            V1.Add(temp);
        //            for (int j = 1; j <= UserList.Count; j++)                    
        //                if (j != i)
        //                {
        //                    Point delta = V[i] - V[j];
        //                }
                    
        //        }
        //    }
        //}
    }
}
