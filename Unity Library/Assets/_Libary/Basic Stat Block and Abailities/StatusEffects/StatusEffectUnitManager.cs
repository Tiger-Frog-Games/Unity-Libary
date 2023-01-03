using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectUnitManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StatBlock _statBlock;
        
        private List<StatusEffectDuration> _durational = new();
        
        private List<StatusEffectConditionalStat> _conditionalStat = new ();
        private List<StatusEffectConditionalOnMethod> _conditionalOnMethod = new ();
        
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
                _durational[i].OnEffectOver();
                _durational.RemoveAt(i);
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
            _durational.Add(newEffect);
        }

        public void AddStatusEffectConditional(StatusEffectConditionalStat newEffect)
        {
            _conditionalStat.Add(newEffect);
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }
        
        
        public float GetStatChangesFromConditionalChanges(CustomTagStat statToGet)
        {
           
                float temp = 0;
            
                //Adds the stat effects from the conditional and durational effects. 
                foreach (var effect1 in _conditionalStat.FindAll(effect => effect.StatToEffect == statToGet)) temp += effect1.Value;
                
                return temp;

        }
        
        #endregion
    }
}