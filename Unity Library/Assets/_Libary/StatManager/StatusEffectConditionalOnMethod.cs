using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectConditionalOnMethod : StatusEffectConditional
    {
        #region Variables

        #endregion

        #region Methods

        public StatusEffectConditionalOnMethod(bool isRemovedOnReset) : base(isRemovedOnReset)
        {
            //AmethodToLisenTo += ApplyEffect;
        }

        public override void OnAddEffect()
        {
            base.OnAddEffect();
            //Unsubscribe ApplyEffect to the lisend to method
        }

        private void ApplyEffect()
        {
            
        }
        
        #endregion
    }
}