using Presentation.PlayerPresentation.UI.Notebook.Inventory;
using Presentation.PlayerPresentation.UI.Notebook.Map;
using Presentation.PlayerPresentation.UI.Notebook.Quests;
using UnityEngine;

namespace Presentation.PlayerPresentation.UI.Notebook
{
    public class NotebookUI : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private QuestsUI questsUI;
        [SerializeField] private MapUI mapUI;
    }
}