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
        
        private StatusEffectDuration.OnProcEvent DealDamageMethod;

        private StatusEffectDuration testOnDealDamageOverTimeUniqueStatusEffects;
        
        private StatusEffectDuration testDurationLostOneStack;
        
        private StatusEffectDuration testDurationLoseAllStacks;
        
        private StatusEffectDuration testCantAddMoreThenOne;
        
        private StatusEffectDuration testRefreshDuration;
        
        private StatusEffectDuration testAddDurationTime;

        private StatusEffectDuration testAddStackAddDurationTime;

        #endregion

        private void Start()
        {
            StatusEffectDuration.OnStackEvent testStackEvent = OnStackEvent;
            StatusEffectDuration.OnApplyEvent start = printApplied;
            StatusEffectDuration.OnRemoveEvent remove = printOver;
            StatusEffectDuration.OnRefreshEvent refresh = OnRefreshEvent;
            
            
            DealDamageMethod = DealDamage;
            
            testOnDealDamageOverTimeUniqueStatusEffects = new StatusEffectDuration( StatusEffectDurationConflict.AddUniqueStatusEffect, duration: 10, procCooldown: 1f , onOnProcEvent: DealDamageMethod );

            testDurationLostOneStack = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onChangeStackEvent: testStackEvent , loseOneStackOnOver: true, onApplyEvent: start, onRemoveEvent: remove );
            
            testDurationLoseAllStacks = new StatusEffectDuration(StatusEffectDurationConflict.AddStack, duration: 10f, startingStacks: 1, stackMax: 5, onChangeStackEvent: testStackEvent, onApplyEvent: start, onRemoveEvent: remove );
            
            testCantAddMoreThenOne = new StatusEffectDuration(StatusEffectDurationConflict.CantAddMoreThenOne, duration: 10f, onApplyEvent: start, onRemoveEvent: remove );

            testRefreshDuration = new StatusEffectDuration(StatusEffectDurationConflict.Refresh, duration: 10f, onApplyEvent: start, onRefreshEvent:refresh, onRemoveEvent: remove);
            
            testAddDurationTime = new StatusEffectDuration(StatusEffectDurationConflict.AddTime, duration: 10f, durationToAdd: 1f, onApplyEvent: start, onRefreshEvent:refresh, onRemoveEvent: remove);

            testAddStackAddDurationTime = new StatusEffectDuration(StatusEffectDurationConflict.AddStackAddTime, duration: 10f, durationToAdd: 1f, onApplyEvent: start, onRefreshEvent:refresh,stackMax: 10,startingStacks: 5, onChangeStackEvent: testStackEvent, onRemoveEvent: remove, loseOneStackOnOver: true);
        }

        #region Methods

        public void testAddDamageOverTimeUniqueEffect()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testOnDealDamageOverTimeUniqueStatusEffects);
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
            UnitStatusEffectHandler.AddStatusEffectDuration(testDurationLostOneStack);
        }

        public void testStackingStatusEffectClearAll()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testDurationLoseAllStacks);
        }
        
        public void testCantAddMoreThenOneTest()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testCantAddMoreThenOne);
        }

        public void testRefresh()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testRefreshDuration);
        }

        public void testAddTimeOnto()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testAddDurationTime);
        }

        public void testAddStackAddTime()
        {
            UnitStatusEffectHandler.AddStatusEffectDuration(testAddStackAddDurationTime);
        }
        
        private void printApplied()
        {
            print("Status Effect Started");
        }
        
        public void DealDamage()
        {
            UnitStatusEffectHandler.AddStatusEffectInstant(new StatusEffectInstant(hp, -1));
        }

        private void printOver()
        {
            print("Status Effect Over");
            //testDuration = null;
        }


        #endregion
    }
}