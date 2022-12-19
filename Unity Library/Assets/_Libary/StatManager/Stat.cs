using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    [Serializable] 
    public class Stat 
    {
        private float _min, _max,_base, _current,
                    _minModifer,_maxModifer,_currentModifer;
        
        
        public event Action<float,float> OnStatChange;
        
        public Stat(float min, float max,float current)
        {
            _min = min;
            _max = max;
            _current = current;
        }

        public void changeCurrentValue(float valueIn)
        {
            _current = Mathf.Clamp(_current + valueIn, _min, _max);
            OnStatChange?.Invoke(_current, _max);
        }

        
        /// <summary>
        /// Changes the max stat value.
        /// If the value change would maker it 
        /// </summary>
        /// <param name="valueIn"></param>
        /// <param name="updateHp"></param>
        public void changeMaxValue(float valueIn, bool updateCurrent = true)
        {
            float temp = _max;
            _max = Mathf.Max(_min, _max + valueIn);
            
        }

        public float getCurrent()
        {
            return _current + _currentModifer;
        }
        
    }
}