using System;
using Presentation.PlayerPresentation.Controllers;
using Yarn.Unity;
using Zenject;

namespace Presentation.Services
{
    public class DialogBlocker : IInitializable, IDisposable
    {
        private readonly DialogueRunner _dialogueRunner;
        private readonly PlayerInputController _controller;

        public DialogBlocker(
            DialogueRunner dialogueRunner,
            PlayerInputController controller
            ) {
            // TODO use evolved input controller
            _dialogueRunner = dialogueRunner;
            _controller = controller;
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
            _controller.MovementController.Disable();
            _controller.InteractionController.Disable();
        }
        private void UnblockMovements() {
            _controller.MovementController.Enable();
            _controller.InteractionController.Enable();
        }

    }
}