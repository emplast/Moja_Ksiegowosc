﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Ten kod źródłowy został wygenerowany automatycznie przez Microsoft.VSDesigner, wersja 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Moja_Ksiegowosc.wsdl {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Piotr_WSBinding", Namespace="urn:http://www.piotr-majewski.com.pl")]
    public partial class Piotr_WS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback _odOperationCompleted;
        
        private System.Threading.SendOrPostCallback wierszOperationCompleted;
        
        private System.Threading.SendOrPostCallback ryczaltOperationCompleted;
        
        private System.Threading.SendOrPostCallback wiersz_1OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Piotr_WS() {
            this.Url = global::Moja_Ksiegowosc.Properties.Settings.Default.Moja_Ksiegowosc_wsdl_Piotr_WS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event _odCompletedEventHandler _odCompleted;
        
        /// <remarks/>
        public event wierszCompletedEventHandler wierszCompleted;
        
        /// <remarks/>
        public event ryczaltCompletedEventHandler ryczaltCompleted;
        
        /// <remarks/>
        public event wiersz_1CompletedEventHandler wiersz_1Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://www.piotr-majewski.com.pl/wsdl.php/_od", RequestNamespace="http://www.piotr-majewski.com.pl", ResponseNamespace="http://www.piotr-majewski.com.pl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string _od(int c, int d, int year) {
            object[] results = this.Invoke("_od", new object[] {
                        c,
                        d,
                        year});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void _odAsync(int c, int d, int year) {
            this._odAsync(c, d, year, null);
        }
        
        /// <remarks/>
        public void _odAsync(int c, int d, int year, object userState) {
            if ((this._odOperationCompleted == null)) {
                this._odOperationCompleted = new System.Threading.SendOrPostCallback(this.On_odOperationCompleted);
            }
            this.InvokeAsync("_od", new object[] {
                        c,
                        d,
                        year}, this._odOperationCompleted, userState);
        }
        
        private void On_odOperationCompleted(object arg) {
            if ((this._odCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this._odCompleted(this, new _odCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://www.piotr-majewski.com.pl/wsdl.php/wiersz", RequestNamespace="http://www.piotr-majewski.com.pl", ResponseNamespace="http://www.piotr-majewski.com.pl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public int wiersz(int year) {
            object[] results = this.Invoke("wiersz", new object[] {
                        year});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void wierszAsync(int year) {
            this.wierszAsync(year, null);
        }
        
        /// <remarks/>
        public void wierszAsync(int year, object userState) {
            if ((this.wierszOperationCompleted == null)) {
                this.wierszOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwierszOperationCompleted);
            }
            this.InvokeAsync("wiersz", new object[] {
                        year}, this.wierszOperationCompleted, userState);
        }
        
        private void OnwierszOperationCompleted(object arg) {
            if ((this.wierszCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.wierszCompleted(this, new wierszCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://www.piotr-majewski.com.pl/wsdl.php/ryczalt", RequestNamespace="http://www.piotr-majewski.com.pl", ResponseNamespace="http://www.piotr-majewski.com.pl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string ryczalt(int z, int x, int year) {
            object[] results = this.Invoke("ryczalt", new object[] {
                        z,
                        x,
                        year});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ryczaltAsync(int z, int x, int year) {
            this.ryczaltAsync(z, x, year, null);
        }
        
        /// <remarks/>
        public void ryczaltAsync(int z, int x, int year, object userState) {
            if ((this.ryczaltOperationCompleted == null)) {
                this.ryczaltOperationCompleted = new System.Threading.SendOrPostCallback(this.OnryczaltOperationCompleted);
            }
            this.InvokeAsync("ryczalt", new object[] {
                        z,
                        x,
                        year}, this.ryczaltOperationCompleted, userState);
        }
        
        private void OnryczaltOperationCompleted(object arg) {
            if ((this.ryczaltCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ryczaltCompleted(this, new ryczaltCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://www.piotr-majewski.com.pl/wsdl.php/wiersz_1", RequestNamespace="http://www.piotr-majewski.com.pl", ResponseNamespace="http://www.piotr-majewski.com.pl")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public int wiersz_1(int year) {
            object[] results = this.Invoke("wiersz_1", new object[] {
                        year});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void wiersz_1Async(int year) {
            this.wiersz_1Async(year, null);
        }
        
        /// <remarks/>
        public void wiersz_1Async(int year, object userState) {
            if ((this.wiersz_1OperationCompleted == null)) {
                this.wiersz_1OperationCompleted = new System.Threading.SendOrPostCallback(this.Onwiersz_1OperationCompleted);
            }
            this.InvokeAsync("wiersz_1", new object[] {
                        year}, this.wiersz_1OperationCompleted, userState);
        }
        
        private void Onwiersz_1OperationCompleted(object arg) {
            if ((this.wiersz_1Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.wiersz_1Completed(this, new wiersz_1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void _odCompletedEventHandler(object sender, _odCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class _odCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal _odCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void wierszCompletedEventHandler(object sender, wierszCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class wierszCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal wierszCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void ryczaltCompletedEventHandler(object sender, ryczaltCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ryczaltCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ryczaltCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void wiersz_1CompletedEventHandler(object sender, wiersz_1CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class wiersz_1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal wiersz_1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591