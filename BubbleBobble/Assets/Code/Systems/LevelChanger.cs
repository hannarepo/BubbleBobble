using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace BubbleBobble
{
    public class LevelChanger : MonoBehaviour
    {
        private float _levelIndex = 1;
        private bool _isLevelLoaded = false;
        private GameObject _newLevel;
        [SerializeField] private GameObject _currentLevel;
        [SerializeField] private GameObject _newLevelSpawnPoint;
        [SerializeField] private float _speed = 1.0f;

        void Update()
        {
            // If the new level is loaded, move the current and the new level up until the new level is centered
            if (_isLevelLoaded)
            {
                _newLevel.transform.position = Vector3.MoveTowards(_newLevel.transform.position, new Vector3(0f, 0f, 0f), _speed * Time.deltaTime);
                _currentLevel.transform.position = Vector3.MoveTowards(_currentLevel.transform.position, new Vector3(0f, 11f, 0f), _speed * Time.deltaTime);
                if (_newLevel.transform.position == new Vector3(0f, 0f, 0f))
                {
                    Destroy(_currentLevel);
                    _currentLevel = _newLevel;
                    _isLevelLoaded = false;
                }
            }
        }

        // Load the prefab of the next level and instantiate it at the spawn point
        public void LoadLevel()
        {
            _levelIndex++;
            GameObject _levelPrefab = Resources.Load<GameObject>("Prefabs/Levels/Level" + _levelIndex);
            _newLevel = Instantiate(_levelPrefab, _newLevelSpawnPoint.transform.position, Quaternion.identity);
            _isLevelLoaded = true;
        }
    }
}
