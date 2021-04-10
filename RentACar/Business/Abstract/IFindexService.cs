using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFindexService
    {
        IDataResult<Findex> GetById(int id);
        IDataResult<Findex> GetByCustomerId(int customerId);
        
        IDataResult<List<Findex>> GetAll();
        IResult Add(Findex findeks);
        IDataResult<Findex> CalculateFindexScore(Findex findeks);
        IResult Update(Findex findex);

    }
}
