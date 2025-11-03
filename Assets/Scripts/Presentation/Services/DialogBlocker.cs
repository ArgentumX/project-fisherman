using System;
using Presentation.PlayerPresentation.Controllers;
using Yarn.Unity;
using Zenject;

namespace Presentation.Services
{
    public class DialogBlocker : IInitializable, IDisposable
    {
        private readonly DialogueRunner _dialogueRunner;
        private readonly MovementController _movementController;
        private readonly InteractionController _interactionController;

        public DialogBlocker(
            DialogueRunner dialogueRunner,
            MovementController movementController,
            InteractionController interactionController
            ) {
            // TODO use evolved input controller
            _dialogueRunner = dialogueRunner;
            _movementController = movementController;
            _interactionController = interactionController;
        }

        public void Initialize()
        {
            _dialogueRunner.onDialogueStart?.AddListener(BlockMovements);
            _dialogueRunner.onDialogueComplete?.AddListener(UnblockMovements);
        }

        public void Dispose()
        {
            _dialogueRunner.onDialogueStart?.RemoveListener(BlockMovements);
            _dialogueRunner.onDialogueComplete?.RemoveListener(UnblockMovements);
        }

        private void BlockMovements() {
            _movementController.Block();
            _interactionController.Block();
        }

        private void UnblockMovements() {
            _movementController.Unblock();
            _interactionController.Unblock();
        }
    }
}