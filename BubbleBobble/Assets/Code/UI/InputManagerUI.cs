using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleBobble
{

    /// <summary>
    /// Listen for inputs that are used to open and close the menu and store.
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    public class InputManagerUI : MonoBehaviour
    {
        public static InputManagerUI instance;
        public bool MenuOpenCloseInput { get; private set; }
        private PlayerInput _playerInput;
        private InputAction _menuOpenCloseAction;

        public bool StoreOpenCloseInput { get; private set; }
        private InputAction _storeOpenCloseAction;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            _playerInput = GetComponent<PlayerInput>();
            _menuOpenCloseAction = _playerInput.actions["Pause"];
            _storeOpenCloseAction = _playerInput.actions["OpenShop"];
        }

        private void Update()
        {
            MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
            StoreOpenCloseInput = _storeOpenCloseAction.WasPressedThisFrame();
        }
    }
}
