using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField] public float Value { get; private set; }

        //[SerializeField] private bool _useStatAsMax = false;
        //[SerializeField] private Stat _maxStat;

        
        
        public event Action<float> OnStatChange;


        public Stat(float value)
        {
            Value = value;
        }

            /// <summary>
        /// Changes the value of the stat value.
        /// If the value change would maker it 
        /// </summary>
        /// <param name="valueIn">Value to</param>
        public void ChangeValue(float valueIn)
        {
            //Value = _useStatAsMax ? Mathf.Clamp(valueIn, 0, _maxStat.Value) : valueIn;
            Value = valueIn;

            OnStatChange?.Invoke(Value);
        }
        
            /*
        public void SetStatAsMax(Stat statIn)
        {
            _useStatAsMax = true;
            _maxStat = statIn;
        }

        public void RemoveStatAsMax()
        {
            _useStatAsMax = false;
        }
        */
        
    }
}