using System.Windows.Controls;
using System;
using System.Net;
using System.IO;
using System.Windows.Browser;

namespace DataInputAnalysis.Control
{
    public partial class FileUploader : UserControl
    {
        /// <summary>
        /// URL of the uploaded file
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// URI to the handler file
        /// </summary>
        public string URI { get; set; }

        /// <summary>
        /// The current File Info
        /// </summary>
        public FileInfo CurrentFileInfo { get; set; }

        /// <summary>
        /// check if user selected a file to upload or not
        /// </summary>
        public bool HasSelectedAFile { get; set; }

        public event EventHandler UploadCompleted;

        public string OutputFileName { get; set; }

        /// <summary>
        /// Initialize new file uploader
        /// </summary>
        /// <param name="uri">URI to the handler file</param>
        public FileUploader(string uri)
        {
            InitializeComponent();
            this.URI = uri;
            HasSelectedAFile = false;
            BrowseButton.Click += new System.Windows.RoutedEventHandler(BrowseButton_Click);
            UploadButton.Click += new System.Windows.RoutedEventHandler(UploadButton_Click);
        }

        public FileUploader(string uri, string outputFileName)
        {
            InitializeComponent();
            this.URI = uri;
            HasSelectedAFile = false;
            BrowseButton.Click += new System.Windows.RoutedEventHandler(BrowseButton_Click);
            UploadButton.Click += new System.Windows.RoutedEventHandler(UploadButton_Click);
            OutputFileName = outputFileName;
        }

        void UploadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Upload(URI,"");
        }

        void BrowseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;            

            bool? retval = dlg.ShowDialog();

            if (retval != null && retval == true)
            {                
                Address.Text = dlg.File.Name;
                CurrentFileInfo = dlg.File;
                HasSelectedAFile = true;
            }            
        }

        /// <summary>
        /// Up load the specific file
        /// </summary>
        /// <param name="file"></param>
        public void Upload(string uri, string fileName)
        {
            UploadFile(CurrentFileInfo.Name, CurrentFileInfo.OpenRead(), uri);
        }

        private void UploadFile(string fileName, Stream data, string uri)
        {
            UriBuilder ub = new UriBuilder(uri);
            if (OutputFileName == null || OutputFileName == "")
                OutputFileName = "output";
            ub.Query = string.Format("filename={0}&output={1}", OutputFileName + ".txt", OutputFileName);            
            
            WebClient c = new WebClient();

            c.OpenWriteCompleted += (sender, e) =>
                {
                    PushData(data, e.Result);
                    e.Result.Close();
                    data.Close();
                    if (UploadCompleted != null)
                    {
                        UploadCompleted(null, null);
                    }
                };            

            c.OpenWriteAsync(ub.Uri);
            //URL = HtmlPage.Document.DocumentUri.ToString() + "/~/Resources/" + fileName;
            //URL = "http://localhost:8081/Resources/" + fileName;
        }

        private void PushData(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
    }
}
