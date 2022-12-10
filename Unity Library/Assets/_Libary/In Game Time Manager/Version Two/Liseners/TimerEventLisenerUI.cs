using System;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class TimerEventLisenerUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text TimerText;

        [SerializeField] private EventChannelTimeSpan OnTimeChange;
        
        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (OnTimeChange != null)
            {
                OnTimeChange.OnEvent += timeChange;
            }

            
        }

        private void OnDisable()
        {
            if (OnTimeChange != null)
            {
                OnTimeChange.OnEvent -= timeChange;
            }

        }

        #endregion

        #region Methods

        private void timeChange(TimeSpan t)
        {
            TimerText.text = $"{t.Days} - {t.Hours}:{t.Minutes}.{t.Seconds}";
        }


        private void RefreshText()
        {
            
        }

        #endregion
    }
}