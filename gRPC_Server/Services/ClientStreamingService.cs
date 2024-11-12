using Grpc.Core;
using gRPC_Client_Streaming_Server;

namespace gRPC_Server.Services
{
    public class ClientStreamingService : ClientStreaming.ClientStreamingBase
    {
        public override async Task<ClientStreamingResponse> SendClientStreamingMessage(IAsyncStreamReader<ClientStreamingRequest> requestStream, ServerCallContext context)
        {

            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"Message : {requestStream.Current.Message}, Name : {requestStream.Current.Name}");
            }


            return new ClientStreamingResponse
            {
                Message = "mesaj alındı."
            };
        }
    }
}
