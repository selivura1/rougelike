using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollectible : Collectible
{
    public override void OnCollected()
    {
        _target.GetComponent<PlayerLevels>().AddExperience((int)_value);
        Destroy(gameObject, 0.1f);
    }
}
