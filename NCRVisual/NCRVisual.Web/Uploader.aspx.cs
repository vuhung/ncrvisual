using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCRVisual.Web.Controllers;

namespace NCRVisual.Web
{
    public partial class Uploader : System.Web.UI.Page
    {
        string ClientBinPath = ""; //path to store input and output data data
        string fileName = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientBinPath = this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\") + "Output\\";
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
                    fileName = fuInput.FileName;
                    fuInput.SaveAs(ClientBinPath +
                         fuInput.FileName);
                    lblStatus.Text = "File name: " +
                         fuInput.PostedFile.FileName + "<br>" +
                         fuInput.PostedFile.ContentLength + " kb<br>" +
                         "Content type: " +
                         fuInput.PostedFile.ContentType;

                    DataInputController controller = new DataInputController();
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