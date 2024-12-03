using UnityEngine;

namespace SceneLoader
{
	public class LoadLevelTrigger : MonoBehaviour
	{
		[SerializeField, Tooltip("Tason nimi, joka ladataan")]
		private string _levelName = "Level1";
		public void OnButtonClicked()
		{
			// Ladataan uusi taso
			LevelLoader.LoadLevel(_levelName);
		}
	}
}
