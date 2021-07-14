using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SalesProductApi.DB;
using SalesProductApi.Models;

namespace salesproductapi.Services
{
    public class ProductService : SalesProductApi.ProductServiceProto.ProductServiceProtoBase
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext context)
        {
            _context = context;
        }
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

                    var productsDb = _context.Products.ToList();

                    productsDb.ToList().ForEach(productDb => {
                        var product = new SalesProductApi.ProductResponse();
                        product.Id = productDb.ProductId;
                        product.Description = productDb.Description;
                        product.Amount = productDb.Amount.ToString();
                        product.Price = productDb.Price.ToString();
                        product.Status = productDb.Status.ToString();
                        products.Add(product);
                    });

                    response.Items.AddRange(products);
                    return Task.FromResult(response);
        }


        public override Task<SalesProductApi.ProductResponse> GetProduct(SalesProductApi.ProductId request, ServerCallContext context)
        {
                    SalesProductApi.ProductResponse product = new SalesProductApi.ProductResponse();
                    product.Id = 1;
                    product.Description = "Product 001";
                    product.Amount = "200";
                    product.Price = "200";
                    product.Status = "Active";
                    return Task.FromResult(product);
        }


        public override Task<SalesProductApi.UpdateAmountResponse> UpdateAmount(SalesProductApi.ItemUpdateAmount request, ServerCallContext context)
        {
                    SalesProductApi.UpdateAmountResponse response = new SalesProductApi.UpdateAmountResponse();
                    response.Message = "OK";
                    var products = request.Items; 
                    products.ToList().ForEach(product => {
                        var id =  product.Id;
                        var amount =  product.Amount;
                    });
                    return Task.FromResult(response);
        }



    }
}