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
        
        private void Update()
        {
            for (int i = _durational.Count - 1; i >= 0; i--)
            {
                
                if (_durational[i].RemoveTimeFromDuration(Time.deltaTime )) continue;
   
                RemoveStatusEffectDuration(_durational[i]);
            }
            
        }
        
        #endregion

        #region Methods

        public void AddStatusEffectInstant(StatusEffectInstant newEffect)
        {
            _statBlock.AddStat(newEffect.StatToEffect,newEffect.Value);
        }
        
        public void AddStatusEffectDuration(StatusEffectDuration newEffect)
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
        
        private void RemoveStatusEffectDuration(StatusEffectDuration effectToRemove)
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