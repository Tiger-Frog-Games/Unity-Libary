using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

namespace TigerFrogGames
{
    public class StatusEffectUnitManagerTester : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StatusEffectUnitManager statBlock;

        [SerializeField] private CustomTagStat hp;
        
        private StatusEffectDuration.OnProcEvent test;
        
        #endregion
        
        #region Methods

        public void testAddingDurationStatOne()
        {
            test = OnDealDamage;
            var temp = new StatusEffectDuration( StatusEffectDurationConflict.AddUniqueStatusEffect, duration: 10, procCooldown: 1f , onProcEvent: test );

            statBlock.AddStatusEffectDuration(temp);
        }

        
        
        public void OnDealDamage()
        {
            statBlock.AddStatusEffectInstant(new StatusEffectInstant(hp, -1));
        }

        public void OnStackEvent(int current, int max )
        {
            print($"Current: {current} --- Max:{max}");
        }

        private StatusEffectDuration testDuration;
        public void testStackingStatusEffect()
        {

            StatusEffectDuration.OnStackEvent testStackEvent = OnStackEvent;
            
            //var temp = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onStackEvent: testStackEvent );
            
            if(testDuration == null) testDuration = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onStackEvent: testStackEvent );
            
            statBlock.AddStatusEffectDuration(testDuration);
        }

        public void testDoNothingEffect()
        {
            
        }
        
        
        
        #endregion
    }
}