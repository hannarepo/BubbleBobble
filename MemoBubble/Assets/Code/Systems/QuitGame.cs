using UnityEngine;

namespace MemoBubble
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
