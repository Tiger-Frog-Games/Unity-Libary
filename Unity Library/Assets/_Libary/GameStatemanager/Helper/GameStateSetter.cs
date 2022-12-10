using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class GameStateSetter : MonoBehaviour
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

        [ContextMenu("Play Game")]
        public void SetGameStateGameplay()
        {
            GameStateManager.Instance.SetState(GameState.Gameplay);
        }
        
        [ContextMenu("Pause Game")]
        public void SetGameStatePauses()
        {
            GameStateManager.Instance.SetState(GameState.Paused);
        }
        
        #endregion
    }
}