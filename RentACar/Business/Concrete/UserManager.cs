using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        private ICustomerService _customerService;
        private IFindexService _findexService;

        public UserManager(IUserDal userDal, ICustomerService customerService, IFindexService findexService)
        {
            _userDal = userDal;
            _customerService = customerService;
            _findexService = findexService;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);

            return new SuccessResult(Messages.SuccessAdded);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [SecuredOperation("user.update,moderator,admin")]
        public IResult Update(User user)
        {
            _userDal.Update(user);

            return new SuccessResult(Messages.SuccessUpdated);
        }

        [SecuredOperation("user.get,moderator,admin")]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        [TransactionScopeAspect]
        public IResult UpdateUserDetails(UserDetailForUpdateDto userDetailForUpdateDto)
        {
            var user = GetByMail(userDetailForUpdateDto.Email).Data;
            byte[] passwordHash, passwordSalt;
            if (!HashingHelper.VerifyPasswordHash(userDetailForUpdateDto.CurrentPassword,
                user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult("Hatalı parola");
            }

            user.FirstName = userDetailForUpdateDto.FirstName;
            user.LastName = userDetailForUpdateDto.LastName;
            HashingHelper.CreatePasswordHash(userDetailForUpdateDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            var customer = _customerService.GetById(userDetailForUpdateDto.CustomerId).Data;
            customer.CompanyName = userDetailForUpdateDto.CompanyName;
            _customerService.Update(customer);

            var findex = _findexService.GetByCustomerId(userDetailForUpdateDto.CustomerId).Data;
            if (findex == null)
            {
                var newFindex = new Findex
                {
                    CustomerId = userDetailForUpdateDto.CustomerId
                };
                _findexService.Add(newFindex);
            }
            else
            {
                _findexService.Update(findex);
            }


            return new SuccessResult(Messages.SuccessUpdated);
        }

        public IDataResult<UserDetailDto> GetUserDetails(string userMail)
        {
            return new SuccessDataResult<UserDetailDto>(_userDal.GetUserDetail(userMail));
        }
    }
}