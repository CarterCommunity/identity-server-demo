namespace WebApp
{
    using Carter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCarter();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(opt =>
                {
                    opt.Authority = "Http://localhost:6666";
                    opt.RequireHttpsMetadata = false;
                    opt.ApiName = "api";
                });
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCarter();
        }
    }
}
