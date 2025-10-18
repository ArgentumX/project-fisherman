using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerUsecase : IPlayerUsecase
    {
        public void TakeDamage(Player player, int amount)
        {
            player.TakeDamage(amount);
        }
    }
}