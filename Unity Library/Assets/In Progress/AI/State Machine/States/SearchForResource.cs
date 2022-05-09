using Micosmo.SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class SearchForResource : IState
    {
        #region Variables

        private readonly Gatherer gatherer;
        private readonly Sensor sensorResources;

        #endregion

        #region IState Methods

        public SearchForResource(Gatherer gathererIn, Sensor sensor)
        {
            gatherer = gathererIn;
            sensorResources = sensor;
        }

        public void Tick()
        {
            gatherer.Target = ChooseOneOfNearestResource();
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        #endregion

        #region Methods

        private GatherableResource ChooseOneOfNearestResource()
        {
            List<GatherableResource> temp = new List<GatherableResource>();
            sensorResources.GetDetectedComponents<GatherableResource>(temp);
            if (temp.Count > 0)
            {
                return temp[0];
            }
            return null;
        }

        #endregion
    }
}