using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectHandler : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StatBlock _statBlock;
        
        
        private List<StatusEffectConditional> _conditional = new();
        private List<StatusEffectDuration> _durational = new();
        
        private List<StatusEffectInstant> _temporaryStatChanges = new ();
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
            
        }
        
        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }
        
        protected void Update()
        {
            OnTick(Time.deltaTime);
        }
        
        #endregion

        #region Methods

        protected void OnTick(float timeChange)
        {
            for (int i = -_conditional.Count - 1; i >= 0; i--)
            {
                _conditional[i].checkForProcEvent(timeChange);
            }
            
            for (int i = _durational.Count - 1; i >= 0; i--)
            {
                _durational[i].checkForProcEvent(timeChange);
                
                if (_durational[i].RemoveTimeFromDuration(Time.deltaTime )) continue;
                RemoveStatusEffectDurational(_durational[i]);
            }
        }

        public virtual void AddStatusEffectInstant(StatusEffectInstant newEffect)
        {
            if (newEffect.IsPermanentChange == false)
            {
                _temporaryStatChanges.Add(newEffect);
            }
            
            _statBlock.ChangeStat(newEffect.StatToEffect,newEffect.Value);
        }

        public void AddStatusEffectConditional( StatusEffectConditional newEffect)
        {
            _conditional.Add(newEffect);
            newEffect.OnApplyStatusEffect();
        }

        public void RemoveStatusEffectConditional( StatusEffectConditional effectToRemove)
        {
            _conditional.RemoveAll(r =>
            {
                if (r.ID == effectToRemove.ID)
                {
                    effectToRemove.OnRemoveStatusEffect();
                    effectToRemove.Reset();
                    return true;
                }
                return false;
            });
        }
        
        public void AddStatusEffectDurational(StatusEffectDuration newEffect)
        {
            if (!_durational.Contains(newEffect) || newEffect.ConflictResolutionType == StatusEffectDurationConflict.AddIndependent)
            {
                if (newEffect.ConflictResolutionType == StatusEffectDurationConflict.AddIndependent)
                {
                    newEffect = (StatusEffectDuration)newEffect.Clone();
                }
                
                newEffect.OnApplyStatusEffect();
                _durational.Add(newEffect);
            }
            else
            {
                newEffect.Refresh();   
            }
        }
        
        private void RemoveStatusEffectDurational(StatusEffectDuration effectToRemove)
        {
            _durational.RemoveAll(r =>
            {
                if (r.ID == effectToRemove.ID)
                {
                    effectToRemove.OnRemoveStatusEffect();
                    effectToRemove.Reset();
                    return true;
                }
                return false;
            });
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }
        
        public float GetTemporaryStatChanges(CustomTagStat statToGet)
        {
            return _temporaryStatChanges.FindAll(effect => effect.StatToEffect == statToGet).Sum(effect1 => effect1.Value);
        }

        public bool DoesConditionalByID( Guid IDtoCheck )
        {
            return _conditional.Exists(e => e.ID == IDtoCheck);
        }
        
        #endregion
    }
}