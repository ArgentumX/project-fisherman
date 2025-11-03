using Presentation.PlayerPresentation.UI.Notebook.Inventory;
using Presentation.PlayerPresentation.UI.Notebook.Map;
using Presentation.PlayerPresentation.UI.Notebook.Quests;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Presentation.PlayerPresentation.UI.Notebook
{
    public class NotebookUI : MonoBehaviour
    {
        [SerializeField] private GameObject notebookContent;
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private QuestsUI questsesUI;
        [SerializeField] private MapUI mapUI;
        private GameObject _previousPage;
        
        [SerializeField] private InputActionAsset inputActions;
        private InputAction _notebookAction;
        private InputActionMap _playerActionMap;

        private void Awake()
        {
            // TODO use evolved input controller
            _playerActionMap = inputActions.FindActionMap("Player");
            var playerUIActionMap = inputActions.FindActionMap("PlayerUI");
            _notebookAction = playerUIActionMap.FindAction("Notebook");
        }

        private void OnEnable()
        {
            _notebookAction.Enable();
            _notebookAction.performed += OnNotebookButtonClicked;
        }
        private void OnDisable()
        {
            _notebookAction.performed -= OnNotebookButtonClicked;
            _notebookAction.Disable();
        }
        private void OnNotebookButtonClicked(InputAction.CallbackContext ctx)
        {
            var active = !notebookContent.activeSelf;
            if (active) {
                EnableNotebook();
            }
            else {
                DisableNotebook();
            }
            
        }

        private void EnableNotebook()
        {
            notebookContent.SetActive(true);
            _playerActionMap.Disable();
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void DisableNotebook()
        {
            notebookContent.SetActive(false);
            _playerActionMap.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void OnInventoryButtonClicked()
        {
            SwapTo(inventoryUI.gameObject);
        }

        public void OnQuestsButtonClicked()
        {
            SwapTo(questsesUI.gameObject);
        }

        public void OnMapButtonClicked()
        {
            SwapTo(mapUI.gameObject);
        }

        private void SwapTo(GameObject target)
        {
            _previousPage?.SetActive(false);
            target.SetActive(true);
            _previousPage = target; 
        }

        private void OnValidate()
        {
            inventoryUI ??= GetComponentInChildren<InventoryUI>(true);
            questsesUI ??= GetComponentInChildren<QuestsUI>(true);
            mapUI ??= GetComponentInChildren<MapUI>(true);
        }
    }
}