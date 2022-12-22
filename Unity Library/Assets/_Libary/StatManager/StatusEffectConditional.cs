using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditional : StatusEffect
    {
        #region Variables

        public event Action OnEffectOverFromTime;
        
        #endregion
        
        #region Methods

        public StatusEffectConditional(CustomTagStat statToEffect, float value) : base(statToEffect, value)
        {
        }
        
        public void CallOnRemoveEffect()
        {
            OnEffectOverFromTime?.Invoke();
        }
        
        #endregion
        
    }
}