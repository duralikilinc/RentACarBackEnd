using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrediCardsController : ControllerBase
    {
        private ICrediCardService _crediCardService;

        public CrediCardsController(ICrediCardService crediCardService)
        {
            _crediCardService = crediCardService;
        }

        

        [HttpGet("getbyUserId/{customerId}")]
        public IActionResult GetByUserId(int customerId)
        {
            var result = _crediCardService.GetByUserId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _crediCardService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Post(CrediCard crediCard)
        {
            var result = _crediCardService.Add(crediCard);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
