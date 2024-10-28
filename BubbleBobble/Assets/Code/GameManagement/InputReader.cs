/// <remarks>
/// author: Hanna Repo
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
        private bool _shootBubble = false;
        private bool _jumpOnBubble = false;

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
            _movementControls = _controls.Game.Move.ReadValue<Vector2>();
            _jump = _controls.Game.Jump.WasPressedThisFrame();
            _jumpOnBubble = _controls.Game.Jump.IsPressed();
            _shootBubble = _controls.Game.Shoot.WasPerformedThisFrame();

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

        public bool JumpOnBubble
        {
            get { return _jumpOnBubble; }
        }

        public bool ShootBubble
        {
            get { return _shootBubble; }
        }
        #endregion
    }
}
