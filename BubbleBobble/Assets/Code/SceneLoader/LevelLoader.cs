using UnityEngine.SceneManagement;
using UnityEngine;
using BubbleBobble;

namespace SceneLoader
{
	public static class LevelLoader
	{
		public enum LoadingState
		{
			None = 0,
			Begin,
			FadeToBlack,
			UnloadingPrevious,
			LoadingNext,
			FadeToClear,
			Finish,
			Options
		}

		private const string LoadingSceneName = "Loader";
		//private const string OptionsSceneName = "OptionsScene";
		private static string s_nextSceneName;
		private static Scene s_originalScene;
		// Viittaus options-sceneen (jotta se voidaan sulkea)
		private static Scene s_optionsScene;
		private static LoadingState s_loadingState;
		private static Fader s_fader;

		// 1. Ladataan Loader-scene additiivisesti ja asynkronisesti
		// 2. Häivytetään näyttö mustaksi
		// 3. Kun näyttö musta -> poistetaan nykyinen scene muistista
		// 4. Aloitetaan uuden scenen lataus (additiivisesti ja asynkronisesti)
		// 5. Kun uusi scene on ladattu -> häivytys mustasta kirkkaaksi
		// 6. Kun häivytys on valmis -> poistetaan Loader-scene muistista

		// Lataa uusi taso
		public static bool LoadLevel(string levelName)
		{
			// Jos lataus on jo käynnissä, ei tehdä mitään
			if (s_loadingState != LoadingState.None)
			{
				return false;
			}

			// Uuden ladattavan scenen nimi
			s_nextSceneName = levelName;

			// Tallennetaan viittaus nykyiseen sceneen.
			s_originalScene = SceneManager.GetActiveScene();

			// Aloitetaan scenen latausprosessi
			s_loadingState = LoadingState.Begin;

			// Aloita sceneLoaded-eventin kuuntelu välittämällä OnSceneLoaded-metodin viittaus
			// kyseiselle eventille.
			SceneManager.sceneLoaded += OnSceneLoaded;

			// Aloita Loader-scenen lataus
			// LoadMode: Additive, koska emme halua poistaa nykyistä sceneä muistista ennen,
			// kun näyttö on häivytetty mustaksi.
			SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);

			return true;
		}

		public static void OpenOptions(string optionsSceneName)
		{
			Time.timeScale = 0f;
			s_loadingState = LoadingState.Options;
			SceneManager.LoadSceneAsync(optionsSceneName, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public static void CloseOptions()
		{
			Time.timeScale = 1f;
			SceneManager.UnloadSceneAsync(s_optionsScene);
			s_loadingState = LoadingState.None;
		}

		private static void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
		{
			// Lopetetaan sceneLoaded-eventin kuuntelu
			SceneManager.sceneLoaded -= OnSceneLoaded;

			switch (s_loadingState)
			{
				case LoadingState.Begin:
					// Loader-scene on ladattu. Häivytetään näyttö mustaksi.
					// Haetaan viittaus Faderiin
					s_fader = GameObject.FindObjectOfType<Fader>();

					// Ala kuuntelemaan Faderin FadeComplete-eventtiä
					Fader.FadeComplete += OnFadeComplete;

					// Aloita näytön häivytys mustaksi
					s_fader.StartFade(Fader.State.FadingIn);

					// Siirry seuraavaan lataustilaan
					s_loadingState = LoadingState.FadeToBlack;
					break;

				case LoadingState.LoadingNext:
					// Uusi scene ladattu, häivytetään näyttö kirkkaaksi
					Fader.FadeComplete += OnFadeComplete;
					s_fader.StartFade(Fader.State.FadingOut);
					s_loadingState = LoadingState.FadeToClear;
					break;

				case LoadingState.Options:
					s_optionsScene = loadedScene;
					break;
			}
		}

		private static void OnFadeComplete()
		{
			Fader.FadeComplete -= OnFadeComplete;

			switch (s_loadingState)
			{
				case LoadingState.FadeToBlack:
					// Näyttö häivytetty mustaksi, poista edellinen scene muistista
					// Kun scene on poistettu muistista, suoritus siirtyy OnSceneUnloaded-metodiin.
					SceneManager.sceneUnloaded += OnSceneUnloaded;
					SceneManager.UnloadSceneAsync(s_originalScene);
					s_loadingState = LoadingState.UnloadingPrevious;
					break;

				case LoadingState.FadeToClear:
					SceneManager.sceneUnloaded += OnSceneUnloaded;
					SceneManager.UnloadSceneAsync(LoadingSceneName);
					s_loadingState = LoadingState.Finish;
					break;
			}
		}

		private static void OnSceneUnloaded(Scene scene)
		{
			SceneManager.sceneUnloaded -= OnSceneUnloaded;

			switch (s_loadingState)
			{
				case LoadingState.UnloadingPrevious:
					// Aloitetaan seuraavan scenen lataus
					SceneManager.sceneLoaded += OnSceneLoaded;
					SceneManager.LoadSceneAsync(s_nextSceneName, LoadSceneMode.Additive);
					s_loadingState = LoadingState.LoadingNext;
					break;

				case LoadingState.Finish:
					// Latausprosessi on valmis, nollataan lataustila
					s_fader = null;
					s_loadingState = LoadingState.None;
					break;
			}
		}
	}
}