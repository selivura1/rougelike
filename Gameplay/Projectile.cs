using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Projectile : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected bool _initialized;
    protected float _damage;
    [SerializeField] protected float _speed;
    protected EntityBase _caster;
    [SerializeField] EffectHandler _effect;
    public UnityEvent OnTerminated;
    protected bool terminated = false;
    [SerializeField] protected bool _destroyOnFirstTarget = true;
    [SerializeField] protected bool _enemy = false;
    protected Vector3 dir;
    [SerializeField ]protected float delay = 0;
    bool delayEnd;
    [Tooltip("Percentage from start damage for each next target")]public float areaDamageForNextTarget = 50;
    List<EntityBase> damaged = new List<EntityBase>();
    new SpriteRenderer renderer;
    public bool RandomFlipX = false;
    public bool RandomFlipY = false;
    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!_initialized && !delayEnd) return;
        var entity = other.GetComponent<EntityBase>();
        if (entity)
        {
            if (entity == _caster || damaged.Contains(entity)) return;
            if (_enemy && entity.tag != "Player") return;
            entity.TakeDamage(_damage);
            damaged.Add(entity);
            _damage *= areaDamageForNextTarget / 100;
        }
        if (_destroyOnFirstTarget)
            Terminate();
    }
    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Initialize(EntityBase caster,  Vector2 direction, float damage, float speed, float lifetime)
    {
        if(RandomFlipX)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                renderer.flipX = true;
            }
            else renderer.flipX = false;
        }
        if (RandomFlipY)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                renderer.flipY = true;
            }
            else renderer.flipY = false;
        }
        _initialized = true;
        _damage = damage;
        _caster = caster;
        _speed = speed;
        dir = direction;
        transform.right = dir;
        Invoke(nameof(Terminate), lifetime);
        Invoke(nameof(EndDelay), delay);
    }
    
    protected void FixedUpdate()
    {
        _rb.velocity = transform.right * _speed;
    }
    public virtual void Terminate()
    {
        terminated = true;
        if(_effect)
        EffectSpawner.Spawn(_effect, transform.position);
        OnTerminated?.Invoke();
        Destroy(gameObject);
    }
    protected void EndDelay()
    {
        delayEnd = true;
    }
}
