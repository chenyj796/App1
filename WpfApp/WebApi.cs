using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Briver.Framework;

namespace WpfApp
{
    public interface IWebApi : IComposition
    {

        HttpClient Client { get; }
    }

    internal class WebApi : IWebApi
    {
        private Lazy<HttpClient> _client = new Lazy<HttpClient>(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (rsp, cert, chain, err) => true;
            return new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:6000/api/"),
            };

        });

        public HttpClient Client => _client.Value;
    }

    internal class WebApiInitialization : ISystemInitialization
    {
        private static void RunWebHost()
        {
            WebApp.Program.Main(new string[0]);
        }

        private Thread thread = new Thread(RunWebHost)
        {
            IsBackground = true,
            Priority = ThreadPriority.BelowNormal
        };

        public void Execute()
        {
            if (thread.ThreadState.HasFlag(ThreadState.Unstarted))
            {
                thread.Start();
            }
        }
    }
}
