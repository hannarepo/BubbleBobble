//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Config/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BubbleBobble
{
    public partial class @Controls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""babbc889-d35c-4cb3-886f-0a30fe876614"",
            ""actions"": [
                {
                    ""name"": ""Player1Move"",
                    ""type"": ""Value"",
                    ""id"": ""8b4a68ce-95cd-4376-8684-88cf126baf44"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Player1Jump"",
                    ""type"": ""Button"",
                    ""id"": ""10a9c9b9-ea9a-41ec-a5ce-a92e9d904720"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Player1Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""aa82399c-74c2-45b9-95ac-f4ba0563f786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Player1DropDown"",
                    ""type"": ""Button"",
                    ""id"": ""6915b64d-7e16-4f3a-8a4a-4c9e22fadb4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b3ee9a89-d854-43f1-9bc4-51e7d2cd10fc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d4bd63d9-9707-44e3-9dd2-6470b01991e6"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""57e07f9a-cf54-4115-b0b1-4f7ad485b4e8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5bc2c490-41a9-46c8-b62a-407b85fcaa78"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2f310d83-d9bf-4aa7-bc0b-541470c3f04c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4d217204-88a3-4812-9d43-5aea1045a3df"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dafd14b-4adb-4dc1-a019-3086a6687476"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e83b1a7c-ea6a-4f7a-9b74-8f5a475d320f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1DropDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_Player1Move = m_Game.FindAction("Player1Move", throwIfNotFound: true);
            m_Game_Player1Jump = m_Game.FindAction("Player1Jump", throwIfNotFound: true);
            m_Game_Player1Shoot = m_Game.FindAction("Player1Shoot", throwIfNotFound: true);
            m_Game_Player1DropDown = m_Game.FindAction("Player1DropDown", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Game
        private readonly InputActionMap m_Game;
        private List<IGameActions> m_GameActionsCallbackInterfaces = new List<IGameActions>();
        private readonly InputAction m_Game_Player1Move;
        private readonly InputAction m_Game_Player1Jump;
        private readonly InputAction m_Game_Player1Shoot;
        private readonly InputAction m_Game_Player1DropDown;
        public struct GameActions
        {
            private @Controls m_Wrapper;
            public GameActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Player1Move => m_Wrapper.m_Game_Player1Move;
            public InputAction @Player1Jump => m_Wrapper.m_Game_Player1Jump;
            public InputAction @Player1Shoot => m_Wrapper.m_Game_Player1Shoot;
            public InputAction @Player1DropDown => m_Wrapper.m_Game_Player1DropDown;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void AddCallbacks(IGameActions instance)
            {
                if (instance == null || m_Wrapper.m_GameActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GameActionsCallbackInterfaces.Add(instance);
                @Player1Move.started += instance.OnPlayer1Move;
                @Player1Move.performed += instance.OnPlayer1Move;
                @Player1Move.canceled += instance.OnPlayer1Move;
                @Player1Jump.started += instance.OnPlayer1Jump;
                @Player1Jump.performed += instance.OnPlayer1Jump;
                @Player1Jump.canceled += instance.OnPlayer1Jump;
                @Player1Shoot.started += instance.OnPlayer1Shoot;
                @Player1Shoot.performed += instance.OnPlayer1Shoot;
                @Player1Shoot.canceled += instance.OnPlayer1Shoot;
                @Player1DropDown.started += instance.OnPlayer1DropDown;
                @Player1DropDown.performed += instance.OnPlayer1DropDown;
                @Player1DropDown.canceled += instance.OnPlayer1DropDown;
            }

            private void UnregisterCallbacks(IGameActions instance)
            {
                @Player1Move.started -= instance.OnPlayer1Move;
                @Player1Move.performed -= instance.OnPlayer1Move;
                @Player1Move.canceled -= instance.OnPlayer1Move;
                @Player1Jump.started -= instance.OnPlayer1Jump;
                @Player1Jump.performed -= instance.OnPlayer1Jump;
                @Player1Jump.canceled -= instance.OnPlayer1Jump;
                @Player1Shoot.started -= instance.OnPlayer1Shoot;
                @Player1Shoot.performed -= instance.OnPlayer1Shoot;
                @Player1Shoot.canceled -= instance.OnPlayer1Shoot;
                @Player1DropDown.started -= instance.OnPlayer1DropDown;
                @Player1DropDown.performed -= instance.OnPlayer1DropDown;
                @Player1DropDown.canceled -= instance.OnPlayer1DropDown;
            }

            public void RemoveCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGameActions instance)
            {
                foreach (var item in m_Wrapper.m_GameActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GameActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IGameActions
        {
            void OnPlayer1Move(InputAction.CallbackContext context);
            void OnPlayer1Jump(InputAction.CallbackContext context);
            void OnPlayer1Shoot(InputAction.CallbackContext context);
            void OnPlayer1DropDown(InputAction.CallbackContext context);
        }
    }
}
