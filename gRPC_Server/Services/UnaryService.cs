using Grpc.Core;
using gRPC_Unary_Server;

namespace gRPC_Server.Services
{
    public class UnaryService : Unary.UnaryBase
    {
        public override async Task<UnaryResponse> SendUnaryMessage(UnaryRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Message : {request.Message}, Name : {request.Name}");

            return new UnaryResponse
            {
                Message = "mesal alındı."
            };
        }
    }
}
