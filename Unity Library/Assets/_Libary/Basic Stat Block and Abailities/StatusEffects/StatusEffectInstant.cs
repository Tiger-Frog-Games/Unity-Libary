using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectInstant : StatusEffect
    {
        #region Variables

        public CustomTagStat StatToEffect { private set; get; }
        public float Value { private set; get; }
        
        public bool IsPermanentChange { private set; get; }
        
        #endregion
        
        #region Methods
        
        #endregion

        public StatusEffectInstant(CustomTagStat statToEffect, float value, bool isPermanentChange = false) : base()
        {
            StatToEffect = statToEffect;
            Value = value;
            IsPermanentChange = isPermanentChange;
        }
        
        public override StatusEffect Clone()
        {
            return new StatusEffectInstant(StatToEffect, Value, IsPermanentChange);
        }
    }
}