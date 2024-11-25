using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble
{
	public class TransitionScreen : MonoBehaviour
	{
		[SerializeField] private float _levelChangeDelay = 1f;
		private LevelChanger _levelChanger;
		private bool _levelChangeCalled = false;
		private void Start()
		{
			_levelChanger = FindObjectOfType<LevelChanger>();
		}

		private void Update()
		{
			if (_levelChanger.IsLevelLoaded && !_levelChangeCalled)
			{
				Invoke("DelayedLevelChange",_levelChangeDelay);
				_levelChangeCalled = true;
			}
		}

		private void DelayedLevelChange()
		{
			_levelChanger.LoadLevel();
		}
	}
}
