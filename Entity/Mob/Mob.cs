using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : EntityBase
{
    [SerializeField] int SoulDrop = 1;
    public override void Terminate()
    {
        base.Terminate();
        float chance = ReferenceContainer.PlayerSpawner.GetPlayer().EntityStats.CollectibleChance.Value;
        if (Random.Range(0, 100) <= chance)
            ReferenceContainer.CollectibleSpawner.SpawnCollectible(0, transform.position);
        if(SoulDrop > 0)
        {
            for (int i = 0; i < SoulDrop; i++)
            {
                ReferenceContainer.CollectibleSpawner.SpawnCollectible
                    (1, transform.position 
                        + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f))
                    );
            }
        }
    }
}
