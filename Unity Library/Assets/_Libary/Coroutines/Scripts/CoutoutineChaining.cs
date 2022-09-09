using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class CoutoutineChaining : MonoBehaviour
    {
        #region Variables

        private WaitForSeconds _waitingForOneSec = new WaitForSeconds(1);
        
        #endregion

        #region Unity Methods
        
        #endregion

        #region Methods

        [ContextMenu("Chain")]
        private void chain()
        {
            StartCoroutine(chainStarter());
        }
        
        private IEnumerator chainStarter()
        {
        
            yield return StartCoroutine(one());
            yield return StartCoroutine(two());
        }
        
        [ContextMenu("sameTime")]
        private void sameTime()
        {
            StartCoroutine(one());
            StartCoroutine(two());
        }
        
        private IEnumerator one()
        {
            print("One - Start");
            
            for (int i = 0; i < 5; i++)
            {
                print($"One - {i}");
                yield return _waitingForOneSec;
            }
            
            print("One - End");
        }

        private IEnumerator two()
        {
            print("Two - Start");
           
            for (int i = 0; i < 5; i++)
            {
                print($"Two - {i}");
                yield return _waitingForOneSec;
            }
            
            print("Two - End");
        }
        
        
        #endregion
    }
}