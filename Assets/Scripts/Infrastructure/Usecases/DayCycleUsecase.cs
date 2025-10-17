using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Entities.DayCycle;
using Zenject;

namespace Infrastructure.Usecases
{
    public class DayCycleUsecase : IDayCycleUsecase
    {
        public void UpdateTime(DayCycle dayCycle,float deltaTime)
        {
            dayCycle.UpdateTime(this, deltaTime);    
        }

        public void SetTime(DayCycle dayCycle,TimeOfDay timeOfDay)
        {
            throw new System.NotImplementedException();
        }
        
    }
}