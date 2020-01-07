using System;
using System.Net.Http;
using IdentityModel.Client;

namespace token_client
{
    class Program
    {
        static void Main(string[] args)
        {
            // If using local IdentityServer4 instance with a development cert and a non-trusted certificate:
            //
            //
            // var client = new HttpClient(new HttpClientHandler {
            //     ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            // });

            var client = new HttpClient();

            var discovery = client.GetDiscoveryDocumentAsync("https://demo.identityserver.io").Result;

            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
            }

            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "m2m",
                ClientSecret = "secret",
                Scope = "api"
            }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
        }
    }
}
