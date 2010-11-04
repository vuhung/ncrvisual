using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCRVisual.Web.Controllers;
using NCRVisual.Web.PHPBBForumService;

namespace NCRVisual.Web
{
    public partial class PHP_BB_Info : System.Web.UI.Page
    {
        private string ClientBinPath = ""; //path to store input and output data data
        private bool buttonDeactivated = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientBinPath = (this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\") + "\\Output\\").Replace("\\\\","\\");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!buttonDeactivated)
            {
                PHPBBForumServiceClient serviceClient = new PHPBBForumServiceClient("BasicHttpBinding_IPHPBBForumService");
                ForumPost[] posts = serviceClient.GetPostsInPHPBBForum(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text);
                List<ForumPost> forumPostList = new List<ForumPost>(posts);
                GridView1.DataSource = forumPostList;
                GridView1.DataBind();
                PHPBBInputController phpInputController = new PHPBBInputController();
                phpInputController.ForumPosts = forumPostList;

                buttonDeactivated = true;
                bool result = phpInputController.processAndOutputToXML(ClientBinPath);
                if (result)
                {
                    LabelStatus.Text = "Data retrieved successfully";
                }
                else
                {
                    LabelStatus.Text = "There was an error while trying to retrieve data, please check your input";
                }
                buttonDeactivated = false;
            }
        }
    }
}