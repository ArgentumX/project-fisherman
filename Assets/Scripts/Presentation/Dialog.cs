using System;
using Presentation.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using Zenject;

namespace Presentation
{
    public class Dialog : OutlineInteractable
    {
        [SerializeField] private string dialogueNode;
        private DialogueRunner _dialogueRunner;

        [Inject]
        private void Construct(DialogueRunner dialogueRunner) {
            _dialogueRunner = dialogueRunner;
        }
        public override void Interact<T>(IInteractor<T> interactor) {
            _dialogueRunner.StartDialogue(dialogueNode);
        }

        public override bool CanInteract<T>(IInteractor<T> interactor)
        {
            return !_dialogueRunner.IsDialogueRunning;
        }

        private void Reset(){
            base.ResetBase();
        }
    }
}