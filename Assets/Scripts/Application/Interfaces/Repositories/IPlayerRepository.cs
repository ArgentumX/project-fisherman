using Application.Interfaces.Repositories.abstraction;
using Domain.Models.Entities.Player;

namespace Application.Interfaces.Repositories
{
    public interface IPlayerRepository : ISingletonRepository<Player>
    {
    }
}