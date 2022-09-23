using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class CustomTag : MonoBehaviour
    {
        #region Variables

        [field: SerializeField] public List<CustomTagScriptableObject> Tags { get; private set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public bool HasTag(CustomTagScriptableObject t)
        {
            return Tags.Contains(t);
        }
        
        #endregion
    }
}