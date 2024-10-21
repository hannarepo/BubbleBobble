using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class Fireball : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            SpreadFire();
        }

        private void SpreadFire()
        {
            
        }
    }
}
