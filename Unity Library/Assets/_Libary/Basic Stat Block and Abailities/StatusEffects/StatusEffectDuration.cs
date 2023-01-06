using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    public enum StatusEffectDurationConflict
    {
        Nothing = 100,
        AddingTimeToEnd = 200,
        Refresh = 300,
        AddStack = 400,
        AddStackRefresh = 500,
        AddUniqueStatusEffect = 600
    }
    
    public class StatusEffectDuration : StatusEffectConditional
    {
        #region Variables

        private float _durationMax;
        private float _durationLeft;
        
        private float _durationToAdd;
        
        private int _stackMax;
        private int _stackCurrent;
        
        
        public delegate void OnProcEvent();
        private OnProcEvent _procMethod;

        public delegate void OnStackEvent(int currentStack, int maxStacks);
        private OnStackEvent _stackMethod;
        
        private float _procCooldownTime;
        private float _procCurrentCoolDown;

        public StatusEffectDurationConflict StatusApplyConflict { private set; get; }

        #endregion
        
        #region Methods

        #endregion

        public StatusEffectDuration(StatusEffectDurationConflict conflictResolutionType, float duration, int startingStacks = 0, int stackMax = 0,  bool isRemovedOnReset = false, OnApplyEvent onApplyEvent = null,
            OnRefreshEvent onRefreshEvent = null, OnStackEvent onStackEvent = null, OnProcEvent onProcEvent = null, OnRemoveEvent onRemoveEvent = null,
            float procCooldown = 0) 
            : base(isRemovedOnReset, onRefreshEvent, onApplyEvent, onRemoveEvent)
        {
            StatusApplyConflict = conflictResolutionType;
            
            _durationLeft = duration;

            _procMethod = onProcEvent;

            _procCooldownTime = procCooldown;
            _procCurrentCoolDown = 0;

            if (_procMethod != null && _procCooldownTime == 0)
            {
                Debug.Log("Warning there is no cooldown for the the ProcEvent");
            }
            
            _stackCurrent = startingStacks;
            _stackMax = stackMax;
            _stackMethod = onStackEvent;
            
            if (StatusApplyConflict is StatusEffectDurationConflict.AddStack or StatusEffectDurationConflict.AddStackRefresh && _stackMax <= 0 )
            {
                Debug.Log("Warning Stack max cant be 0 or negative for this status effect");
            }
            
        }
        
        public bool RemoveTimeFromDuration( float time )
        {
            _durationLeft -= time;
            
            if (_procMethod != null)
            {
                _procCurrentCoolDown += time;
                if (_procCurrentCoolDown >= _procCooldownTime)
                {
                    _procMethod?.Invoke();
                    _procCurrentCoolDown -= _procCooldownTime;
                }
            }
            
            
            return (_durationLeft > 0);
        }

        public void Refresh()
        {
            switch (StatusApplyConflict)
            {
                case StatusEffectDurationConflict.Nothing:
                    return;
                case StatusEffectDurationConflict.AddingTimeToEnd:
                    _durationLeft += _durationToAdd;
                    break;
                case StatusEffectDurationConflict.Refresh:
                    _durationLeft = _durationMax;
                    break;
                case StatusEffectDurationConflict.AddStack:
                    addStack();
                    _durationLeft += _durationToAdd;
                    break;
                case StatusEffectDurationConflict.AddStackRefresh:
                    addStack();
                    _durationLeft = _durationMax;
                    break;
                case StatusEffectDurationConflict.AddUniqueStatusEffect:
                    //this is handled in statuseffect manager;
                    break;
                default:
                    return;
            }
        }

        private void addStack()
        {
            _stackCurrent = Mathf.Min(_stackCurrent+1 ,_stackMax);
            _stackMethod?.Invoke(_stackCurrent, _stackMax);
        }
        
    }
}