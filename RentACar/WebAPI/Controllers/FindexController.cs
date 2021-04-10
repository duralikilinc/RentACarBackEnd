using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Routing;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindexController : ControllerBase
    {
        private IFindexService _findexService;

        public FindexController(IFindexService findexService)
        {
            _findexService = findexService;
        }

        [HttpGet]
        [Route("getFindexByCustomerId/{customerId}")]
        public IActionResult GetFindexByCustomerId(int customerId)
        {
            var result = _findexService.GetByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
