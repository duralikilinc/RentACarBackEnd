using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {

                var result = from c in  filter == null ? context.Car: context.Car.Where(filter)
                             join b in context.Brand on c.BrandId equals b.Id
                             join col in context.Color on c.ColorId equals col.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Name,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 ColorId = c.ColorId,
                                 BrandId = b.Id,
                                 MinFindexScore = c.MinFindexScore,
                                 CarImages = (from img in context.CarImage
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList()

                             };
                return result.ToList();
            }
        }


    }
}
