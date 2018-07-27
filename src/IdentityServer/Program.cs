namespace IdentityServer
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer";
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:6666")
                .UseStartup<Startup>()
                .Build();
    }
}
