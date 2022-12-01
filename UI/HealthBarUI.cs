using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] ProgressBar _healthBar;
    [SerializeField] ProgressBar _damageBar;
    [SerializeField] RectTransform _barTransform;
    private PlayerSpawner _playerSpawner;
    EntityBase _player;
    [SerializeField] float damageBarSpeed = 1;
    private void Start()
    {
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        _player = _playerSpawner.GetPlayer().GetComponent<EntityBase>();
    }
    private void Update()
    {
        _healthBar.CurrentValue = _player.GetHealth();
        _healthBar.Max = _player.EntityStats.Health.Value;
        _damageBar.CurrentValue = Mathf.MoveTowards(_damageBar.CurrentValue, _healthBar.CurrentValue, damageBarSpeed);
        _damageBar.Max = _healthBar.Max;
    }
} 
