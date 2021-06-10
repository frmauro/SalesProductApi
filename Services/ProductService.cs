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


        public override Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request, ServerCallContext context)
        {
                    List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
                    SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();
                    SalesProductApi.ProductResponse product = new SalesProductApi.ProductResponse();

                    product.Id = 1;
                    product.Description = "Product 001";
                    product.Amount = "200";
                    product.Price = "200";
                    product.Status = "Active";
                    products.Add(product);
                    
                    response.Items.AddRange(products);

                    return Task.FromResult(response);
        }
    }
}