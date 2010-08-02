using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCRV;

namespace TestAlgo.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string ClientBinPath = "";
        string fileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (fuInput.HasFile)
                try
                {
                    if (!fuInput.FileName.Contains(".txt"))
                    {
                        lblStatus.Text = "This is not a text file";
                        return;
                    }
                    ClientBinPath = this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\") + "ClientBin\\";
                    fileName = fuInput.FileName;
                    fuInput.SaveAs(ClientBinPath+
                         fuInput.FileName);
                    lblStatus.Text = "File name: " +
                         fuInput.PostedFile.FileName + "<br>" +
                         fuInput.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         fuInput.PostedFile.ContentType;

                    Controller controller = new Controller();
                    controller.SolveData(ClientBinPath, fileName);
                    lblStatus.Text = "Success, please press the link below to see visualization";
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                lblStatus.Text = "You have not specified a file.";
            }
            
        }
    }
}
