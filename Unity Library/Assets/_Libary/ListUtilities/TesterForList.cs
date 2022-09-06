using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class TesterForList : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<GameObject> TestGameObjects;

        private int _lastIndex;
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        [ContextMenu("GetRandomInxdex")]
        private void printRandomNonerepeatingIndex()
        {
            _lastIndex = ListUtilities.GetNoneRepeatingIndex(_lastIndex, TestGameObjects.Count);
            
            print(_lastIndex);
        }

        #endregion
    }
}