using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class StatusEffectUnitManagerTester : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StatusEffectHandler UnitStatusEffectHandler;

        [SerializeField] private CustomTagStat hp;
        [SerializeField] private CustomTagStat MaxHp;
        
        private StatusEffectDuration testOnDealDamageOverTimeUniqueStatusEffects;
        
        private StatusEffectDuration testDurationLostOneStack;
        
        private StatusEffectDuration testDurationLoseAllStacks;
        
        private StatusEffectDuration testCantAddMoreThenOne;
        
        private StatusEffectDuration testRefreshDuration;
        
        private StatusEffectDuration testAddDurationTime;

        private StatusEffectDuration testAddStackAddDurationTime;

        private StatusEffectConditional testConditional;
        

        #endregion

        private void Start()
        {
            StatusEffectDuration.OnStackEvent testStackEvent = OnStackEvent;
            StatusEffectDuration.OnApplyEvent apply = printApplied;
            StatusEffectDuration.OnRemoveEvent remove = printOver;
            StatusEffectDuration.OnRefreshEvent refresh = OnRefreshEvent;
            
            StatusEffectConditional.OnProcEvent dealDamageMethod = DealDamage;

            testOnDealDamageOverTimeUniqueStatusEffects = new StatusEffectDuration( StatusEffectDurationConflict.AddIndependent, duration: 10, procCooldown: 1f , onProcEvent: dealDamageMethod );

            testDurationLostOneStack = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onChangeStackEvent: testStackEvent , loseOneStackOnOver: true, onApplyEvent: apply, onRemoveEvent: remove );
            
            testDurationLoseAllStacks = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onChangeStackEvent: testStackEvent, onApplyEvent: apply, onRemoveEvent: remove );
            
            testCantAddMoreThenOne = new StatusEffectDuration(StatusEffectDurationConflict.CantAddMoreThenOne, duration: 10f, onApplyEvent: apply, onRemoveEvent: remove );

            testRefreshDuration = new StatusEffectDuration(StatusEffectDurationConflict.Refresh, duration: 10f, onApplyEvent: apply, onRefreshEvent:refresh, onRemoveEvent: remove);
            
            testAddDurationTime = new StatusEffectDuration(StatusEffectDurationConflict.AddTime, duration: 10f, durationToAdd: 1f, onApplyEvent: apply, onRefreshEvent:refresh, onRemoveEvent: remove);

            testAddStackAddDurationTime = new StatusEffectDuration(StatusEffectDurationConflict.AddStackAddTime, duration: 10f, durationToAdd: 1f, onApplyEvent: apply, onRefreshEvent:refresh,stackMax: 10,startingStacks: 5, onChangeStackEvent: testStackEvent, onRemoveEvent: remove, loseOneStackOnOver: true);

            testConditional = new StatusEffectConditional(onApplyEvent: apply, onRemoveEvent: remove, onOnProcEvent: dealDamageMethod, procCooldown: 1f);
        }

        #region Methods

        public void testAddDamageOverTimeUniqueEffect()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testOnDealDamageOverTimeUniqueStatusEffects);
        }

        
        
        public void OnStackEvent(int current, int max )
        {
            print($"Current: {current} --- Max: {max}");
        }

        public void OnRefreshEvent(float durationLeft)
        {
            print($"TimeLeft: {durationLeft}");
        }
        
        public void testStackingStatusEffectLostOneStack()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testDurationLostOneStack);
        }

        public void testStackingStatusEffectClearAll()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testDurationLoseAllStacks);
        }
        
        public void testCantAddMoreThenOneTest()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testCantAddMoreThenOne);
        }

        public void testRefresh()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testRefreshDuration);
        }

        public void testAddTimeOnto()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testAddDurationTime);
        }

        public void testAddStackAddTime()
        {
            UnitStatusEffectHandler.AddStatusEffectDurational(testAddStackAddDurationTime);
        }

        
        public void testTurnOnCondition()
        {
            UnitStatusEffectHandler.AddStatusEffectConditional( testConditional);
        }

        public void testTurnOffCondition()
        {
            UnitStatusEffectHandler.RemoveStatusEffectConditional(testConditional);
        }
        
        private void printApplied()
        {
            print("Status Effect Started");
        }
        
        public void DealDamage()
        {
            UnitStatusEffectHandler.AddStatusEffectInstant(new StatusEffectInstant(hp, -1, true));
        }

        public void ChangeMax()
        {
            UnitStatusEffectHandler.AddStatusEffectInstant(new StatusEffectInstant(MaxHp, 1, false));
        }
        
        private void printOver()
        {
            print("Status Effect Over");
            //testDuration = null;
        }

        
        

        #endregion
    }
}