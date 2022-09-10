using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class CoroutineStartingThem : MonoBehaviour
    {
        #region Variables

        private WaitForSeconds _waitingForOneSec = new WaitForSeconds(1);
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        
        [ContextMenu("Prevent multiple Starts")]
        private void preventMultipleStarts()
        {
            if (_countDownIsRunning)
            {   
                print("Start was prevented");
                return;
            }
            if (_countDown == null) _countDown = CountDownToZero(5);

            StartCoroutine(_countDown);
        }

        [ContextMenu("Interupt in progress")]
        private void InteruptInProggress()
        {
            if (_countDown != null && _countDownIsRunning)
            {
                print("I was interupted");
                StopCoroutine(_countDown);
            }
            _countDown = CountDownToZero(5);
            StartCoroutine(_countDown);
        }

        private IEnumerator _countDown;
        private bool _countDownIsRunning = false;
        private IEnumerator CountDownToZero(int timeIn)
        {
            _countDownIsRunning = true;
            print($"Starting Countdown for - {timeIn}");
            for (int i = timeIn - 1; i >= 0; i--)
            {
                yield return _waitingForOneSec;
                print(i);
            }

            _countDownIsRunning = false;
            print("TimerDone");
        }
        
        #endregion
    }
}