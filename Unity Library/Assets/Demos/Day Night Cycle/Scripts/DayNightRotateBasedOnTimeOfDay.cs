using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    public class DayNightRotateBasedOnTimeOfDay : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelTimeSpan OnTimeChange;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnTimeChange.OnEvent += OnTimeChangeOnOnEvent;
        }
        
        private void OnDestroy()
        {
            OnTimeChange.OnEvent -= OnTimeChangeOnOnEvent;
        }
        
        #endregion

        #region Methods
        
        private void OnTimeChangeOnOnEvent(TimeSpan timeSpan)
        {
            float percentage = (float) (timeSpan.Hours *60 + timeSpan.Minutes ) /1440;

            float xRot = Mathf.Lerp(360f,0f,percentage);
            
            //if time speed is slow you might want to set a target vector 3 here and lerp in an update method.
            transform.eulerAngles = new Vector3(xRot, -90, 0f);
        }

        
        #endregion
    }
}