using System;
using System.Collections;
using Domain.Models.Common;

public interface IIdentifiableRepository<T> where T : BaseModel
{
    T Get(Guid id);
    IEnumerable GetAll();
    void Save(T entity);
}