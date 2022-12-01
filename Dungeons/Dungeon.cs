using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Dungeon : ScriptableObject
{
    public new string name;
    public string desc;
    public Room[] Rooms;
    //public Room[] TreasureRooms;
    public Room[] BossRooms;
    public Room[] StartRooms;
    public int RoomsToBoss = 5;
    //public int RoomsToTreasure = 4;
}
