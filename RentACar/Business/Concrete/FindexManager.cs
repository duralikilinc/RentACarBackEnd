using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class FindexManager:IFindexService
   {
       private IFindexDal _findexDal;

       public FindexManager(IFindexDal findexDal)
       {
           _findexDal = findexDal;
       }

       public IDataResult<Findex> GetById(int id)
        {
            return new SuccessDataResult<Findex>(_findexDal.Get(k=>k.Id==id));
        }

       public IDataResult<Findex> GetByCustomerId(int customerId)
       {
           var result = _findexDal.Get(k => k.CustomerId == customerId);
           if (result==null)
           {
               return new ErrorDataResult<Findex>("Findex Skoru Bulunamadı");
           }
           return new SuccessDataResult<Findex>(result);
       }

      

       public IDataResult<List<Findex>> GetAll()
        {
            return new SuccessDataResult<List<Findex>>(_findexDal.GetAll());
        }

        public IResult Update(Findex findex)
        {
            var result = CalculateFindexScore(findex).Data;
            _findexDal.Update(result);

            return new SuccessResult(Messages.SuccessUpdated);
        }
       
        public IResult Add(Findex findex)
        {
            var result = CalculateFindexScore(findex).Data;
            _findexDal.Add(result);
            return new SuccessResult("Findex Skoru Eklendi");
        }

        public IDataResult<Findex> CalculateFindexScore(Findex findex)
        {
           findex.Score=(short)new Random().Next(0,1901);
           return new SuccessDataResult<Findex>(findex);
        }
    }
}
