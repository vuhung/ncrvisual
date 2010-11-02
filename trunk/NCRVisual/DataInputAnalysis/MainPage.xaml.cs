using System.Windows.Controls;
using System;
using NCRVisual.API;

namespace DataInputAnalysis
{
    public partial class MainPage : BaseDataAnalysisControl
    {
        //public event EventHandler UploadComplete;

        public MainPage()
        {
            InitializeComponent();

            Control.FileUploader Uploader = new Control.FileUploader("http://localhost:50491/Upload.ashx");
            this.rootPanel.Children.Add(Uploader);

            Uploader.UploadCompleted += new System.EventHandler(Uploader_UploadCompleted);
        }

        void Uploader_UploadCompleted(object sender, System.EventArgs e)
        {
            this.OnComplete(e);
        }
    }
}
