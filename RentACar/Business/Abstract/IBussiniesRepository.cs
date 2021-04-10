using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBussiniesRepository<T>
    {
        IDataResult<List<T>> GetAll();

        IDataResult<List<T>> GetById(int entityId);

        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
