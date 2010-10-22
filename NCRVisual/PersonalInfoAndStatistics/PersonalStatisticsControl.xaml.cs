
﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Visifire.Charts;

namespace PersonalInfoAndStatistics
{
    public partial class PersonalStatisticsControl : UserControl
    {
        #region constants
        private const int INVALID_NUM = -1; // Used as default values to indicate a variable hasn't been set
        private const string MESS_SENT_CAPTION = "Number of messages sent";
        private const string MESS_RECV_CAPTION = "Number of messages received";
        #endregion

        #region variable
        // variable for general information
        /* No need anymore, will decide later to keep or delete
            private string emailAddr = null;
            private string name = null;
            private int messagesSent = INVALID_NUM;
            private int messagesReceived = INVALID_NUM;
        */
        // variables used to create the graph
        private List<DateTime> allMessagesSentTime = null;
        private List<DateTime> allMessagesReceivedTime = null;
        // the graphs & their variables
        private static string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private Chart messagesSentChart = null;
        private DataPoint[] messagesSentDataPoints = null;
        private Chart messagesRecvChart = null;
        private DataPoint[] messagesRecvDataPoints = null;
        #endregion

        #region getters & setters, but with more functionality (refresh data on update)
        public string EmailAddr
        {
            get { return labelEmailAddrVal.Content.ToString(); }
            set {
                labelEmailAddrVal.Content = value;
            }
        }
        public string Name
        {
            get { return labelNameVal.Content.ToString(); }
            set {
                labelNameVal.Content = value;
            }
        }
        public int MessagesSent
        {
            get
            {
                try
                {
                    return Convert.ToInt32(labelTotalMessSentVal.Content.ToString());
                }
                catch (Exception) // Can only be number format exception
                {
                    return 0;
                }
            }
            set {
                labelTotalMessSentVal.Content = value;
            }
        }
        public int MessagesReceived
        {
            get {
                try
                {
                    return Convert.ToInt32(labelTotalMessRecVal.Content.ToString());
                }
                catch (Exception) // Can only be number format exception
                {
                    return 0;
                }
            }
            set {
                labelTotalMessRecVal.Content = value;
            }
        }
        public List<DateTime> AllMessagesSentTime
        {
            get { return allMessagesSentTime; }
            set {
                allMessagesSentTime = value; // do the normal task of a setter
                // then refresh the graph
                if (messagesSentChart == null) // make sure it's not null
                {
                    messagesSentChart = new Chart();
                    // set the caption of the YAxis
                    Axis YAxis = new Axis();
                    YAxis.Title = MESS_SENT_CAPTION;
                    messagesSentChart.AxesY.Add(YAxis);
                    // init the data
                    DataSeries dataSeries = new DataSeries();
                    messagesSentChart.Series.Add(dataSeries);
                    messagesSentDataPoints = new DataPoint[12];
                    DataPoint tmpDP;
                    for (int i = 0; i < 12; i++)
                    {
                        messagesSentDataPoints[i] = new DataPoint();
                        tmpDP = messagesSentDataPoints[i];
                        tmpDP.YValue = 0;
                        tmpDP.AxisXLabel = months[i];
                        tmpDP.XValue = i + 1;
                        dataSeries.DataPoints.Add(tmpDP);
                    }
                    // Add the chart to the control
                    chart1.Children.Add(messagesSentChart);
                }
                // refresh values of the chart
                processMessageTime(allMessagesSentTime, messagesSentDataPoints, 2010);
            }
        }
        public List<DateTime> AllMessagesReceivedTime
        {
            get { return allMessagesReceivedTime; }
            set {
                allMessagesReceivedTime = value; // do the normal task of a setter

                // then refresh the graph
                if (messagesRecvChart == null) // make sure it's not null
                {
                    messagesRecvChart = new Chart();
                    // set the caption of the YAxis
                    Axis YAxis = new Axis();
                    YAxis.Title = MESS_RECV_CAPTION;
                    messagesRecvChart.AxesY.Add(YAxis);
                    // init the data
                    DataSeries dataSeries = new DataSeries();
                    messagesRecvChart.Series.Add(dataSeries);
                    messagesRecvDataPoints = new DataPoint[12];
                    messagesRecvDataPoints = new DataPoint[12];
                    DataPoint tmpDP;
                    for (int i = 0; i < 12; i++)
                    {
                        messagesRecvDataPoints[i] = new DataPoint();
                        tmpDP = messagesRecvDataPoints[i];
                        tmpDP.YValue = 1;
                        tmpDP.AxisXLabel = months[i];
                        tmpDP.XValue = i + 1;
                        dataSeries.DataPoints.Add(tmpDP);
                    }
                    // Add the chart to the control
                    chart2.Children.Add(messagesRecvChart);
                }
                // refresh values of the chart
                processMessageTime(allMessagesReceivedTime, messagesRecvDataPoints, 2010);
            }
        }
        #endregion

        #region private functions
        private void processMessageTime(List<DateTime> dateTimeList, DataPoint[] dataPoints, int year)
        {
            foreach (DateTime tmpDateTime in dateTimeList)
            {
                if (tmpDateTime.Year == year)
                {
                    dataPoints[tmpDateTime.Month-1].YValue++;
                }
            }
        }
        #endregion

        public PersonalStatisticsControl()
        {
            InitializeComponent();
            //#region debug code to create dummy data
            //List<DateTime> tmp1 = new List<DateTime>();
            //List<DateTime> tmp2 = new List<DateTime>();

            //Random random = new Random();
            //for (int i = 0; i < 50; i++)
            //{
            //    int randomMonth = random.Next(12);
            //    int randomDay = random.Next(28);
            //    tmp1.Add(new DateTime(2010, randomMonth + 1, randomDay + 1));
            //}
            //for (int i = 0; i < 50; i++)
            //{
            //    int randomMonth = random.Next(12);
            //    int randomDay = random.Next(28);
            //    tmp2.Add(new DateTime(2010, randomMonth + 1, randomDay + 1));
            //}
            //AllMessagesReceivedTime = tmp1;
            //AllMessagesSentTime = tmp2;
            //#endregion
        }
    }
}