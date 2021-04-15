using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;
        private IFindexService _findexService;

        public RentalManager(IRentalDal rentalDal,ICarService carService, IFindexService findexService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _findexService = findexService;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.SuccessListed);
        }

        public IDataResult<List<Rental>> GetById(int entityId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.Id == entityId), Messages.SuccessListed);
        }

        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(IsRental(rental.CarId),CheckFindexScore(rental.CarId,rental.CustomerId));
            if (result !=null)
            {
                return  new ErrorResult(Messages.RentalInvalid);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.SuccessAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.SuccessDeleted);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailDto()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetailDto(), Messages.SuccessListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetailDto(k=>k.CustomerId==customerId), Messages.SuccessListed);
        }

        public IResult IsRental(int carId)
        {
            bool result = _rentalDal.GetAll(k=>k.CarId==carId&& k.ReturnDate>=DateTime.Now).Any();
            
            if (result)
            {
                return new ErrorResult(Messages.CarError);
            }
            return new SuccessResult();
        }

        public IResult CheckFindexScore(int carId, int customerId)
        {
            var customerFindex=_findexService.GetByCustomerId(customerId).Data.Score;
            var carFindex=_carService.GetByFindexScore(carId).Data;

            if (carFindex>customerFindex)
            {
                return new ErrorResult(Messages.FindexError);
            }

            return new SuccessResult();

        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.SuccessUpdated);
        }
    }
}
