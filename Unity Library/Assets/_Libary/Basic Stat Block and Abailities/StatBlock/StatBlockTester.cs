using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatBlockTester : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StatBlock statBlock;

        [SerializeField] private CustomTagStat hp;
        
        private StatusEffectDuration.OnProcEvent test;
        
        #endregion
        
        #region Methods

        public void testAddingDurationStatOne()
        {
            test = OnDealDamage;
            var temp = new StatusEffectDuration( true, test, 10f, 1f );

            statBlock.AddStatusEffectDuration(temp);
        }

        public void OnDealDamage()
        {
            statBlock.AddStatusEffectInstant(new StatusEffectInstant(hp, -1));
        }
        
        #endregion
    }
}