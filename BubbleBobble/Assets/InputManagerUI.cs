using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleBobble
{
    public class InputManagerUI : MonoBehaviour
    {
        public static InputManagerUI instance;
        public bool MenuOpenCloseInput { get; private set; }
        private PlayerInput _playerInput;
        private InputAction _menuOpenCloseAction;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            _playerInput = GetComponent<PlayerInput>();
            _menuOpenCloseAction = _playerInput.actions["Pause"];
        }

        private void Update()
        {
            MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
        }
    }
}
