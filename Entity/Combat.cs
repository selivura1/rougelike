using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    EntityBase entity;
    [SerializeField] SpriteRenderer _graphics;
    float reloadTimer = 0;
    public Transform RangedAttackSpawn;
    bool canAttack = true;
    Vector2 attackDirection;
    Movement movement;
    Inventory inventory;
    public System.Action<Vector2> onAttack;
    private void Awake()
    {
        entity = GetComponent<EntityBase>();
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
    }
    public void StartAttack(Vector2 direction, WeaponBase weapon)
   {
        if (!CheckIfCanAttack()) return;
        canAttack = false;
        reloadTimer = 0;
        attackDirection = direction;
        if(attackDirection.x < 0)
        {
            _graphics.flipX = true;
        }
        else
        {
            _graphics.flipX = false;
        }
        if(weapon.StunOnAttack)
        {
            movement.StunForTime(weapon.AttackTime);
        }
        weapon.StartAttack(attackDirection, entity);
        inventory.ChangeAmmo(inventory.Ammo - 1);
        onAttack?.Invoke(direction);
    }
    public bool CheckIfCanAttack()
    {
        if (!canAttack) return false;
        if (inventory.Ammo < 1) return false;
        return true;
    }
    private void Update()
    {
        if (reloadTimer < GetReloadTime())
            reloadTimer += Time.deltaTime;
        if(reloadTimer >= GetReloadTime())
        {
            canAttack = true;
        }
    }
    float GetReloadTime()
    {
        var time = 1 / (entity.GetComponent<Inventory>().GetWeapon().reloadSpeed / 100);
        return time;
    }
}
