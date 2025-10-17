using Domain.Enums;
using Domain.Models.Entities.DayCycle;

namespace Application.Interfaces.Usecases
{
    public interface IDayCycleUsecase
    {
        void UpdateTime(DayCycle dayCycle, float deltaTime);
        void SetTime(DayCycle dayCycle,TimeOfDay timeOfDay);
    }
}