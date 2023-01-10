using UnityEngine;

namespace TigerFrogGames
{
    public enum StatusEffectDurationConflict
    {
        CantAddMoreThenOne = 100,
        AddTime = 200,
        Refresh = 300,
        AddStack = 400,
        AddStackRefresh = 500,
        AddStackAddTime = 600,
        AddIndependent = 700
    }
    
    public class StatusEffectDuration : StatusEffectConditional
    {
        #region Variables

        private float _durationMax;
        private float _durationLeft;
        
        private float _durationToAdd;
        
        public delegate void OnRefreshEvent(float durationLeft);
        private OnRefreshEvent _onRefreshEvent;
        
        private int _stackMax;
        private int _stacksToAdd;
        private int _stackCurrent;
        private int _stackStarting;

        private bool _loseOneStackOnOver;
        
        public delegate void OnStackEvent(int currentStack, int maxStacks);
        private OnStackEvent _onStackEvent;
        
        public StatusEffectDurationConflict ConflictResolutionType { private set; get; }

        #endregion
        
        #region Methods

        #endregion

        public StatusEffectDuration(StatusEffectDurationConflict conflictResolutionTypeResolutionType, float duration, float durationToAdd = 0, int startingStacks = 0,int stacksToAdd = 1, int stackMax = 0, bool loseOneStackOnOver = false,   bool isRemovedOnReset = false, OnApplyEvent onApplyEvent = null,
            OnRefreshEvent onRefreshEvent = null, OnStackEvent onChangeStackEvent = null, OnProcEvent onProcEvent = null, OnRemoveEvent onRemoveEvent = null,
            float procCooldown = 0) 
            : base(isRemovedOnReset, onApplyEvent,onProcEvent, procCooldown, onRemoveEvent)
        {
            ConflictResolutionType = conflictResolutionTypeResolutionType;
            
            _durationLeft = duration;
            _durationMax = duration;
            _durationToAdd = durationToAdd;

            if (( ConflictResolutionType == StatusEffectDurationConflict.AddTime || conflictResolutionTypeResolutionType == StatusEffectDurationConflict.AddStackAddTime) && durationToAdd == 0 )
            {
                Debug.Log("Warning there is no time added to the stack duration");
            }

            _onRefreshEvent = onRefreshEvent;
            
            _stackCurrent = startingStacks;
            _stacksToAdd = stacksToAdd;
            _stackMax = stackMax;
            _onStackEvent = onChangeStackEvent;

            if ((_stackCurrent == 0 || _stackMax == 0) &&
                ConflictResolutionType is StatusEffectDurationConflict.AddStack
                    or StatusEffectDurationConflict.AddStackAddTime or StatusEffectDurationConflict.AddStackRefresh)
            {
                Debug.Log("Warning starting stacks or max stacks is not set");
            }
            
            _loseOneStackOnOver = loseOneStackOnOver;
            
            if (ConflictResolutionType is StatusEffectDurationConflict.AddStack or StatusEffectDurationConflict.AddStackRefresh && _stackMax <= 0 )
            {
                Debug.Log("Warning Stack max cant be 0 or negative for this status effect");
            }
            
        }
        
        public bool RemoveTimeFromDuration( float time )
        {
            _durationLeft -= time;
            
            if (_durationLeft < 0 && _loseOneStackOnOver)
            {
                changeStack(-1);
                if(_stackCurrent != 0) _durationLeft = _durationMax;
            }
            
            return (_durationLeft > 0);
        }

        
        
        public void Refresh()
        {
            //Handles CantAddMoreThenOne and AddUniqueStatusEffect inside the StatusEffect Handler
            
            switch (ConflictResolutionType)
            {
                case StatusEffectDurationConflict.AddTime:
                    _durationLeft = Mathf.Min(_durationMax ,_durationLeft + _durationToAdd);
                    break;
                case StatusEffectDurationConflict.Refresh:
                    _durationLeft = _durationMax;
                    break;
                case StatusEffectDurationConflict.AddStack:
                    changeStack(_stacksToAdd);
                    break;
                case StatusEffectDurationConflict.AddStackRefresh:
                    _durationLeft = _durationMax;
                    break;
                case StatusEffectDurationConflict.AddStackAddTime:
                    changeStack(_stacksToAdd);
                    _durationLeft = Mathf.Min(_durationMax ,_durationLeft + _durationToAdd);
                    break;
                default:
                    return;
            }
            _onRefreshEvent?.Invoke(_durationLeft);
        }

        public override void Reset()
        {
            base.Reset();
            _stackCurrent = _stackStarting;
            _durationLeft = _durationMax;
        }

        public override StatusEffect Clone()
        {
            return new StatusEffectDuration(ConflictResolutionType, _durationMax, _durationToAdd,  _stackStarting,_stacksToAdd, _stackMax, _loseOneStackOnOver,   _isRemovedOnReset, _onApplyEvent,
                _onRefreshEvent, _onStackEvent, _onProcEvent, _onRemoveEvent , _procCooldownTime );

        }

        public void changeStack(int stackChangeNumber)
        {
            _stackCurrent = Mathf.Min(_stackCurrent+stackChangeNumber ,_stackMax);
            
            if(_stackCurrent != 0) _onStackEvent?.Invoke(_stackCurrent, _stackMax);
        }
        
    }
}