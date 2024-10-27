using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class PanicMode : MonoBehaviour
    {
        private bool _canPanic = false;
        [SerializeField] private float _panicTime = 2f;
        [SerializeField] private float _panicImmuneTime = 1f;
        private InputReader _inputReader;
        private Rigidbody2D _rb;
        private float _timer = 0f;

        private void Start()
        {
            _inputReader = GetComponent<InputReader>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_canPanic)
            {
                _timer += Time.deltaTime;
                if (_timer <= _panicTime)
                {
                    Panic();
                }
                if (_timer >= _panicTime + _panicImmuneTime)
                {
                    _timer = 0f;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D trigger)
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
                _timer = 0f;
            }
        }

        private void Panic()
        {
            _inputReader.enabled = false;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
