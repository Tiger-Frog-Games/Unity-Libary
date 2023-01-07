using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class StatusEffect 
    {
        #region Variables
        
        //ID?
        //Name?
        
        #endregion
        
        #region Methods

        public StatusEffect()
        {
            
        }
        
        public virtual void OnAddEffect()
        {
            //OnEffectOverFromTime?.Invoke();
        }

        public virtual void Reset()
        {
            
        }
        
        public abstract StatusEffect Clone();

        #endregion

    }
}