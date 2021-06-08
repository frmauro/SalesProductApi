using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace salesproductapi.Services
{
    public class ProductService : SalesProductApi.ProductServiceProto.ProductServiceProtoBase
    {
        public override Task<SalesProductApi.ProductReply> SendProduct(SalesProductApi.ProductRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SalesProductApi.ProductReply
            {
                Message = "Product - id: " + request.Id + " description: " + request.Description + " amount: " + request.Amount + " price: " + request.Price + " status: " + request.Status
            });
        }
    }
}