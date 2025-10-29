using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerMutationsUsecase : IMutationsUsecase
    {
        private IMutationFactory _mutationFactory;
        
        [Inject]
        private PlayerMutationsUsecase(IMutationFactory mutationFactory) {
            _mutationFactory = mutationFactory;
        }
        
        public void AddRandomMutation(Player player) {
            var type = SelectRandomMutationType();
            AddMutation(player, type);
        }
        public void AddMutation(Player player, MutationType mutationType) {
            Mutation mutation = _mutationFactory.Create(mutationType);
            player.AddMutation(mutation);
        }

        private MutationType SelectRandomMutationType() {
            // TODO selecting
            return MutationType.Stamina; 
        }

    }
}