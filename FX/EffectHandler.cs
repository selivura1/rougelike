using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] float _randomRotationMin = -180;
    [SerializeField] float _randomRotationMax = 180;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(_randomRotationMin, _randomRotationMax));
    }
    public void Terminate()
    {
        Destroy(gameObject);
    }
}
