using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class StatusEffect 
    {
        #region Variables
        
        public CustomTagStat StatToEffect { private set; get; }
        public float Value { private set; get; }
        
        #endregion
        
        #region Methods

        public StatusEffect(CustomTagStat statToEffect, float value)
        {
            StatToEffect = statToEffect;
            Value = value;
        }
        
        #endregion
        
    }
}