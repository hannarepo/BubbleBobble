/// <remarks>
/// auhtor: Hanna Repo
/// </remarks>
/// 
/// <summary>
/// Input reader for reading and storing the player input.
/// Includes getters for use in other scripts.
/// </summary>
using UnityEngine;

namespace BubbleBobble
{
    public class InputReader : MonoBehaviour
    {
        private Controls _controls;
        private Vector2 _movementControls;
        private bool _jump = false;
        private bool _dropDown = false;

        #region UnityMethods
        private void Awake()
        {
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            _movementControls = _controls.Game.Player1Move.ReadValue<Vector2>();
            _jump = _controls.Game.Player1Jump.WasPerformedThisFrame();
            _dropDown = _controls.Game.Player1DropDown.WasPerformedThisFrame();
        }
        #endregion

        #region Getters
        public Vector2 Movement
        {
            get { return _movementControls; }
        }

        public bool Jump
        {
            get { return _jump; }
        }

        public bool DropDown
        {
            get { return _dropDown; }
        }
        #endregion
    }
}
