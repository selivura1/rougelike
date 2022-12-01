using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableCharacterAnimation : CharacterAnimation
{
    [SerializeField] private string damagedAnimation = "oof";
    EntityBase entity;
    private void Awake()
    {
        entity = GetComponent<EntityBase>();
    }
    private void OnEnable()
    {
        entity.onDamage += OnDamageTaken;
    }
    private void OnDisable()
    {
        entity.onDamage -= OnDamageTaken;
    }
    void OnDamageTaken(string arg)
    {
        animator.Play(damagedAnimation);
    }
}
