using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D _rb;
    EntityBase entity;
    Animator _anim;
    public bool CanMove { get; private set; } = true;
    private void Awake()
    {
        entity = GetComponent<EntityBase>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 dir)
    {
        if(CanMove)
            _rb.velocity = dir.normalized * entity.EntityStats.Speed.Value * Time.deltaTime;
        if (_anim)
            _anim.SetFloat("Speed", dir.magnitude);
    }
    public void StunForTime(float time)
    {
        StartCoroutine(Stun(time));
    }
    protected IEnumerator Stun(float time)
    {
        CanMove = false;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        _rb.velocity = Vector2.zero;
        CanMove = true;
    }
    public void Teleport(Vector2 pos)
    {
        transform.position = pos;
    }
}
