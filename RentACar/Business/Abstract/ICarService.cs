using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId);
        IResult Add(Car car);
        IDataResult<Car> GetById(int carId);
        IDataResult<short> GetByFindexScore(int carId);

        IDataResult<List<CarDetailDto>> GetCarDetailsFilter(int brandId, int colorId);
    }
}
