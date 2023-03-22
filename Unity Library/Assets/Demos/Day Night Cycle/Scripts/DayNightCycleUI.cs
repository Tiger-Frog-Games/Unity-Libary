using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class DayNightCycleUI : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private TMP_InputField changeTimeField;

        [SerializeField] private Button ResetTimer;
        
        #endregion

        #region Unity Methods

        private void Start()
        {
            changeTimeField.onValueChanged.AddListener(changeTimeScale);
            changeTimeField.text = ""+TimeManager.Instance.dayLength;
            
            
            ResetTimer.onClick.AddListener(resetTimer);
        }

        private void OnDestroy()
        {
            changeTimeField.onValueChanged.RemoveListener(changeTimeScale);
            ResetTimer.onClick.RemoveListener(resetTimer);
        }

        #endregion

        #region Methods

        private void resetTimer()
        {
            TimeManager.Instance.ResetTimer();
        }
        
        private void changeTimeScale(string newTime)
        { 
            if(String.Equals("", newTime)) return;

            //new time is a float by setting the input field content type
            TimeManager.Instance.changeTimeScale(float.Parse(newTime));
        }
        
        #endregion
    }
}