using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditional : StatusEffect
    {
        #region Variables

        protected bool _isRemovedOnReset;
        
        public delegate void OnApplyEvent();
        protected OnApplyEvent _onApplyEvent;
        
        public delegate void OnProcEvent();
        protected OnProcEvent _onProcEvent;
        
        protected float _procCooldownTime;
        private float _procCurrentCoolDown;
        
        public delegate void OnRemoveEvent();
        protected OnRemoveEvent _onRemoveEvent;
        
        #endregion
        
        #region Methods

        
        public StatusEffectConditional(bool isRemovedOnReset = false, OnApplyEvent onApplyEvent = null, OnProcEvent onOnProcEvent = null, float procCooldown = 0, OnRemoveEvent onRemoveEvent= null) :base()
        {
            _isRemovedOnReset = isRemovedOnReset;
            
            _onApplyEvent = onApplyEvent;
            _onRemoveEvent = onRemoveEvent;
            
            _onProcEvent = onOnProcEvent;

            _procCooldownTime = procCooldown;
            _procCurrentCoolDown = 0;

            if (_onProcEvent != null && _procCooldownTime == 0)
            {
                Debug.Log("Warning there is no cooldown for the the ProcEvent");
            }
        }

        public void OnApplyStatusEffect()
        {
            _onApplyEvent?.Invoke();
        }
        
        public void checkForProcEvent(float time)
        {
            if (_onProcEvent != null)
            {
                _procCurrentCoolDown += time;
                if (_procCurrentCoolDown >= _procCooldownTime)
                {
                    _onProcEvent?.Invoke();
                    _procCurrentCoolDown -= _procCooldownTime;
                }
            }
        }
        
        public void OnRemoveStatusEffect()
        {
            _onRemoveEvent?.Invoke();
        }

        public override StatusEffect Clone()
        {
            return new StatusEffectConditional(_isRemovedOnReset, _onApplyEvent, _onProcEvent, _procCooldownTime, _onRemoveEvent );
        }

        #endregion
        
    }
}