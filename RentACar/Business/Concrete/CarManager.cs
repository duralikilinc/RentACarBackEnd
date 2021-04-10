using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{

    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.SuccessListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.Id == carId));
        }

        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("car.add,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);

            return new SuccessResult(Messages.SuccessAdded);
        }



        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == carId));
        }

    

        public IDataResult<short> GetByFindexScore(int carId)
        {
            var result = _carDal.GetCarDetails(k => k.Id == carId).Select(k => k.MinFindexScore).FirstOrDefault();

            return new SuccessDataResult<short>(result);
        }


        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsFilter(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(
                _carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId),
                Messages.SuccessListed);
        }

        private List<CarDetailDto> CheckNullImageList(List<CarDetailDto> carDetailDtos)
        {

            foreach (var carDetailDto in carDetailDtos)
            {
                CheckNullImageSingle(carDetailDto.CarImages, carDetailDto.CarId);
            }
            return carDetailDtos;
        }
        private List<CarImage> CheckNullImageSingle(List<CarImage> carImages, int carId)
        {
            string path = @"\Uploads\default.png";
            if (carImages.Count == 0)
            {
                carImages.Add(new CarImage { ImagePath = path, CarId = carId, Date = null });
            }
            return carImages;
        }
    }
}
