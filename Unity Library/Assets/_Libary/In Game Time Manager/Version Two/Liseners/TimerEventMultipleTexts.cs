using System;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class TimerEventMultipleTexts : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text TimerTextHour, TimerTextMinutes, TimerTextAmPm;

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
            
            //TimerText.text = String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
            
            TimerTextHour.text = $"{hours}";
            TimerTextMinutes.text = String.Format("{0:00}",minutes);
            TimerTextAmPm.text = $"{amPmDesignator}";


        }


        private void RefreshText()
        {
            
        }

        #endregion
    }
}