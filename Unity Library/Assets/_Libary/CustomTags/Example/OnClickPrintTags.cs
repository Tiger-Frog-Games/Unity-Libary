using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class OnClickPrintTags : MonoBehaviour
    {
        #region Variables

        #endregion

        #region Unity Methods
        
        private void OnClick()
        {
            TryGetComponent<CustomTag>(out CustomTag tags);
            tags.printTags();
        }

        #endregion

        #region Methods

        #endregion
    }
}