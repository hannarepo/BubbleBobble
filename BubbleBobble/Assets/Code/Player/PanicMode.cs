using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class PanicMode : MonoBehaviour
    {
        private bool _canPanic = false;
        [SerializeField] private float _panicTime = 2f;
        [SerializeField] private float _timeBeforePanic = 1f;
        private InputReader _inputReader;
        private Rigidbody2D _rb;

        private void Start()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_canPanic)
            {
                _inputReader.enabled = false;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if(trigger.gameObject.GetComponent<GroundFireTrigger>())
            {
                _canPanic = true;
            }
        }

        private void OnTriggerExit2D(Collider2D trigger)
        {
            if(trigger.gameObject.GetComponent<GroundFireTrigger>())
            {
                _canPanic = false;
                _inputReader.enabled = true;
            }
        }
    }
}
