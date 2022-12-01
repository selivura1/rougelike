using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles = new List<Collectible>();
    public Collectible SpawnCollectible(int id, Vector3 position)
    {
        return Instantiate(_collectibles[id], position, Quaternion.identity);
    }
    public Collectible[] GetCollectibles()
    {
        return _collectibles.ToArray();
    }
}
