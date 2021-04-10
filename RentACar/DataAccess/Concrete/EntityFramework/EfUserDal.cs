using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentACarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RentACarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public UserDetailDto GetUserDetail(string userMail)
        {
            using (var context=new RentACarContext())
            {
                var result = from customer in context.Customer
                    join users in context.User on customer.Id equals users.Id
                             where users.Email==userMail
                    join findex in context.Findex on customer.Id equals findex.CustomerId 
                    select new UserDetailDto
                    {
                        CompanyName = customer.CompanyName,
                        CustomerId = customer.Id,
                        Email = users.Email,
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        UserId = users.Id,
                        Score = findex.Score
                    };
                return result.FirstOrDefault();
            }
        }
    }
}
