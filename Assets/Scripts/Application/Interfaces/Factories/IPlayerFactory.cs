using Domain.Models.Entities.Player;

namespace Application.Interfaces.Factories
{
    public interface IPlayerFactory
    {
        Player CreateDefault();
        Player Create(PlayerDto dto);
    }
}