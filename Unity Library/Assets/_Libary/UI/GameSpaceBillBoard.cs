using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    public class GameSpaceBillBoard : MonoBehaviour
    {
        #region Variables

        private Camera _cam;
        
        #endregion

        
        #region Unity Methods

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        }

        #endregion

        #region Methods

        #endregion
    }
}