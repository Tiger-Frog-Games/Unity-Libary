using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public enum StatusEffectDurationConflict
    {
        ResetDuration = 100,
        AddingTimeToEnd = 200,
    }
    
    public class StatusEffectDuration : StatusEffectConditional
    {
        #region Variables

        private float _durationLeft;
        
        private float _durationResetValue;
        
        private float _timeToAdd;

        public delegate void OnProcEvent();

        private OnProcEvent _procMethod;

        private float _procCooldownTime;
        private float _procCurrentCoolDown;
        
        
        
        #endregion
        
        #region Methods

        #endregion

        public StatusEffectDuration(CustomTagStat statToEffect, float value, float duration, OnProcEvent onProcEvent ,float procCooldown = 0, float timeToAdd = 0) : base(statToEffect, value)
        {
            _durationLeft = duration;
            _durationResetValue = duration;

            _timeToAdd = timeToAdd;
            
            _procMethod = onProcEvent;

            _procCooldownTime = procCooldown;
            _procCurrentCoolDown = 0;
        }
        
        public bool RemoveTimeFromDuration( float time )
        {
            _durationLeft -= time;

            if (_procMethod != null)
            {
                _procCurrentCoolDown += time;
                if (_procCurrentCoolDown >= _procCooldownTime)
                {
                    _procMethod();
                    _procCurrentCoolDown -= _procCooldownTime;
                }
            }
            
            
            return (_durationLeft > 0);
        }
        
    }
}