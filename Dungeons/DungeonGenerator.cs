using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] Dungeon _dungeon;
    [SerializeField] AstarPath _pathfinder;
    public Room CurrentRoom { get; private set; }
    Movement playerMovement;
    PlayerSpawner playerSpawner;
    int roomNumber = -1;
    int loop = 0;
    public Action onRoomSpawned;
    public Action onRoomPreDestroy;
    public Action onStageCompleted;
    public void CreateDungeon()
    {
        roomNumber = -1;
        SpawnRandomRoom(_dungeon.StartRooms);
        //Log.WriteInLog("Welcome to the PELMENI dungeon!");
    }
    public void Loop()
    {
        onStageCompleted?.Invoke();
        roomNumber = -1;
        SpawnRandomRoom(_dungeon.StartRooms);
        loop++;
    }
    private void Start()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        playerMovement = playerSpawner.GetPlayer().GetComponent<Movement>();
        CreateDungeon();
    }
    private void SpawnRandomRoom(Room[] pool)
    {
        SpawnRoom(pool[UnityEngine.Random.Range(0, pool.Length)]);
    }
    private void SpawnRoom(Room room)
    {
        if (CurrentRoom)
        {
            onRoomPreDestroy?.Invoke();
            Destroy(CurrentRoom.gameObject);
        }
        CurrentRoom = Instantiate(room, transform);
        playerMovement.Teleport(CurrentRoom.PlayerRespawn.position);
        _pathfinder.Scan();
        if(roomNumber == 0 && loop == 0)
        {
            ActDisplayerUI.Show(_dungeon.name, _dungeon.desc);
        }
        CurrentRoom.Initialize();
        roomNumber++;
        onRoomSpawned?.Invoke();
    }
    public void ProceedToNextRoom()
    {
        if(roomNumber == _dungeon.RoomsToBoss)
        {
            SpawnRandomRoom(_dungeon.BossRooms);
            return;
        }
        //if(roomNumber == _dungeon.RoomsToTreasure)
        //{
        //    SpawnRandomRoom(_dungeon.TreasureRooms);
        //    return;
        //}
        SpawnRandomRoom(_dungeon.Rooms);
    }
}
