using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditional : StatusEffect
    {
        #region Variables

        private bool _isRemovedOnReset;
        
        public delegate void OnApplyEvent();
        private OnApplyEvent _onApplyEvent;
        
        public delegate void OnRefreshEvent();
        private OnRefreshEvent _onRefreshEvent;
        
        public delegate void OnRemoveEvent();
        private OnRemoveEvent _onRemoveEvent;
        
        #endregion
        
        #region Methods

        
        public StatusEffectConditional(bool isRemovedOnReset = false, OnRefreshEvent onRefreshEvent = null, OnApplyEvent onApplyEvent = null, OnRemoveEvent onRemoveEvent= null)
        {
            _isRemovedOnReset = isRemovedOnReset;
            _onApplyEvent = onApplyEvent;
            _onRefreshEvent = onRefreshEvent;
            _onRemoveEvent = onRemoveEvent;
        }

        public void OnApplyStatusEffect()
        {
            _onApplyEvent?.Invoke();
        }

        public virtual void OnRefreshStatusEffect()
        {
            _onRefreshEvent?.Invoke();
        }
        
        public virtual void OnRemoveStatusEffect()
        {
            _onRemoveEvent?.Invoke();
        }
        
        
        #endregion
        
    }
}