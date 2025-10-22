using System;

namespace Domain.Models.Common
{
    public abstract class BaseModel
    {
        // TODO final split entities and static objects
        public Guid Id { get; private set; }

        protected BaseModel(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}