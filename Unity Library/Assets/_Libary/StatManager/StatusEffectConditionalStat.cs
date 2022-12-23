using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditionalStat : StatusEffectConditional
    {
        #region Variables

        public CustomTagStat StatToEffect { private set; get; }
        public float Value { private set; get; }
        
        #endregion
        
        #region Methods

        #endregion

        public StatusEffectConditionalStat( bool isRemovedOnReset, CustomTagStat statToEffect, float value) : base(isRemovedOnReset)
        {
            StatToEffect = statToEffect;
            Value = value;
        }
    }
}