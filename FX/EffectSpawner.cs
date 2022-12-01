using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public static EffectSpawner inst;
    [SerializeField] List<EffectHandler> _effects = new List<EffectHandler>();
    private void Awake()
    {
        inst = this;
    }
    public static void Spawn(EffectHandler effect, Vector3 position, float lifetime = 5)
    {
        Destroy(Instantiate(effect, position, Quaternion.identity).gameObject, lifetime);
    }
    public static void Spawn(int ID, Vector3 position, float lifetime = 5)
    {
        Destroy(Instantiate(inst._effects[ID], position, Quaternion.identity).gameObject, lifetime);
    }
    public static void Spawn(int ID, Transform parent, float lifetime = 5)
    {
        Destroy(Instantiate(inst._effects[ID], parent).gameObject, lifetime);
    }
}
