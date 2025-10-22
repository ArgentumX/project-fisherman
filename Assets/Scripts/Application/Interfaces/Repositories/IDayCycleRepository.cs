using Application.Interfaces.Repositories.abstraction;
using Domain.Models.Entities.DayCycle;

namespace Application.Interfaces.Repositories
{
    public interface IDayCycleRepository : ISingletonRepository<DayCycle>
    {
    }
}