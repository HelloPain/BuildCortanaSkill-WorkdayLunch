using System;
using Autofac;
using System.Web.Http;
using System.Configuration;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Reflection;

namespace Bot_Application
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //string uriString = "https://bot-data.documents.azure.com:443/";
            //string key = "WLVFk6aL86IyrwOTmfWmH32ljeAsOswLapwnbdANRMn0mIjXTx8et45HhilyB3Hm3qQYHzs2ku7Ytx3w52Whfg==";
            //var uri = new Uri(uriString);
            ////var key = ConfigurationManager.AppSettings["DocumentDbKey"];
            //var store = new DocumentDbBotDataStore(uri, key);

            //Conversation.UpdateContainer(
            //                builder =>
            //                {
            //                    builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));
            //                    builder.Register(c => store)
            //                                    .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
            //                                    .AsSelf()
            //                                    .SingleInstance();
            //                });
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
