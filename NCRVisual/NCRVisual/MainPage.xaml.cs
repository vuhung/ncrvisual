using System.Windows.Controls;
using NCRVisual.Helper;
using System;
using System.Reflection;
using NCRVisual.API;

namespace NCRVisual
{
    public partial class MainPage : UserControl
    {
        LoadSilverlightModuleHelper loadDataModuleHelper;
        LoadSilverlightModuleHelper loadGraphModuleHelper;

        string dataModuleToLoad = "DataInputAnalysis";
        string graphModuleToLoad = "RelationDiagram";

        ChildWindow Upload;

        public MainPage()
        {
            InitializeComponent();
            Upload = new ChildWindow();

            // Init the load module helper and assign a listener for it
            loadDataModuleHelper = new LoadSilverlightModuleHelper();
            loadGraphModuleHelper = new LoadSilverlightModuleHelper();

            loadDataModuleHelper.SilverlightModuleLoaded += new System.EventHandler(loadModuleHelper_SilverlightModuleLoaded);
            loadDataModuleHelper.loadXapFile(dataModuleToLoad);

            loadGraphModuleHelper.SilverlightModuleLoaded += new EventHandler(loadGraphModuleHelper_SilverlightModuleLoaded);
            loadGraphModuleHelper.loadXapFile(graphModuleToLoad);

            AboutUsButton.MouseClick += new EventHandler(AboutUsButton_MouseClick);
        }

        void loadGraphModuleHelper_SilverlightModuleLoaded(object sender, EventArgs e)
        {
            VisualizeButton.MouseClick += new EventHandler(VisualizeButton_MouseClick);

        }

        void AboutUsButton_MouseClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void VisualizeButton_MouseClick(object sender, EventArgs e)
        {
            Assembly asm = loadGraphModuleHelper.LoadedModules[graphModuleToLoad];
            UserControl uc = asm.CreateInstance(LoadSilverlightModuleHelper.MAIN_NAMESPACE + graphModuleToLoad + LoadSilverlightModuleHelper.MODULE_EXT) as UserControl;
            this.Content = uc;
        }

        void UploadButton_MouseClick(object sender, EventArgs e)
        {

            // This function run when a module we requested to load is downloaded to client and was initialized
            Assembly asm = loadDataModuleHelper.LoadedModules[dataModuleToLoad];

            // Init the user control

            UserControl uc = asm.CreateInstance("DataInputAnalysis.MainPage") as UserControl;
            BaseDataAnalysisControl ctrl = (BaseDataAnalysisControl)uc;
            ctrl.UploadComplete += new EventHandler(ctrl_UploadComplete);

            // add it to the main page
            Upload.Content = ctrl;
            Upload.Show();
        }

        void ctrl_UploadComplete(object sender, EventArgs e)
        {
            Upload.Close();
        }

        #region listeners
        public void loadModuleHelper_SilverlightModuleLoaded(object source, EventArgs e)
        {
            UploadButton.MouseClick += new EventHandler(UploadButton_MouseClick);
        }
        #endregion
    }
}
