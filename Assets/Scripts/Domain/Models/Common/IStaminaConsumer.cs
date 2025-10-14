namespace Domain.Models.Common
{
    public interface IStaminaConsumer
    {
       bool TryConsumeStamina(float amount); 
       void RestoreStamina(float amount);
       bool HasStamina(float amount);
       float Stamina { get; }
       float StaminaMax { get; }
    }
}