using Pathfinding;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    protected Movement _movement;
    protected EntityBase _target;
    private PlayerSpawner _playerSpawner;
    protected EntityBase _entity;
    protected Seeker _seeker;
    protected Combat _combat;
    protected Path _path;
    protected Inventory _inventory;
    protected float _timer;
    [SerializeField] protected float _nextWaypointDistance = 3;
    [SerializeField] protected float _timeBtwScan;
    [SerializeField] protected float _visionRange = 10;
    [SerializeField] protected bool _reachedEndOfPath;
    [Header("Combat")]
    [SerializeField] protected float _prepareTime;
    [SerializeField] protected float _attackRange = 2f;
    [SerializeField] protected bool _preparing = false;
    protected WeaponBase _weapon;
    protected float _prepareTimer = 0;
    protected int _currentWaypoint = 0;
    protected Vector3 dir;
    protected Vector3 atkDir;
    void Awake()
    {
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        _entity = GetComponent<EntityBase>();
        _seeker = GetComponent<Seeker>();
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
        _target = _playerSpawner.GetPlayer().GetComponent<EntityBase>();
        _inventory = GetComponent<Inventory>();
        _weapon = _inventory.GetWeapon();
    }

    public void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }
    private Vector3 GetDirToPlayer()
    {
        return (_target.transform.position - _combat.RangedAttackSpawn.position).normalized ;
    }
    Vector3 GetRandomPath(float length = 2)
    {
        return transform.position + new Vector3(Random.Range(-length, length), Random.Range(-length, length));
    }
    private void FixedUpdate()
    {
        if (_timer < _timeBtwScan)
        {
            _timer += Time.fixedDeltaTime;
        }
        else
        {
            _timer = 0;
            if (_target && Vector2.Distance(transform.position, _target.transform.position) <= _visionRange)
                _seeker.StartPath(transform.position, _target.transform.position, OnPathComplete);
            else 
                _seeker.StartPath(transform.position, GetRandomPath(), OnPathComplete);
        }
        if (_path == null)
        {
            return;
        }
        _reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
            if (distanceToWaypoint < _nextWaypointDistance)
            {
                if (_currentWaypoint + 1 < _path.vectorPath.Count)
                {
                    _currentWaypoint++;
                }
                else
                {
                    _reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        if (!_target) 
            return;
        if (_preparing)
        {
            _prepareTimer += Time.deltaTime;
            if (_prepareTimer >= _prepareTime)
            {
                _prepareTimer = 0;
                _preparing = false;
                ExecuteAttack();
            }
        }
        else if (_combat.CheckIfCanAttack() && Vector3.Distance(transform.position, _target.transform.position) - 0.1f <= _attackRange)
        {
            PrepareForAttack();
        }
        if (!_preparing && Vector3.Distance(transform.position, _target.transform.position) > _attackRange)
            dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        else
            dir = Vector3.zero;
        _movement.Move(dir);
    }
    public void PrepareForAttack()
    {
        if (_preparing) return;
        _preparing = true;
        var display = _entity.GetComponent<EntityStatbar>();
        if (display)
            display.GetDisplay().AddEffect(EffectTypes.Warning, _prepareTime);
        atkDir = GetDirToPlayer();
    }
    public virtual void ExecuteAttack()
    {
        _combat.StartAttack(atkDir, _weapon);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}

