using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TigerFrogGames
{
    public class MouseClicker : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Camera cam;
        
        [SerializeField] private InputActionAsset playerInput;

        
        public InputAction _mouseLeftClick, _mouseRightClick;

        #endregion

        #region Unity Methods

        private void OnEnable() {
            playerInput.Enable();
        }

        private void OnDisable() {
            playerInput.Disable();
        }
        
        private void Awake()
        {
            if (playerInput == null)
            {
                return;
            }

            _mouseLeftClick = playerInput.FindAction("LeftClick");
            _mouseLeftClick.performed += MouseLeftClickOnPerformed;
            
            _mouseRightClick = playerInput.FindAction("RightClick");
            _mouseRightClick.performed += MouseRightClickOnPerformed ;
        }
        
        private void OnDestroy()
        {
            if (_mouseLeftClick != null)
            {
                _mouseLeftClick.performed -= MouseLeftClickOnPerformed;
            }
            
            if (_mouseRightClick != null)
            {
                _mouseRightClick.performed -= MouseRightClickOnPerformed;
            }
        }

        #endregion

        #region Methods

        private void MouseLeftClickOnPerformed(InputAction.CallbackContext obj)
        {
            //print("Mouse has been left clicked");
            
            RaycastHit hit; 
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            if (Physics.Raycast(cam.ScreenPointToRay(mousePosition), out hit)) 
            {
                hit.collider.GetComponent<IClickable>()?.OnClick();
            }
            
        }
        
        private void MouseRightClickOnPerformed(InputAction.CallbackContext obj)
        {
            //print("Mouse has been right clicked");
        }
        
        #endregion
    }
}