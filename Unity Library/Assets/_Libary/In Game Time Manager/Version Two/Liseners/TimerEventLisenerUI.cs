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

            TimerText.text = string.Format("{0} - {1}:{2:D2}", t.Days, t.Hours, t.Minutes);
            
            
            var hours = t.Hours;
            var minutes = t.Minutes;
            var amPmDesignator = "AM";
            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = "PM";
            else if (hours > 12) {
                hours -= 12;
                amPmDesignator = "PM";
            }
            TimerText.text = String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
            

        }


        private void RefreshText()
        {
            
        }

        #endregion
    }
}