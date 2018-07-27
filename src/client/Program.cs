namespace Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;

    class Program
    {
        static async Task Main(string[] args)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:6666");
            if (disco.IsError)
            {
                await Console.Error.WriteLineAsync($"Discovery error: {disco.Error}");
            }
            
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api");


            if (tokenResponse.IsError)
            {
                await Console.Error.WriteLineAsync($"Token error: {tokenResponse.Error}");
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            
            // Start making Requests to the WebApp
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:8888/insecure");
            await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            
            response = await client.GetAsync("http://localhost:8888/secure");
            await Console.Out.WriteLineAsync(response.StatusCode.ToString());

            client.SetBearerToken(tokenResponse.AccessToken);
            response = await client.GetAsync("http://localhost:8888/secure");
            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(content);
            }
        }
    }
}
