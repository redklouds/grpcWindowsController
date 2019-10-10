using System;

using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace WindowsGrpcControllerServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<ToggleResponse> ToggleWindowSwitching(ToggleRequest tg, ServerCallContext context)
        {
            return Task.FromResult(
                new ToggleResponse
                {
                    IsOn = true
                });
        }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine("Searving the request");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
