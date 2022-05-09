using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TigerFrogGames
{
    public class MoveToResourse : IState
    {
        #region Variables

        private readonly Gatherer gatherer;
        
        private readonly NavMeshAgent navAgent;

        private Vector3 lastPosition = Vector3.zero;

        public float timeStuck { get; private set; }
        #endregion

        #region State Methods

        public MoveToResourse(Gatherer gatherIn,  NavMeshAgent agent)
        {
            gatherer = gatherIn;
            navAgent = agent;
        }

        public void Tick()
        {
            if (Vector3.Distance(gatherer.transform.position, lastPosition) <= 0f)
            {
                timeStuck += Time.deltaTime;
            }
            lastPosition = gatherer.transform.position;
        }

        public void OnEnter()
        {
            timeStuck = 0;
            navAgent.enabled = true;
            navAgent.SetDestination(gatherer.Target.transform.position);
        }

        public void OnExit()
        {
            navAgent.enabled = false;
        }

        #endregion

        #region Methods

        #endregion
    }
}