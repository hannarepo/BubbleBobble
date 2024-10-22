using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
    public class GroundFireTrigger : MonoBehaviour
    {
        // TODO: Activate panic mode when player enters trigger
        // and prevent multiple instances of panic mode from triggering at once

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyTestScript>().Die(); // REFACTOR when enemy scripts are done
            }

        }
    }
}
