using System;
using Domain.Models.Entities.Player;
using Presentation.Common;
using TMPro;
using UnityEngine;
using System.Linq;

public class Mirror : OutlineInteractable
{
    // TODO create separate mutation display with activate/disable logic
    [SerializeField] private TextMeshProUGUI mutationText;

    public override void Interact<T>(IInteractor<T> interactor)
    {
        switch (interactor.GetModel())
        {
            case Player player:
                var mutations = player.GetMutations();
                if (mutations == null || mutations.Count == 0) {
                    mutationText.text = "<i>Мутаций нет</i>";
                    return;
                }

                var descriptions = mutations
                    .Select(m => $"• {m.GetDescription()}")
                    .ToArray();

                mutationText.text = string.Join("\n", descriptions);
                mutationText.gameObject.SetActive(true);
                break;

            default:
                throw new System.NotImplementedException();
        }
    }

    public override void OnHoverExit<T>(IInteractor<T> interactor)
    {
        base.OnHoverExit(interactor);
        mutationText.gameObject.SetActive(false);
    }
    public override bool CanInteract<T>(IInteractor<T> interactor)
    {
        return true;
    }

    private void Reset() {
        base.ResetBase();
    }
}