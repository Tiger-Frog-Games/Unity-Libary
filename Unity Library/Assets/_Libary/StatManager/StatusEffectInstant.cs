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
        
        #endregion
        
        #region Methods

        #endregion

        public StatusEffectInstant(CustomTagStat statToEffect, float value) : base()
        {
            StatToEffect = statToEffect;
            Value = value;
            
            #if UNITY_EDITOR

            if (statToEffect.CanBeUsedAsInstantStatusEffect == false)
            {
                Debug.Log("Waring trying to use a stat for an instant effect that can not be used");
            }
            
            #endif
            
        }
    }
}