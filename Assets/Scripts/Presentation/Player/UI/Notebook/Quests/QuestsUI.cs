using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Quest;
using Domain.Models.Entities.Quest.Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presentation.PlayerPresentation.UI.Notebook.Quests
{
    public class QuestsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textDisplay;

        private List<Quest> _trackedQuests; // Только отслеживаемые (корневые) квесты
        private IQuestRepository _questRepository;

        [Inject]
        private void Construct(IQuestRepository questRepository)
        {
            _questRepository = questRepository;
        }

        private void Start()
        {
            // Получаем только отслеживаемые квесты (например, активные или назначенные игроку)
            // Предполагается, что репозиторий предоставляет такой метод.
            // Если нет — можно фильтровать по статусу или флагу IsTracked.
            _trackedQuests = new List<Quest>(_questRepository.GetAll());

            foreach (var quest in _trackedQuests)
            {
                SubscribeToQuest(quest);
            }

            UpdateDisplay();
        }

        private void OnDestroy()
        {
            foreach (var quest in _trackedQuests)
            {
                UnsubscribeFromQuest(quest);
            }
        }

        private void SubscribeToQuest(Quest quest)
        {
            quest.OnQuestUpdated += HandleQuestUpdated;
        }

        private void UnsubscribeFromQuest(Quest quest)
        {
            quest.OnQuestUpdated -= HandleQuestUpdated;
        }

        private void HandleQuestUpdated(QuestUpdatedEvent e)
        {
            // Обновляем всё отображение при любом изменении
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var sb = new StringBuilder();
            foreach (var trackedQuest in _trackedQuests)
            {
                BuildQuestDisplay(sb, trackedQuest, 0);
            }
            textDisplay.text = sb.ToString();
        }

        private void BuildQuestDisplay(StringBuilder sb, Quest quest, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            string statusEmoji = GetStatusEmoji(quest.Status);
            string progressText = quest.Progress.ToString("P0");

            sb.AppendLine($"{indent}{statusEmoji} {quest.Title} (Progress: {progressText}, Status: {quest.Status})");

            if (quest is CompositeQuest composite)
            {
                foreach (var subQuest in composite.SubQuests)
                {
                    BuildQuestDisplay(sb, subQuest, indentLevel + 1);
                }
            }
        }

        private string GetStatusEmoji(QuestStatus status)
        {
            return status switch
            {
                QuestStatus.NotStarted => "❌",
                QuestStatus.Active => "▶️",
                QuestStatus.Completed => "✅",
                QuestStatus.Failed => "❗",
                _ => "❓"
            };
        }

        private void OnValidate()
        {
            textDisplay ??= GetComponentInChildren<TextMeshProUGUI>();
            if (textDisplay == null)
                throw new Exception("Cant find TextMeshProUGUI in children");
        }    
    }
}