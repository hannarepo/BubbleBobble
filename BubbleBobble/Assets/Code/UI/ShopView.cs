using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubbleBobble
{

    /// <summary>
    /// Handles opening the shop view and pausing the game in the background (No longer in use).
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        public void OpenShop()
        {
            _shopMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void CloseShop()
        {
            _shopMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
