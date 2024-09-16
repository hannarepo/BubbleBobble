using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class DropDown : MonoBehaviour
    {
        private InputReader _inputReader;
        private Collider2D _collider;
        [SerializeField] private int _dropDownLayer;
        [SerializeField] private int _playerLayer;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (Physics2D.IsTouchingLayers(_collider, _dropDownLayer) && _inputReader.DropDown)
            {
                Physics2D.IgnoreLayerCollision(_playerLayer, _dropDownLayer, true);
                Debug.Log("Drop down");
            }
        }
    }
}
