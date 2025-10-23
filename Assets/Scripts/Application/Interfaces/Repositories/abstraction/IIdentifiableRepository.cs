using System;
using System.Collections.Generic;
using Domain.Models.Common;

public interface IIdentifiableRepository<T> where T : BaseModel
{
    T Get(Guid id);
    IEnumerable<T> GetAll();
    void Save(T entity);
}