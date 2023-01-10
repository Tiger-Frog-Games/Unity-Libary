using System.Collections;
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
        
        private List<StatusEffectInstant> _conditionalStatChanges = new ();
        
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
            print(_conditional.Count);
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
        
        public void AddStatusEffectInstant(StatusEffectInstant newEffect)
        {
            _statBlock.AddStat(newEffect.StatToEffect,newEffect.Value);
        }

        public void AddStatusEffectConditional( StatusEffectConditional newEffect)
        {
            print(newEffect.ID);
            _conditional.Add(newEffect);
        }

        public void RemoveStatusEffectConditional( StatusEffectConditional effectToRemove)
        {
            print(effectToRemove.ID);
            _conditional.RemoveAll(r => r.ID == effectToRemove.ID);
        }
        
        public void AddStatusEffectDurational(StatusEffectDuration newEffect)
        {
            if (!_durational.Contains(newEffect) || newEffect.ConflictResolutionType == StatusEffectDurationConflict.AddUniqueStatusEffect)
            {
                if (newEffect.ConflictResolutionType == StatusEffectDurationConflict.AddUniqueStatusEffect)
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
            effectToRemove.OnRemoveStatusEffect();
            _durational.Remove(effectToRemove);
            effectToRemove.Reset();
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }
        
        public float GetStatChangesFromConditionalChanges(CustomTagStat statToGet)
        {
            //Adds the stat effects from the conditional and durational effects. 

            return _conditionalStatChanges.FindAll(effect => effect.StatToEffect == statToGet).Sum(effect1 => effect1.Value);
        }
        
        #endregion
    }
}