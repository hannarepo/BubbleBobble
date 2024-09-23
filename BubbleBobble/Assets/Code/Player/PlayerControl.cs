using UnityEngine;

namespace BubbleBobble
{
    [RequireComponent(typeof(InputReader))]
    public class PlayerControl : MonoBehaviour
    {
        private InputReader _inputReader;
        private Inventory _inventory;
        private PlayerMover _playerMover;
        private ShootBubble _shootBubble;
        private SpriteRenderer _spriteRenderer;
        private bool _lookRight;

        public bool LookingRight => _lookRight;
        
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _inventory = new Inventory();
            _playerMover = GetComponent<PlayerMover>();
            _shootBubble = GetComponent<ShootBubble>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 movement = _inputReader.Movement;
            _playerMover.Move(movement);

            bool shoot = _inputReader.ShootBubble;
            _shootBubble.Shoot(shoot, movement);

            // print(movement.x);

            LookRight(movement);
        }

        private void LookRight(Vector2 movement)
        {
            if (movement.x < 0)
            {
                _lookRight = true;
            }
            else if (movement.x > 0)
            {
                _lookRight = false;
            }

            _spriteRenderer.flipX = _lookRight;
        }
    }
}
