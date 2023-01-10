using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class StatusEffect 
    {
        #region Variables
        
        //ID?

        public Guid ID { private set; get;}
        
        //Name?
        
        #endregion
        
        #region Methods

        public StatusEffect()
        {
            ID = Guid.NewGuid();
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