using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// Will always be upto date when a new condition is added/removed
        /// </summary>
        private Dictionary<CustomTagStat, Stat> _stats = new();
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (var v in initializingStats)
            {
                ChangeStat(v.statType, v.statValue);
            }
        }
        
        #endregion

        #region Methods
        
        public void ChangeStat(CustomTagStat statToGet, float value)
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
        
        public float GetStatValue(CustomTagStat statToGet)
        {
            if ( _stats.TryGetValue(statToGet, out Stat value ))
            {
                return value.Value ;
            }
            return 0;
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
        
        #endregion
    }
}