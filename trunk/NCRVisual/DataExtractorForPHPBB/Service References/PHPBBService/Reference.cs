﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.50401.0
// 
namespace DataExtractorForPHPBB.PHPBBService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ForumPost", Namespace="http://schemas.datacontract.org/2004/07/DBWebService")]
    public partial class ForumPost : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int PostTimeField;
        
        private int PosterIdField;
        
        private int TopicIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PostTime {
            get {
                return this.PostTimeField;
            }
            set {
                if ((this.PostTimeField.Equals(value) != true)) {
                    this.PostTimeField = value;
                    this.RaisePropertyChanged("PostTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PosterId {
            get {
                return this.PosterIdField;
            }
            set {
                if ((this.PosterIdField.Equals(value) != true)) {
                    this.PosterIdField = value;
                    this.RaisePropertyChanged("PosterId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TopicId {
            get {
                return this.TopicIdField;
            }
            set {
                if ((this.TopicIdField.Equals(value) != true)) {
                    this.TopicIdField = value;
                    this.RaisePropertyChanged("TopicId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PHPBBService.IPHPBBForumService")]
    public interface IPHPBBForumService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IPHPBBForumService/GetPostsInPHPBBForum", ReplyAction="http://tempuri.org/IPHPBBForumService/GetPostsInPHPBBForumResponse")]
        System.IAsyncResult BeginGetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password, System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> EndGetPostsInPHPBBForum(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPHPBBForumServiceChannel : DataExtractorForPHPBB.PHPBBService.IPHPBBForumService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetPostsInPHPBBForumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetPostsInPHPBBForumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PHPBBForumServiceClient : System.ServiceModel.ClientBase<DataExtractorForPHPBB.PHPBBService.IPHPBBForumService>, DataExtractorForPHPBB.PHPBBService.IPHPBBForumService {
        
        private BeginOperationDelegate onBeginGetPostsInPHPBBForumDelegate;
        
        private EndOperationDelegate onEndGetPostsInPHPBBForumDelegate;
        
        private System.Threading.SendOrPostCallback onGetPostsInPHPBBForumCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public PHPBBForumServiceClient() {
        }
        
        public PHPBBForumServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PHPBBForumServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PHPBBForumServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PHPBBForumServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<GetPostsInPHPBBForumCompletedEventArgs> GetPostsInPHPBBForumCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult DataExtractorForPHPBB.PHPBBService.IPHPBBForumService.BeginGetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetPostsInPHPBBForum(dbServerAddr, dbName, username, password, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> DataExtractorForPHPBB.PHPBBService.IPHPBBForumService.EndGetPostsInPHPBBForum(System.IAsyncResult result) {
            return base.Channel.EndGetPostsInPHPBBForum(result);
        }
        
        private System.IAsyncResult OnBeginGetPostsInPHPBBForum(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string dbServerAddr = ((string)(inValues[0]));
            string dbName = ((string)(inValues[1]));
            string username = ((string)(inValues[2]));
            string password = ((string)(inValues[3]));
            return ((DataExtractorForPHPBB.PHPBBService.IPHPBBForumService)(this)).BeginGetPostsInPHPBBForum(dbServerAddr, dbName, username, password, callback, asyncState);
        }
        
        private object[] OnEndGetPostsInPHPBBForum(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> retVal = ((DataExtractorForPHPBB.PHPBBService.IPHPBBForumService)(this)).EndGetPostsInPHPBBForum(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetPostsInPHPBBForumCompleted(object state) {
            if ((this.GetPostsInPHPBBForumCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetPostsInPHPBBForumCompleted(this, new GetPostsInPHPBBForumCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetPostsInPHPBBForumAsync(string dbServerAddr, string dbName, string username, string password) {
            this.GetPostsInPHPBBForumAsync(dbServerAddr, dbName, username, password, null);
        }
        
        public void GetPostsInPHPBBForumAsync(string dbServerAddr, string dbName, string username, string password, object userState) {
            if ((this.onBeginGetPostsInPHPBBForumDelegate == null)) {
                this.onBeginGetPostsInPHPBBForumDelegate = new BeginOperationDelegate(this.OnBeginGetPostsInPHPBBForum);
            }
            if ((this.onEndGetPostsInPHPBBForumDelegate == null)) {
                this.onEndGetPostsInPHPBBForumDelegate = new EndOperationDelegate(this.OnEndGetPostsInPHPBBForum);
            }
            if ((this.onGetPostsInPHPBBForumCompletedDelegate == null)) {
                this.onGetPostsInPHPBBForumCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetPostsInPHPBBForumCompleted);
            }
            base.InvokeAsync(this.onBeginGetPostsInPHPBBForumDelegate, new object[] {
                        dbServerAddr,
                        dbName,
                        username,
                        password}, this.onEndGetPostsInPHPBBForumDelegate, this.onGetPostsInPHPBBForumCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override DataExtractorForPHPBB.PHPBBService.IPHPBBForumService CreateChannel() {
            return new PHPBBForumServiceClientChannel(this);
        }
        
        private class PHPBBForumServiceClientChannel : ChannelBase<DataExtractorForPHPBB.PHPBBService.IPHPBBForumService>, DataExtractorForPHPBB.PHPBBService.IPHPBBForumService {
            
            public PHPBBForumServiceClientChannel(System.ServiceModel.ClientBase<DataExtractorForPHPBB.PHPBBService.IPHPBBForumService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[4];
                _args[0] = dbServerAddr;
                _args[1] = dbName;
                _args[2] = username;
                _args[3] = password;
                System.IAsyncResult _result = base.BeginInvoke("GetPostsInPHPBBForum", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> EndGetPostsInPHPBBForum(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost> _result = ((System.Collections.ObjectModel.ObservableCollection<DataExtractorForPHPBB.PHPBBService.ForumPost>)(base.EndInvoke("GetPostsInPHPBBForum", _args, result)));
                return _result;
            }
        }
    }
}
