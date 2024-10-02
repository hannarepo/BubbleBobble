using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
    public class PlayerPosition : MonoBehaviour
    {
        public static Vector2 playerPosition;

        void Update()
        {
            playerPosition = transform.position;

        }
        
    }
}
