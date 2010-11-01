using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace TopTen
{
    public partial class TopTenControl : UserControl
    {
        #region consts & pseudo consts
        private const string FILTER_BY_MES_SENT_RECV = "Total messages sent & received";
        private const string FILTER_BY_MES_SENT = "Total messages sent";
        private const string FILTER_BY_MES_RECV = "Total messages received";
        private static string[] comboBoxSelections = new string[] { FILTER_BY_MES_SENT_RECV, FILTER_BY_MES_SENT, FILTER_BY_MES_RECV };
        #endregion

        #region private vars
        private ObservableCollection<UserInfo> userList = null;
        private bool triggerFlag = true;
        #endregion

        #region getters & setters, with additonal functionality
        public ObservableCollection<UserInfo> UserList
        {
            get { return this.userList; }
            set
            {
                this.userList = value;
                if (comboBox1.SelectedIndex > -1 && userList != null)
                {
                    if (comboBox1.SelectedItem.Equals(FILTER_BY_MES_SENT_RECV))
                    {
                        sortTopTenBySentAndRecv();
                    }
                    else if (comboBox1.SelectedItem.Equals(FILTER_BY_MES_SENT))
                    {
                        sortTopTenBySent();
                    }
                    else if (comboBox1.SelectedItem.Equals(FILTER_BY_MES_RECV))
                    {
                        sortTopTenByRecv();
                    }
                }
                dataGrid1.ItemsSource = userList;
            }
        }
        #endregion

        #region private functions
        private void sortTopTenBySentAndRecv()
        {
            // sort the user list based on total num of messages sent & received (shell sort)
            int h = 1; // separation between items being compared
            while (h < userList.Count)
            {
                h = 3 * h + 1;
            }

            while (h > 0)
            {
                h = (h - 1) / 3;
                for (int i = h; i < userList.Count; ++i)
                {
                    UserInfo item = userList[i];
                    int j = 0;
                    for (j = i - h; j >= 0 && item.NumMessagesSentAndReceived < userList[j].NumMessagesSentAndReceived; j -= h)
                    {
                        userList[j + h] = userList[j];
                    }// end inner for
                    userList[j + h] = item;
                }// end outer for
            }// end while
        }
        private void sortTopTenBySent()
        {
            // sort the user list based on total num of messages sent (shell sort)
            int h = 1; // separation between items being compared
            while (h < userList.Count)
            {
                h = 3 * h + 1;
            }

            while (h > 0)
            {
                h = (h - 1) / 3;
                for (int i = h; i < userList.Count; ++i)
                {
                    UserInfo item = userList[i];
                    int j = 0;
                    for (j = i - h; j >= 0 && item.NumMessagesSent < userList[j].NumMessagesSent; j -= h)
                    {
                        userList[j + h] = userList[j];
                    }// end inner for
                    userList[j + h] = item;
                }// end outer for
            }// end while
        }
        private void sortTopTenByRecv()
        {
            // sort the user list based on total num of messages received (shell sort)
            int h = 1; // separation between items being compared
            while (h < userList.Count)
            {
                h = 3 * h + 1;
            }

            while (h > 0)
            {
                h = (h - 1) / 3;
                for (int i = h; i < userList.Count; ++i)
                {
                    UserInfo item = userList[i];
                    int j = 0;
                    for (j = i - h; j >= 0 && item.NumMessagesRecv < userList[j].NumMessagesRecv; j -= h)
                    {
                        userList[j + h] = userList[j];
                    }// end inner for
                    userList[j + h] = item;
                }// end outer for
            }// end while
        }
        #endregion

        public TopTenControl()
        {
            InitializeComponent();
            foreach (string tmpStr in comboBoxSelections) {
                this.comboBox1.Items.Add(tmpStr);
            }
            this.triggerFlag = false;
            this.comboBox1.SelectedIndex = 0;
            this.triggerFlag = true;
            
            #region debug code
            ObservableCollection<UserInfo> tmp = new ObservableCollection<UserInfo>();
            for (int i = 0; i < 15; i++)
            {
                UserInfo tmpUI = new UserInfo(i + "@gmail.com", i % 5, i);
                tmp.Add(tmpUI);
            }
            this.UserList = tmp;
            #endregion
        }

        #region event handlers
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (triggerFlag)
            {
                this.UserList = userList; // refresh the datagrid
            }
        }
        #endregion
    }
}
