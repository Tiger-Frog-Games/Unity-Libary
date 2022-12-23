using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditional : StatusEffect
    {
        #region Variables

        private bool _isRemovedOnReset;
        
        #endregion
        
        #region Methods

        //todo accept an OnOver delaget
        public StatusEffectConditional(bool isRemovedOnReset)
        {
            _isRemovedOnReset = isRemovedOnReset;
        }
        
        public void OnEffectOver()
        {
            //OnEffectOver?.Invoke();
        }
        
        
        #endregion
        
    }
}