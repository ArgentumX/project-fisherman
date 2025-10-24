namespace Domain.Models.Common
{
    public interface IStaminaConsumer
    {
       bool TryConsumeStamina(object sender, float amount); 
       void RestoreStamina(object sender, float amount);
       void SetStamina(object sender, float amount);
       bool HasStamina(float amount);
       float Stamina { get; }
       float MaxStamina { get; }
    }
}