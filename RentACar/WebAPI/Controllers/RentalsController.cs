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
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("isrental/{carId}")]
        public IActionResult IsRental(int carId)
        {
            var result = _rentalService.IsRental(carId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getFindexControl/{customerId}/{carId}")]
        public IActionResult GetFindexControll(int customerId, int carId)
        {
            var result = _rentalService.CheckFindexScore(carId, customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getRentalDetails")]
        public IActionResult GetRentalDetails()
        {
            var result = _rentalService.GetRentalDetailDto();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _rentalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getByCustomerIdDetail/{id}")]
        public IActionResult GetByCustomerIdDetail(int id)
        {
            var result = _rentalService.GetRentalByCustomerId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult Post(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
