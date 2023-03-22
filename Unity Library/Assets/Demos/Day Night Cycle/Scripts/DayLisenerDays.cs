using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class DayLisenerDays : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text TimerTextDay;

        [SerializeField] private EventChannelTimeSpan OnTimeChange;
        
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnTimeChange.OnEvent += OnTimeChangeOnOnEvent;
            TimerTextDay.text = $"{TimeManager.Instance.CurrentTime.Days}";
        }
        
        private void OnDisable()
        {
            OnTimeChange.OnEvent -= OnTimeChangeOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnTimeChangeOnOnEvent(TimeSpan timeSpan)
        {
            TimerTextDay.text = $"{timeSpan.Days}";
        }
        
        #endregion
    }
}