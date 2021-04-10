using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IDataResult<User> GetByMail(string userMail);
        IResult Update(User user);
        IDataResult<List<User>> GetAll();

        IResult UpdateUserDetails(UserDetailForUpdateDto userDetailForUpdateDto);
        IDataResult<UserDetailDto> GetUserDetails(string userMail);
    }
}
