using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesProductApi.DB;
using SalesProductApi.Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

         [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> GetProducts()
        {
            var products = _context.Products;
            var vms = new List<ProductViewModel>();

            products.ToList().ForEach(product => {
                    var vm = new ProductViewModel();
                    vm.Amount = product.Amount;
                    vm.Description = product.Description;
                    vm.Id = product.ProductId;
                    vm.Status = product.Status.ToString();
                    vm.Price = product.Price.ToString();
                    vms.Add(vm);
            });

            return vms;
        }


        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel();
            vm.Amount = product.Amount;
            vm.Description = product.Description;
            vm.Id = product.ProductId;
            vm.Status = product.Status.ToString();
            vm.Price = product.Price.ToString();
            
            return vm;
        }

        [HttpPost]
        public ActionResult<ProductViewModel> Post(ProductViewModel vm)
        {
                var product = new Product 
                {
                     Description = vm.Description,
                     Amount = vm.Amount, 
                     Price = Convert.ToDecimal(vm.Price) 
                };

                if (vm.Status == "Active")
                    product.Status = ProductStatus.Active;
                else
                    product.Status = ProductStatus.Inactive;

                    _context.Add(product);
                    _context.SaveChanges();

                    return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }


        [HttpPut]
        public ActionResult<ProductViewModel> Put(ProductViewModel vm)
        {
             var product =  _context.Products.Find(vm.Id);

            if (product == null)
            {
                return NotFound();
            }
                
            product.Description = vm.Description;
            product.Amount = vm.Amount; 
            product.Price = Convert.ToDecimal(vm.Price); 
                

            if (vm.Status == "Active")
                product.Status = ProductStatus.Active;
            else
                product.Status = ProductStatus.Inactive;

                _context.Update(product);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

    }
}