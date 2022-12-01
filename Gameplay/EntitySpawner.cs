using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] List<EntityBase> _entityList = new List<EntityBase>();
    //public Action<EntityBase> onEntitySpawn;
    public EntityBase SpawnMob(EntityBase mob, Vector3 position)
    {
        EffectSpawner.Spawn(9, position);
        var spawned = Instantiate(mob, position, Quaternion.identity);
        //onEntitySpawn.Invoke(spawned);
        return spawned;
    }
    public  EntityBase SpawnMob(int mobInList, Vector3 position)
    {
        return Instantiate(_entityList[mobInList], position, Quaternion.identity);
    }
}
