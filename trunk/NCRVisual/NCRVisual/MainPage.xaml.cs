using System.Windows.Controls;
using NCRVisual.Helper;
using System;
using System.Reflection;

namespace NCRVisual
{
    public partial class MainPage : UserControl
    {
        LoadSilverlightModuleHelper loadModuleHelper;

        string moduleToLoad = "RelationDiagram";

        public MainPage()
        {
            InitializeComponent();
            // Init the load module helper and assign a listener for it
            loadModuleHelper = new LoadSilverlightModuleHelper();
            loadModuleHelper.SilverlightModuleLoaded += new System.EventHandler(loadModuleHelper_SilverlightModuleLoaded);
            loadModuleHelper.loadXapFile(moduleToLoad);
        }

        #region listeners
        public void loadModuleHelper_SilverlightModuleLoaded(object source, EventArgs e)
        {
            // This function run when a module we requested to load is downloaded to client and was initialized
            Assembly asm = loadModuleHelper.LoadedModules[moduleToLoad];

            // Init the user control
            //Type ex = asm.CreateInstance(moduleToLoad + LoadSilverlightModuleHelper.MODULE_EXT).GetType();
            UserControl uc = asm.CreateInstance(LoadSilverlightModuleHelper.MAIN_NAMESPACE + moduleToLoad + LoadSilverlightModuleHelper.MODULE_EXT) as UserControl;

            // add it to the main page
            this.Content = uc;
        }
        #endregion
    }
}
