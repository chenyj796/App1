using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Owin.Hosting;

namespace WpfApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private class InnerApplication : Briver.Framework.Application
        {
            protected override Information LoadInformation()
            {
                return new Information
                {
                    Name = "IntegrationExample",
                    Version = new Version(1, 0, 0).ToString(),
                    DisplayName = "WPF集成AspNetCore示例"
                };
            }
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Briver.Framework.SystemContext.Initialize(new InnerApplication());
        }

    }

    
}
