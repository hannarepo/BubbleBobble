using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerControl _playerControl;
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
        }
    }
}
