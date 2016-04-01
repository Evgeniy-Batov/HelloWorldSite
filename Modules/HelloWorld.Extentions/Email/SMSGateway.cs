﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace WebSite.Email
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ServiceSoap", Namespace = "http://turbosms.in.ua/api/Turbo")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback AuthOperationCompleted;

        private System.Threading.SendOrPostCallback GetCreditBalanceOperationCompleted;

        private System.Threading.SendOrPostCallback SendSMSOperationCompleted;

        private System.Threading.SendOrPostCallback GetNewMessagesOperationCompleted;

        private System.Threading.SendOrPostCallback GetMessageStatusOperationCompleted;

        /// <remarks/>
        public Service()
        {
            this.Url = "http://turbosms.in.ua/api/soap.html";
        }

        /// <remarks/>
        public event AuthCompletedEventHandler AuthCompleted;

        /// <remarks/>
        public event GetCreditBalanceCompletedEventHandler GetCreditBalanceCompleted;

        /// <remarks/>
        public event SendSMSCompletedEventHandler SendSMSCompleted;

        /// <remarks/>
        public event GetNewMessagesCompletedEventHandler GetNewMessagesCompleted;

        /// <remarks/>
        public event GetMessageStatusCompletedEventHandler GetMessageStatusCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://turbosms.in.ua/api/Turbo/Auth", RequestNamespace = "http://turbosms.in.ua/api/Turbo", ResponseNamespace = "http://turbosms.in.ua/api/Turbo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Auth(string login, string password)
        {
            object[] results = this.Invoke("Auth", new object[] {
                    login,
                    password});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginAuth(string login, string password, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Auth", new object[] {
                    login,
                    password}, callback, asyncState);
        }

        /// <remarks/>
        public string EndAuth(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void AuthAsync(string login, string password)
        {
            this.AuthAsync(login, password, null);
        }

        /// <remarks/>
        public void AuthAsync(string login, string password, object userState)
        {
            if ((this.AuthOperationCompleted == null))
            {
                this.AuthOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthOperationCompleted);
            }
            this.InvokeAsync("Auth", new object[] {
                    login,
                    password}, this.AuthOperationCompleted, userState);
        }

        private void OnAuthOperationCompleted(object arg)
        {
            if ((this.AuthCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthCompleted(this, new AuthCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://turbosms.in.ua/api/Turbo/GetCreditBalance", RequestNamespace = "http://turbosms.in.ua/api/Turbo", ResponseNamespace = "http://turbosms.in.ua/api/Turbo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetCreditBalance()
        {
            object[] results = this.Invoke("GetCreditBalance", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetCreditBalance(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetCreditBalance", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndGetCreditBalance(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GetCreditBalanceAsync()
        {
            this.GetCreditBalanceAsync(null);
        }

        /// <remarks/>
        public void GetCreditBalanceAsync(object userState)
        {
            if ((this.GetCreditBalanceOperationCompleted == null))
            {
                this.GetCreditBalanceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCreditBalanceOperationCompleted);
            }
            this.InvokeAsync("GetCreditBalance", new object[0], this.GetCreditBalanceOperationCompleted, userState);
        }

        private void OnGetCreditBalanceOperationCompleted(object arg)
        {
            if ((this.GetCreditBalanceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCreditBalanceCompleted(this, new GetCreditBalanceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://turbosms.in.ua/api/Turbo/SendSMS", RequestNamespace = "http://turbosms.in.ua/api/Turbo", ResponseNamespace = "http://turbosms.in.ua/api/Turbo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("ResultArray")]
        public string[] SendSMS(string sender, string destination, string text, string wappush)
        {
            object[] results = this.Invoke("SendSMS", new object[] {
                    sender,
                    destination,
                    text,
                    wappush});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginSendSMS(string sender, string destination, string text, string wappush, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("SendSMS", new object[] {
                    sender,
                    destination,
                    text,
                    wappush}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndSendSMS(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void SendSMSAsync(string sender, string destination, string text, string wappush)
        {
            this.SendSMSAsync(sender, destination, text, wappush, null);
        }

        /// <remarks/>
        public void SendSMSAsync(string sender, string destination, string text, string wappush, object userState)
        {
            if ((this.SendSMSOperationCompleted == null))
            {
                this.SendSMSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendSMSOperationCompleted);
            }
            this.InvokeAsync("SendSMS", new object[] {
                    sender,
                    destination,
                    text,
                    wappush}, this.SendSMSOperationCompleted, userState);
        }

        private void OnSendSMSOperationCompleted(object arg)
        {
            if ((this.SendSMSCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendSMSCompleted(this, new SendSMSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://turbosms.in.ua/api/Turbo/GetNewMessages", RequestNamespace = "http://turbosms.in.ua/api/Turbo", ResponseNamespace = "http://turbosms.in.ua/api/Turbo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("ResultArray")]
        public string[] GetNewMessages()
        {
            object[] results = this.Invoke("GetNewMessages", new object[0]);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetNewMessages(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetNewMessages", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string[] EndGetNewMessages(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void GetNewMessagesAsync()
        {
            this.GetNewMessagesAsync(null);
        }

        /// <remarks/>
        public void GetNewMessagesAsync(object userState)
        {
            if ((this.GetNewMessagesOperationCompleted == null))
            {
                this.GetNewMessagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNewMessagesOperationCompleted);
            }
            this.InvokeAsync("GetNewMessages", new object[0], this.GetNewMessagesOperationCompleted, userState);
        }

        private void OnGetNewMessagesOperationCompleted(object arg)
        {
            if ((this.GetNewMessagesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetNewMessagesCompleted(this, new GetNewMessagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://turbosms.in.ua/api/Turbo/GetMessageStatus", RequestNamespace = "http://turbosms.in.ua/api/Turbo", ResponseNamespace = "http://turbosms.in.ua/api/Turbo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetMessageStatus(string MessageId)
        {
            object[] results = this.Invoke("GetMessageStatus", new object[] {
                    MessageId});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetMessageStatus(string MessageId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetMessageStatus", new object[] {
                    MessageId}, callback, asyncState);
        }

        /// <remarks/>
        public string EndGetMessageStatus(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GetMessageStatusAsync(string MessageId)
        {
            this.GetMessageStatusAsync(MessageId, null);
        }

        /// <remarks/>
        public void GetMessageStatusAsync(string MessageId, object userState)
        {
            if ((this.GetMessageStatusOperationCompleted == null))
            {
                this.GetMessageStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMessageStatusOperationCompleted);
            }
            this.InvokeAsync("GetMessageStatus", new object[] {
                    MessageId}, this.GetMessageStatusOperationCompleted, userState);
        }

        private void OnGetMessageStatusOperationCompleted(object arg)
        {
            if ((this.GetMessageStatusCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMessageStatusCompleted(this, new GetMessageStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void AuthCompletedEventHandler(object sender, AuthCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal AuthCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetCreditBalanceCompletedEventHandler(object sender, GetCreditBalanceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCreditBalanceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetCreditBalanceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void SendSMSCompletedEventHandler(object sender, SendSMSCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendSMSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SendSMSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetNewMessagesCompletedEventHandler(object sender, GetNewMessagesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetNewMessagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetNewMessagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetMessageStatusCompletedEventHandler(object sender, GetMessageStatusCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMessageStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetMessageStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    public class SMSWorker : Service
    {
        // ключ сессии для хранения инстанса объекта
        private const string SessionKey = "SMSWorker_Instance";

        class SMSProxy
        {
            public string Host;
            public int Port;
        }

        // конструктор
        public SMSWorker()
        {
            this.CookieContainer = new CookieContainer();

            // Если используется прокси
            if (ConfigurationManager.AppSettings["UseProxy"] == "true")
            {
                // Вычитываем настройки прокси из Web.Config - а
                SMSProxy pr = new SMSProxy();
                pr.Host = ConfigurationManager.AppSettings["ProxyHost"];
                pr.Port = Convert.ToInt32(ConfigurationManager.AppSettings["ProxyPort"]);

                WebProxy proxy = new WebProxy(pr.Host, pr.Port);
                this.Proxy = proxy;
            }
        }

        public static SMSWorker GetInstance()
        {
            if (HttpContext.Current == null)
                return new SMSWorker();

            // определяем, существует ли объект в сессии
            SMSWorker worker = HttpContext.Current.Session[SessionKey] as SMSWorker;
            if (worker == null)
            {
                // создаем объект
                worker = new SMSWorker();
                // помещаем в сессию
                HttpContext.Current.Session[SessionKey] = worker;
            }
            return worker;
        }

        /// <summary>
        ///    Обнуляем объект в сессии (если нужно)
        /// </summary>
        public void CloseSession()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session[SessionKey] = null;
        }
    }

}