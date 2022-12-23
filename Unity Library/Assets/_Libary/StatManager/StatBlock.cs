using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TigerFrogGames
{
    [Serializable]
    public class statInfo
    {
        public CustomTagStat statType;
        public float statValue;
    }
    
    
    public class StatBlock : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<statInfo> initializingStats;
        private Dictionary<CustomTagStat, float> _baseStats = new();
        
        /// <summary>
        /// Will always be upto date when a new condition is added/removoved
        /// </summary>
        private Dictionary<CustomTagStat, Stat> _stats = new();
        
        #region Status Effects

        private List<StatusEffectDuration> _durational = new();
        
        
        private List<StatusEffectConditionalStat> _conditionalStat = new ();
        private List<StatusEffectConditionalOnMethod> _conditionalOnMethod = new ();

        #endregion

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
        
        public void Start()
        {
            foreach (var v in initializingStats)
            {
                AddStat(v.statType, v.statValue);
                _baseStats.Add(v.statType,v.statValue);
            }
        }

        private void Update()
        {
            for (int i = _durational.Count - 1; i >= 0; i--)
            {
                
                if (_durational[i].RemoveTimeFromDuration(Time.deltaTime)) continue;
                _durational[i].OnEffectOver();
                //_durational.RemoveAt(i);
            }
            
        }
        
        #endregion

        #region Methods

        public void AddStatusEffectInstant(StatusEffectInstant newEffect)
        {
            
        }

        public void AddStatusEffectDuration(StatusEffectDuration newEffect)
        {
            _durational.Add(newEffect);
        }

        public void AddStatusEffectConditional(StatusEffectConditionalStat newEffect)
        {
            _conditionalStat.Add(newEffect);
        }
        
        public float GetStatValue(CustomTagStat statToGet)
        {
            if ( _stats.TryGetValue(statToGet, out Stat value ))
            {
                float temp = 0;
                
                //Adds the stat effects from the conditional and durational effects. 
                foreach (var effect1 in _conditionalStat.FindAll(effect => effect.StatToEffect == statToGet)) temp += effect1.Value;

                
                
                return value.Value + temp;
            }
            else
            {
                #if UNITY_EDITOR
                Debug.Log($"{statToGet.name} not found in the stat block",this);
                #endif
                return 0;
            }
        }
        
        private void AddStat(CustomTagStat statToGet, float value)
        {
            if ( _stats.TryGetValue(statToGet, out Stat stat ))
            {
                stat.ChangeValue(value);
            }
            else
            {
                _stats.Add(statToGet, new Stat(value) );
            }
        }
        
        public Stat GetStat(CustomTagStat statToGet)
        {
            if ( _stats.TryGetValue(statToGet, out Stat value ))
            {
                return value;
            }
            else
            {
                #if UNITY_EDITOR
                Debug.Log($"{statToGet.name} not found in the stat block",this);
                #endif
                return null;
            }
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }
        
        #endregion
    }
}