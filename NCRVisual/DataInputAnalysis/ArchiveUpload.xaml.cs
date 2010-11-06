using System.Windows.Controls;
using System;
using NCRVisual.API;

namespace DataInputAnalysis
{
    public partial class ArchiveUpload : BaseDataAnalysisControl
    {
        //public event EventHandler UploadComplete;

        public ArchiveUpload()
        {
            InitializeComponent();
            string outputFilename = Guid.NewGuid().ToString();
            this.OutputFileName = outputFilename;
            Control.FileUploader Uploader = new Control.FileUploader("http://localhost:50491/Upload.ashx",OutputFileName);
            this.rootPanel.Children.Add(Uploader);
            Uploader.UploadCompleted += new System.EventHandler(Uploader_UploadCompleted);
        }

        void Uploader_UploadCompleted(object sender, System.EventArgs e)
        {
            this.OnComplete(e);
        }
    }
}
