using Domain.Enums;

namespace Application.Interfaces.Usecases
{
    public interface IDayCycleUsecase
    {
        void UpdateTime(float deltaTime);
        void SetTime(TimeOfDay timeOfDay);
    }
}