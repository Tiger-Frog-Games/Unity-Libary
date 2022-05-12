using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class OnAwakeSetGameState : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameState startingState;
        #endregion

        #region Unity Methods

        private void Start()
        {
            GameStateManager.Instance.SetState(startingState);
        }

        #endregion

        #region Methods

        #endregion
    }
}