using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Jump : MonoBehaviour
    {
        private InputReader _inputReader;
        private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce = 5f;
        private bool _canJump = true;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_inputReader.Jump)
            {
                Debug.Log("Jump");
                _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
