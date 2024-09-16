using System.Collections;
using System.Collections.Generic;
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
        
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _inventory = new Inventory();
            _playerMover = GetComponent<PlayerMover>();
            _shootBubble = GetComponent<ShootBubble>();
        }

        private void Update()
        {
            Vector2 movement = _inputReader.Movement;
            _playerMover.Move(movement);

            bool shoot = _inputReader.ShootBubble;
            _shootBubble.Shoot(shoot);
        }
    }
}
