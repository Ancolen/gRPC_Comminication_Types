using Grpc.Core;
using gRPC_Bi_Directional_Streaming_Server;

namespace gRPC_Server.Services
{
    public class BiDirectionalStreamingService : BiDirectionalStreaming.BiDirectionalStreamingBase
    {
        public override async Task SendBiDirectionalStreamingMessage(IAsyncStreamReader<BiDirectionalStreamingRequest> requestStream, IServerStreamWriter<BiDirectionalStreamingResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Message : {requestStream.Current.Message}, Name : {requestStream.Current.Name}");
                }
            });

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new BiDirectionalStreamingResponse
                {
                    Message = $"mesaj alındı. {i}"
                });
            }
            await task1;
        }
    }
}
