using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICrediCardService
    {
        IResult Add(CrediCard card);
        IDataResult<List<CrediCard>> GetByUserId(int id);
        IDataResult<List<CrediCard>> GetAll();
    }
}
