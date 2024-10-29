using System;
using TMPro;
using UnityEngine;

namespace BubbleBobble
{
    // Create a new Asset to menu to build new prefab assets
    [CreateAssetMenu(fileName = "New Power Up", menuName = "Shop/Power Up")]
    public class PowerUpData : ScriptableObject
    {
        [SerializeField] private PowerUpType _powerUpType = PowerUpType.None;
        [SerializeField] private string _name = "";
        [SerializeField] private Sprite _sprite = null;
        [SerializeField] private bool _isActive = false;

        public PowerUpType PowerUpType => _powerUpType;
        public String Name => _name;
        public Sprite Sprite => _sprite;
        public bool IsActive => _isActive;
    }
}
