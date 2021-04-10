using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CrediCardManager : ICrediCardService
    {
        private ICrediCardDal _crediCardDal;

        public CrediCardManager(ICrediCardDal crediCardDal)
        {
            _crediCardDal = crediCardDal;
        }

        [ValidationAspect(typeof(CrediCardValidation))]
        public IResult Add(CrediCard card)
        {
            _crediCardDal.Add(card);
            return new SuccessResult(Messages.SuccessAdded);
        }
        public IDataResult<List<CrediCard>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<CrediCard>>(_crediCardDal.GetAll(c => c.UserId == userId));
        }

        public IDataResult<List<CrediCard>> GetAll()
        {
            return new SuccessDataResult<List<CrediCard>>(_crediCardDal.GetAll(), Messages.SuccessListed);
        }
    }
}
