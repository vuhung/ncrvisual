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
using DataExtractorForPHPBB.PHPBBService;

namespace DataExtractorForPHPBB
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void serviceClient_GetPostsInPHPBBForumCompleted(Object sender, GetPostsInPHPBBForumCompletedEventArgs e)
        {
            dataGrid1.ItemsSource = e.Result;
            label1.Content = e.Result.Count;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PHPBBForumServiceClient serviceClient = new PHPBBForumServiceClient("BasicHttpBinding_IPHPBBForumService");
            serviceClient.GetPostsInPHPBBForumAsync(textBox1.Text, textBox2.Text, textBox3.Text, passwordBox1.Password);
            serviceClient.GetPostsInPHPBBForumCompleted += new EventHandler<GetPostsInPHPBBForumCompletedEventArgs>(serviceClient_GetPostsInPHPBBForumCompleted);
        }
    }
}
