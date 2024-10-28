using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class GroundFireDestroy : MonoBehaviour
    {
        [SerializeField] private int _timeBeforeDestroy = 1;
        void Start()
        {
            Invoke("DestroyFire", _timeBeforeDestroy);
        }

        private void DestroyFire()
        {
            Destroy(gameObject);
        }
    }
}
