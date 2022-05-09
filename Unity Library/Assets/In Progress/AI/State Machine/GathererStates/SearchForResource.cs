using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class SearchForResource : IState
    {
        #region Variables

        private readonly Gatherer gatherer;

        #endregion

        #region IState Methods

        public SearchForResource(Gatherer gathererIn)
        {
            gatherer = gathererIn;
        }

        public void Tick()
        {
            //gatherer.Target = Ch
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        #endregion

        #region Methods



        #endregion
    }
}