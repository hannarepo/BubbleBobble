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

            print(movement.x);

            LookRight(movement);
        }

        private void LookRight(Vector2 movement)
        {
            bool lookRight = movement.x < 0;
            _spriteRenderer.flipX = lookRight;
        }
    }
}
