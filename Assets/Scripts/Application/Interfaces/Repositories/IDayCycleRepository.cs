using Domain.Models.Entities.DayCycle;

namespace Application.Interfaces.Repositories
{
    public interface IDayCycleRepository
    {
        DayCycle LoadDayCycle();
        void SaveDayCycle(DayCycle dayCycle);
    }
}