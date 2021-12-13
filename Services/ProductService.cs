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
            ProductStatus status;

            if (request.Status == "Active")
                status = ProductStatus.Active;
            else
                status = ProductStatus.Inactive;

            var product = new Product(request.Description, Convert.ToInt32(request.Amount), Convert.ToDecimal(request.Price), status);
            _context.Products.Add(product);
            _context.SaveChanges();

            return Task.FromResult(new SalesProductApi.ProductReply
            {
                Message = "Product - id: " + product.ProductId + " description: " + product.Description + " amount: " + product.Amount + " price: " + product.Price + " status: " + product.Status
            });
        }


        public override Task<SalesProductApi.ItemResponse> GetProducts(SalesProductApi.Empty request, ServerCallContext context)
        {
            List<SalesProductApi.ProductResponse> products = new List<SalesProductApi.ProductResponse>();
            SalesProductApi.ItemResponse response = new SalesProductApi.ItemResponse();

            var productsDb = _context.Products.ToList();

            productsDb.ToList().ForEach(productDb =>
            {
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
            var productDb = _context.Products.Find(request.Id);

            product.Id = productDb.ProductId;
            product.Description = productDb.Description;
            product.Amount = productDb.Amount.ToString();
            product.Price = productDb.Price.ToString();
            product.Status = productDb.Status.ToString();

            return Task.FromResult(product);
        }


        public override Task<SalesProductApi.UpdateAmountResponse> UpdateAmount(SalesProductApi.ItemUpdateAmount request, ServerCallContext context)
        {
            SalesProductApi.UpdateAmountResponse response = new SalesProductApi.UpdateAmountResponse();
            response.Message = "OK";
            var products = request.Items;
            // products.ToList().ForEach(product => {
            //     var id =  product.Id;
            //     var amount =  product.Amount;
            // });

            products.ToList().ForEach(vm =>
            {
                var product = _context.Products.Find(vm.Id);
                product.Amount -= Convert.ToInt32(vm.Amount);
                _context.Update(product);
                _context.SaveChanges();
            });

            return Task.FromResult(response);
        }



    }
}