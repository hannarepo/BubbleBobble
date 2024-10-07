using UnityEngine;

namespace BubbleBobble
{
    public class QuitGame : MonoBehaviour
    {
        public void Quit()
        {
            print("Quitting game");
            Application.Quit();
        }
    }
}
