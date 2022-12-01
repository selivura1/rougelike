using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform PlayerRespawn;
    List<EntityBase> spawnedMobs = new List<EntityBase>();
    [SerializeField] RoomExit _exit;
    private bool clear;
    public Action onRoomClear;
    public void Initialize()
    {
        SpawnRoomMobs();
        CheckIfRoomClear();
    }
    public void SpawnRoomMobs()
    {
        MobSpawnPoint[] mobSpawnPoints = FindObjectsOfType<MobSpawnPoint>();
        for (int i = 0; i < mobSpawnPoints.Length; i++)
        {
            spawnedMobs.Add(ReferenceContainer.EntitySpawner.SpawnMob(mobSpawnPoints[i].Mob, mobSpawnPoints[i].transform.position));
        }
        foreach (var mob in spawnedMobs)
        {
            mob.onDeath += RemoveDeadMob;
        }
        foreach (var spawnPoint in mobSpawnPoints)
        {
            spawnPoint.gameObject.SetActive(false);
        }
    }
    private void RemoveDeadMob(EntityBase deadMob)
    {
        spawnedMobs.Remove(deadMob);
        deadMob.onDeath -= RemoveDeadMob;
        CheckIfRoomClear();
    }
    bool CheckIfRoomClear()
    {
        if (spawnedMobs.Count <= 0)
        {
            ClearRoom();
            return true;
        }
        else if(clear != false)
        {
            LockRoom();
        }
        return false;
    }

    public void ClearRoom()
    {
        clear = true;
        _exit.Activate();
        onRoomClear?.Invoke();
    }
    public void LockRoom()
    {
        clear = false;
        _exit.Deactivate();
    }
}
