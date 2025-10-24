using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.PlayerPresentation.UI.Interaction
{
    public class InteractionDisplay : MonoBehaviour
    {
        [SerializeField] private Image holdProgressUI; // Image в режиме Filled Radial
        [SerializeField] private TextMeshProUGUI text;
        
        public void ShowInteractionText(string message) {
            text.gameObject.SetActive(true);
            text.SetText(message);
        }
        
        public void DisableInteractionText()
        {
            text.gameObject.SetActive(false);
        }
        public void EnableHoldBar() {
            holdProgressUI.gameObject.SetActive(true);
            ShowInteractionText("[Hold progress]");
        }
        public void ShowHoldProgress(float progress)
        {
            holdProgressUI.fillAmount = progress;
        }
        public void DisableHoldBar() {
            holdProgressUI.gameObject.SetActive(false);
        }
        private void OnValidate()
        {
            text ??= GetComponentInChildren<TextMeshProUGUI>(true);
            if (text == null)
                throw new Exception("Cant find text component");
            holdProgressUI ??= GetComponentInChildren<Image>(true);
            if (holdProgressUI == null)
                throw new Exception("Cant find image component");
        }
    }
}