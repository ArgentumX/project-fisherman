using Domain.Models.Entities.DayCycle;

namespace Application.Interfaces.Factories
{
    public interface IDayCycleFactory
    {
        DayCycle CreateDefault();
        DayCycle Create(DayCycleDto dto);
    }
}