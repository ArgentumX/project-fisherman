using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;

namespace Application.Interfaces
{
    public interface IGameContext
    {
        public PlayerDto PlayerDto { get; }
        public DayCycleDto DayCycleDto { get; }
    }
}