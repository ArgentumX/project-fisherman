using TMPro;
using UnityEngine;

namespace Presentation.PlayerPresentation.UI.Interaction
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InteractionText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void Show(string message)
        {
            gameObject.SetActive(true);
            text.SetText(message);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void OnValidate()
        {
            if (text == null)
                text = GetComponent<TextMeshProUGUI>();
        }
    }
}