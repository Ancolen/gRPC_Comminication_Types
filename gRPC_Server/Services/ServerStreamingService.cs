
using gRPC_Server_Streaming_Server;
using Grpc.Core;

namespace gRPC_Server.Services
{
    public class ServerStreamingService : ServerStreaming.ServerStreamingBase
    {
        public override async Task SendServerStreamingMessage(ServerStreamingRequest request, IServerStreamWriter<ServerStreamingResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message : {request.Message}, Name : {request.Name}");

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new ServerStreamingResponse
                {
                    Message = $"mesaj alındı. {i}"
                });
            }
        }
    }
}
