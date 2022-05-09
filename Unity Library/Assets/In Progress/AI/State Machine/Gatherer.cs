using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private StateMachine StateMachine;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //Set up State Machine
            StateMachine = new StateMachine();

            //Set up states



        }

        #endregion

        #region Methods

        #endregion
    }
}