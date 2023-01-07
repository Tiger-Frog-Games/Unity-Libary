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
        
        public delegate void OnRemoveEvent();
        protected OnRemoveEvent _onRemoveEvent;
        
        #endregion
        
        #region Methods

        
        public StatusEffectConditional(bool isRemovedOnReset = false, OnApplyEvent onApplyEvent = null, OnRemoveEvent onRemoveEvent= null)
        {
            _isRemovedOnReset = isRemovedOnReset;
            
            _onApplyEvent = onApplyEvent;
            _onRemoveEvent = onRemoveEvent;
        }

        public void OnApplyStatusEffect()
        {
            _onApplyEvent?.Invoke();
        }
        
        public void OnRemoveStatusEffect()
        {
            _onRemoveEvent?.Invoke();
        }

        public override StatusEffect Clone()
        {
            return new StatusEffectConditional(_isRemovedOnReset, _onApplyEvent, _onRemoveEvent );
        }

        #endregion
        
    }
}