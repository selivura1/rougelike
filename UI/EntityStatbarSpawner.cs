using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatbarSpawner : MonoBehaviour
{
    [SerializeField] StatBar _hpBar;
    Transform HUDTransform;
    void GetReferences()
    {
        HUDTransform = ReferenceContainer.HUDActivator.GetHUD().transform;
    }
    public StatBar SpawnHPBar(EntityBase target)
    {
        GetReferences();
        var spawned = Instantiate(_hpBar, HUDTransform);
        spawned.SetOwner(target);
        return spawned;
    }
}
