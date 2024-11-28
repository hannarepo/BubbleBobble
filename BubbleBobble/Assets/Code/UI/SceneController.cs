using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Handles the scene transitions. (no longer in use)
    /// </summary>
    ///
    /// <remarks>
    /// author: Juho Kokkonen
    /// </remarks>
    private string previousSceneName;

    // Lataa "Store"-scenen ja tallenna nykyinen scene
    public void LoadStoreScene()
    {
        // Tallenna nykyisen scenen nimi, jotta sinne voidaan palata myöhemmin
        previousSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Previous scene: " + previousSceneName);

        // Lataa "Store"-scene additiivisesti nykyisen scenen päälle
        SceneManager.LoadScene("Store", LoadSceneMode.Additive);
    }

    // Palaa edelliseen sceneen purkamalla "Store"-scenen
    public void ReturnToPreviousScene()
    {
        Debug.Log("Returning to: " + previousSceneName);

        // Varmista, että edellinen scene on tallennettu
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // Poista "Store"-scene
            SceneManager.UnloadSceneAsync("Store");

            // Palauta pelaaja edelliseen sceneen, jos se on edelleen aktiivinen
            // Tällä hetkellä ladattu scene on additiivisesti edellisen päälle, joten tässä ei tarvitse ladata sitä uudelleen.
        }
        else
        {
            Debug.LogWarning("No previous scene to return to.");
        }
    }
}
