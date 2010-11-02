using System.Windows.Controls;
using System;

namespace NCRVisual.API
{
    public partial class BaseDataAnalysisControl : UserControl
    {
        public event EventHandler UploadComplete;

        protected void OnComplete(EventArgs e)
        {
            EventHandler handler = UploadComplete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public BaseDataAnalysisControl()
        {
            InitializeComponent();
        }
    }
}
