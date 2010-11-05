﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NCRVisual.Web.PHPBBForumService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ForumPost", Namespace="http://schemas.datacontract.org/2004/07/DBWebService")]
    [System.SerializableAttribute()]
    public partial class ForumPost : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PostIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostSubjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PostTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PosterEmailAddrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PosterIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PosterNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TimeZoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TopicIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PostId {
            get {
                return this.PostIdField;
            }
            set {
                if ((this.PostIdField.Equals(value) != true)) {
                    this.PostIdField = value;
                    this.RaisePropertyChanged("PostId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostSubject {
            get {
                return this.PostSubjectField;
            }
            set {
                if ((object.ReferenceEquals(this.PostSubjectField, value) != true)) {
                    this.PostSubjectField = value;
                    this.RaisePropertyChanged("PostSubject");
                }
            }
        }
        
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
        public string PosterEmailAddr {
            get {
                return this.PosterEmailAddrField;
            }
            set {
                if ((object.ReferenceEquals(this.PosterEmailAddrField, value) != true)) {
                    this.PosterEmailAddrField = value;
                    this.RaisePropertyChanged("PosterEmailAddr");
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
        public string PosterName {
            get {
                return this.PosterNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PosterNameField, value) != true)) {
                    this.PosterNameField = value;
                    this.RaisePropertyChanged("PosterName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double TimeZone {
            get {
                return this.TimeZoneField;
            }
            set {
                if ((this.TimeZoneField.Equals(value) != true)) {
                    this.TimeZoneField = value;
                    this.RaisePropertyChanged("TimeZone");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PHPBBForumService.IPHPBBForumService")]
    public interface IPHPBBForumService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPHPBBForumService/GetPostsInPHPBBForum", ReplyAction="http://tempuri.org/IPHPBBForumService/GetPostsInPHPBBForumResponse")]
        NCRVisual.Web.PHPBBForumService.ForumPost[] GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPHPBBForumServiceChannel : NCRVisual.Web.PHPBBForumService.IPHPBBForumService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PHPBBForumServiceClient : System.ServiceModel.ClientBase<NCRVisual.Web.PHPBBForumService.IPHPBBForumService>, NCRVisual.Web.PHPBBForumService.IPHPBBForumService {
        
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
        
        public NCRVisual.Web.PHPBBForumService.ForumPost[] GetPostsInPHPBBForum(string dbServerAddr, string dbName, string username, string password) {
            return base.Channel.GetPostsInPHPBBForum(dbServerAddr, dbName, username, password);
        }
    }
}
