using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class OnClickPrintTags : MonoBehaviour, IClickable
    {
        #region Variables

        #endregion

        #region Unity Methods
        
        public void OnClick()
        {
            TryGetComponent<CustomTag>(out CustomTag tags);
            tags.printTags();
        }

        #endregion

        #region Methods

        #endregion
    }
}