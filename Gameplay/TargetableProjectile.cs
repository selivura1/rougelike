using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TargetableProjectile : Projectile
{
    public void Initialize(EntityBase caster, Vector3 targetPos, float damage, float speed, float lifetime)
    {
        _initialized = true;
        _damage = damage;
        _caster = caster;
        _speed = speed;
        dir = targetPos - transform.position;
        transform.right = dir;
        Invoke(nameof(Terminate), lifetime);
    }
}
