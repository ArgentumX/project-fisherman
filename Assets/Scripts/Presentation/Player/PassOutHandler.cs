using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Player.Events;
using Infrastructure.Extensions;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

namespace Presentation.PlayerPresentation
{
    [RequireComponent(typeof(CharacterController))]
    public class PassOutHandler : MonoBehaviour
    {

        [SerializeField] private CharacterController _characterController;
        private Player _player;
        private IPlayerSleepUsecase _playerSleepUsecase;
        [Inject]
        private void Construct(IPlayerRepository playerRepository, IPlayerSleepUsecase  playerSleepUsecase)
        {
            _player = playerRepository.Get();
            _playerSleepUsecase = playerSleepUsecase;
        }

        private void OnEnable()
        {
            _player.OnPassOut += HandleStartPassOut;
            _player.OnPlayerSetPosition += HandeSetPosition;
        }

        private void OnDisable()
        {
            _player.OnPassOut -= HandleStartPassOut;
            _player.OnPlayerSetPosition -= HandeSetPosition;
        }

        private void HandleStartPassOut(PlayerPassOutEvent e)
        {
            if (!e.IsCompleted)
            {
                // TODO animation
                _playerSleepUsecase.EndPassOut(_player);
            }
        }

        private void HandeSetPosition(PlayerSetPositionEvent e)
        {
           _characterController.Teleport(e.PlayerDto.Position.ToUnityVector3());
        }

        private void OnValidate()
        {
            if (!_characterController)
                _characterController = GetComponent<CharacterController>();
        }
    }
}