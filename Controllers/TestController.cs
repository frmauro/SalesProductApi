using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesProductApi.Models;

namespace Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public String Get()
        {
            return string.Format("TESTE_{0}", DateTime.Now);
        }

        [HttpPost()]
        public ActionResult<String> Post(TestViewModel vm)
        {
            var teste = string.Empty;
            try
            {
                teste = vm.Name;
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(teste);
        }
        
    }
}