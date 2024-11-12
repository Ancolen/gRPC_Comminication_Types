using Grpc.Net.Client;
using gRPC_Bi_Directional_Streaming_Client;
using gRPC_Client_Streaming_Client;
using gRPC_Server;
using gRPC_Server_Streaming_Client;
using gRPC_Unary_Client;

namespace gRPC_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = GrpcChannel.ForAddress("http://localhost:5095");


            //var greeterClient = new Greeter.GreeterClient(client);

            //HelloReply result = await greeterClient.SayHelloAsync(new HelloRequest
            //{
            //    Name = "gRPC"
            //});


            //Unary
            //var unaryClient = new Unary.UnaryClient(client);

            //UnaryResponse response = await unaryClient.SendUnaryMessageAsync(new UnaryRequest
            //{
            //    Message = "Unary",
            //    Name    = "gRPC"
            //});
            //Console.WriteLine(response.Message);



            //server streaming
            //var serverStreamingClient = new ServerStreaming.ServerStreamingClient(client);

            //var response = serverStreamingClient.SendServerStreamingMessage(new ServerStreamingRequest
            //{
            //    Message = "Server Streaming",
            //    Name = "gRPC"
            //});

            //CancellationTokenSource cts = new CancellationTokenSource();

            //while (await response.ResponseStream.MoveNext(cts.Token))
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Message);
            //}


            //client streaming
            //var clientStreamingClient = new ClientStreaming.ClientStreamingClient(client);

            //var request = clientStreamingClient.SendClientStreamingMessage();

            //for (int i = 0; i < 5; i++)
            //{
            //    await request.RequestStream.WriteAsync(new ClientStreamingRequest
            //    {

            //        Message = Console.ReadLine(),
            //        Name = "gRPC"
            //    });
            //}
            //request.RequestStream.CompleteAsync();
            //Console.WriteLine((await request.ResponseAsync).Message);



            //Bi-Directional Streaming
            var biDirectionalStreamingClient = new BiDirectionalStreaming.BiDirectionalStreamingClient(client);

            var request = biDirectionalStreamingClient.SendBiDirectionalStreamingMessage();

            var task1   = Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    await Task.Delay(1000);
                    await request.RequestStream.WriteAsync(new BiDirectionalStreamingRequest
                    {
                        Message = "Bi-Directional " + i,
                        Name = "gRPC"
                    });
                }
            });

            CancellationTokenSource cts = new CancellationTokenSource();
            
            while (await request.ResponseStream.MoveNext(cts.Token))
            {
                Console.WriteLine(request.ResponseStream.Current.Message);
            }

            await task1;
            await request.RequestStream.CompleteAsync();

        }
    }
}
