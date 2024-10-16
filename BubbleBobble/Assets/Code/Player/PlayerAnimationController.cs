using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerControl _playerControl;
        private Jump _jump;
        private bool _isWalking;

        public bool IsMoving
        {
            get { return _isWalking; }
            set { _isWalking = value; }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerControl = GetComponent<PlayerControl>();
            _jump = GetComponent<Jump>();
        }

        void Update()
        {
            bool isWalking = _animator.GetBool("IsWalking");

            if (_isWalking && !isWalking)
            {
                _animator.SetBool("IsWalking", true);
            }
            else if (!_isWalking && isWalking)
            {
                _animator.SetBool("IsWalking", false);
            }

            if (_jump.Jumping)
            {
                _animator.SetTrigger("Jumped");
            }
            else if (_jump.Falling)
            {
                _animator.SetTrigger("Falling");
            }
            else if (_jump.Grounded)
            {
                _animator.SetTrigger("Grounded");
            }

        }
    }
}
