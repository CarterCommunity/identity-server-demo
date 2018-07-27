namespace WebApp
{
    using System.Linq;
    using Carter;
    using Carter.Response;
    using Microsoft.AspNetCore.Http;

    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            Get("/insecure", async(req, res, routeData) => { await res.WriteAsync("Hello World!"); });
            Get("/secure", async(req, res, routeData) =>
            {
                if (!req.HttpContext.User.Identity.IsAuthenticated)
                {
                    res.StatusCode = 401;
                }
                await res.AsJson(req.HttpContext.User.Claims.Select(c => new {c.Type, c.Value}));
            });
        }
    }
}
