using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        
    }
}