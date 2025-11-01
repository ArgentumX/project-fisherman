using System;
using UnityEngine;

namespace Infrastructure.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public float DayLengthInSec => dayLengthInSec;
        public float TimeStartInSec => timeStartInSec;
        public int DaysPerCycle => daysPerCycle;
        public float PassOutRestorePercent => passOutRestorePercent;

        [Header("Player")]
        [SerializeField, Range(0f, 1f)] private float passOutRestorePercent = 0.5f;
        
        [Header("Time")]
        [SerializeField, Min(1f)] private float dayLengthInSec = 60;
        [SerializeField, Min(0f)] private float timeStartInSec = 30;
        [SerializeField, Min(1f)] private int daysPerCycle = 1;

        private void OnValidate() {
            if (timeStartInSec > dayLengthInSec)
                timeStartInSec = dayLengthInSec;
        }
    }
}