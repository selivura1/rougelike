using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : EntitySoundPlayer
{
    [SerializeField] AudioClip[] levelUpSounds;
    private PlayerLevels levels;
    protected override void OnEnable()
    {
        levels = GetComponent<PlayerLevels>();
        base.OnEnable();
        levels.onLevelUp += OnLevelUp;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        levels.onLevelUp -= OnLevelUp;
    }
    protected override void PlayRandomKillSound(EntityBase arg)
    {
        if(killSounds.Length > 0)
            globalSoundSpawner.PlaySoundEffect(killSounds[Random.Range(0, killSounds.Length)], 1 ,1);
    }
    protected void OnLevelUp()
    {
        if (levelUpSounds.Length > 0)
            source.PlayOneShot(levelUpSounds[Random.Range(0, levelUpSounds.Length)]);
    }
}
