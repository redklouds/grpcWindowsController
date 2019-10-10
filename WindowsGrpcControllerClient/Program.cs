using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsGrpcControllerServer;

namespace WindowsGrpcControllerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);


            GrpcChannel channel = GrpcChannel.ForAddress("https://23.100.19.203:5000/",
                new GrpcChannelOptions
                {
                    HttpClient = httpClient
                });


            //GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5000/");
            var client = new Greeter.GreeterClient(channel);
            while (true)
            {
                var reply = await client.SayHelloAsync(new HelloRequest()
                {
                    Name = "Danny"
                });
                var reply1 = await client.ToggleWindowSwitchingAsync(new ToggleRequest());


                Console.WriteLine(reply.Message);
                Console.WriteLine(reply1.IsOn);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
