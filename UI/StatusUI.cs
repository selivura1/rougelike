using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Status
{
    Safe,
    Unsafe,
    Danger,
    Dead
}

public class StatusUI : MonoBehaviour
{
    [SerializeField] Animator _statusAnim;
    PlayerSpawner _playerSpawner;
    private void Awake()
    {
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
    }
    public void SetStatus(Status status)
    {
        switch (status)
        {
            case Status.Safe:
                _statusAnim.Play("Safe");
                break;
            case Status.Unsafe:
                _statusAnim.Play("Unsafe");
                break;
            case Status.Danger:
                _statusAnim.Play("Danger");
                break;
            case Status.Dead:
                _statusAnim.Play("Dead");
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        var hp = _playerSpawner.GetPlayer().GetHealth();
        var mhp = _playerSpawner.GetPlayer().EntityStats.Health.Value;
        if (_playerSpawner.GetPlayer().Dead)
        {
            SetStatus(Status.Dead);
            return;
        }
        else
        if (hp < mhp * 0.25f)
        {
            SetStatus(Status.Danger);
            return;
        }
        if (hp < mhp * 0.75f)
        {
            SetStatus(Status.Unsafe);
            return;
        }
        else
        {
            SetStatus(Status.Safe);
            return;
        }
    }
}
