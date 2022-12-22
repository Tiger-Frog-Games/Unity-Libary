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

        [SerializeField] private CustomTagStat stat;
        
        private StatusEffectDuration.OnProcEvent test;
        
        #endregion
        
        #region Methods

        public void testAddingDurationStatOne()
        {
            test = OnDealDamage;
            var temp = new StatusEffectDuration( stat, 0, .5f, test, .1f, 2 );

            print("Sanity Check");
            statBlock.AddStatusEffectDuration(temp);
        }

        private void OnDealDamage()
        {
            print("moew to the mix");
        }
        
        #endregion
    }
}