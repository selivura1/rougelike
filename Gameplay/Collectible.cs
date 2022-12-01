using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected EntityBase _target;
    [SerializeField] protected float _value = 5;
    [SerializeField] protected float _speed = 5;
    [SerializeField] protected float range = 2;
    bool collected;
    void Start()
    {
        _target = FindObjectOfType<PlayerEntity>();
    }

    void FixedUpdate()
    {
        if (!_target) return;
        
            if (Vector3.Distance(_target.transform.position, transform.position) <= range)
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
        
        if(transform.position == _target.transform.position)
        {
            if (!collected)
            {
                OnCollected();
            }
            collected = true;
        }
    }

    public virtual void OnCollected()
    {
        _target.Heal(_target.EntityStats.Health.Value * _value / 100);
        Destroy(gameObject, 0.1f);
    }
}
