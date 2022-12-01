using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] Item _itemToSpawn;
    public void Start()
    {
        var database = FindObjectOfType<Database>();
        if(_itemToSpawn)
        {
            ReferenceContainer.ItemSpawner.SpawnItem(_itemToSpawn,transform);
        }
        else
        {
            ReferenceContainer.ItemSpawner.SpawnItem(database.GetAllItems()[Random.Range(0, database.GetAllItems().Count)], transform);
        }
    }
}
