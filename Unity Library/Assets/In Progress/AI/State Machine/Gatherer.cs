using Micosmo.SensorToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TigerFrogGames
{
    /// <summary>
    /// This is the state machine for the ai gather. 
    /// 
    /// He looks around and gather materials. Then deposits them in the main hub. 
    /// 
    /// </summary>
    public class Gatherer : MonoBehaviour
    {
        #region Variables

        [Header("Inspector Setters")]
        [SerializeField] private Sensor sensorResources;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public GatherableResource Target { get; set; }

        public GameObject moveTargetLocation => test();

        private GameObject test()
        {
            return this.gameObject;
        }

        public GameObject StockPile;
        private float gathered;
        private float maxCarried;

        private StateMachine StateMachine;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //Set up State Machine
            StateMachine = new StateMachine();

            //Set up states
            var search = new SearchForResource(this, sensorResources);
            var moveToSelectedResource = new MoveToResourse(this, navMeshAgent);

            //set up conditions
            At(search, moveToSelectedResource, hasTarget());

            //initial state
            StateMachine.SetState(search);

            //Helper Functions
            
            //sort hand for Statemachine add transition function
            void At(IState to, IState from, Func<bool> condition) => StateMachine.AddTransition(to, from, condition);

            // Exit Conditions
            Func<bool> hasTarget() => () => Target != null;
            Func<bool> StuckForOverASecond() => () => moveToSelectedResource.timeStuck > 1f;
            Func<bool> ReachedResource() => () => Target != null && Vector3.Distance(transform.position,Target.transform.position) < 1f;
            Func<bool> InventoryFull() => () => gathered >= maxCarried;
            Func<bool> TargetIsDepletedAndICanCarryMore() => () => StockPile != null && Vector3.Distance(transform.position, Target.transform.position) < 1f;
        }

        private void Update()
        {
            StateMachine.tick();
        }

        #endregion

        #region Methods



        #endregion
    }
}