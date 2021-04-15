using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetailDto(Expression<Func<Rental, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from renta in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join custo in context.Customer
                                 on renta.CustomerId equals custo.Id

                             join use in context.User
                                 on custo.UserId equals use.Id

                             join car in context.Car
                                 on renta.CarId equals car.Id

                             join brand in context.Brand
                                 on car.BrandId equals brand.Id


                             select new RentalDetailDto
                             {
                                 RentalId = renta.Id,
                                 BrandName = brand.Name,
                                 FirstName = use.FirstName,
                                 LastName = use.LastName,
                                 RentDate = renta.RentDate,
                                 ReturnDate = renta.ReturnDate,
                                 customerId = custo.Id,
                                 CarName = car.Name,
                                 CarId = car.Id
                             };

                return result.ToList();
            }
        }
    }
}
