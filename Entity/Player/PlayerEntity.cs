using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityBase
{
    [SerializeField] private float immortalTimer = 1f;
    private float timer;

    public Action onKill;
    private void FixedUpdate()
    {
        if (timer < immortalTimer)
        {
            timer += Time.fixedDeltaTime;
            invincible = true;
        }
        else
        {
            invincible = false;
        }
    }
}
