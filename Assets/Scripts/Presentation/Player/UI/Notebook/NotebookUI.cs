using Presentation.Player.UI.Notebook.Inventory;
using Presentation.Player.UI.Notebook.Map;
using Presentation.Player.UI.Notebook.Quests;
using UnityEngine;

namespace Presentation.Player.UI.Notebook
{
    public class NotebookUI : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private QuestsUI questsUI;
        [SerializeField] private MapUI mapUI;
    }
}