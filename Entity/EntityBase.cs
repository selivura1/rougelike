using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class EntityBase : MonoBehaviour, IHealth
{
    [SerializeField] public new string name;
    [Header("Stats")]
    [SerializeField] float currentHealth;
    public float MaxHealthLimit = 400;
    public EntityStats EntityStats = new EntityStats();

    [Header("Other")]
    [SerializeField] bool destroyOnDeath = true;

    protected bool invincible = false;
    public bool Dead;

    public delegate void EntityAction(EntityBase entity);
    public event EntityAction onDeath;
    public Action onDestroy;
    public Action<string> onDamage;
    public Action<EntityBase> onLethalDamage;
    public Action onHeal;
    public ReferenceContainer ReferenceContainer { get; private set; }
    public bool Enemy = true;

    public float GetHealth()
    {
        return currentHealth;
    }
    private void Awake()
    {
        ReferenceContainer = FindObjectOfType<ReferenceContainer>(); 
    }
    private void Start()
    {
        currentHealth = EntityStats.Health.Value;
    }
    public void TakeDamage(float amount)
    {
        if (invincible)
        {
            onDamage?.Invoke("Invincible");
            return;
        }
        float hp = currentHealth;
        hp -= amount;
        currentHealth = hp;
        if (currentHealth <= 0)
        {
            onLethalDamage?.Invoke(this);
            if (currentHealth <= 0)
                Terminate();
        }
        onDamage?.Invoke(amount.ToString("F0"));
    }
    public void Heal(float amount)
    {
        float hp = currentHealth;
        float maxHp = EntityStats.Health.Value;
        hp += amount;
        if (hp >= maxHp)
        {
            hp = maxHp;
        }
        currentHealth = hp;
        onHeal?.Invoke();
    }
    public virtual void Terminate()
    {
        Dead = true;      
        if(Enemy)
        {
            ReferenceContainer.PlayerSpawner.GetPlayer().onKill?.Invoke();
        }
        if(Dead && destroyOnDeath)
        {
            Destroy(gameObject);
        }
        onDeath?.Invoke(this);
    }
    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}

public interface IHealth
{
    public void TakeDamage(float damage);
    public void Heal(float value);
}


