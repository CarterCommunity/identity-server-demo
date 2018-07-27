namespace WebApp
{
    using Carter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using IdentityServer;


    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());


            services.AddCarter();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "Http://localhost:5000";
                    opt.RequireHttpsMetadata = false;
                    opt.ApiName = "api";
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseCarter();
        }
    }
}