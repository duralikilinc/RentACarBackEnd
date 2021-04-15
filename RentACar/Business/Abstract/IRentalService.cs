using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
   public interface IRentalService:IBussiniesRepository<Rental>
   {
       IDataResult<List<RentalDetailDto>> GetRentalDetailDto();
       IDataResult<List<RentalDetailDto>> GetRentalByCustomerId(int customerId);
        IResult IsRental(int carId);

       IResult CheckFindexScore(int carId, int customerId);
   }
}
