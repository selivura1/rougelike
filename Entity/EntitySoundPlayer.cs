using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EntitySoundPlayer : MonoBehaviour
{
    [SerializeField] protected AudioClip[] killSounds;
    [SerializeField] protected AudioClip[] damagedSounds;
    [SerializeField] protected AudioClip[] healedSounds;
    protected Options options;
    protected GlobalSoundSpawner globalSoundSpawner;
    protected AudioSource source;
    protected EntityBase entity;
    protected virtual void OnEnable()
    {
        globalSoundSpawner = FindObjectOfType<GlobalSoundSpawner>();
        options = FindObjectOfType<Options>();
        source = GetComponent<AudioSource>();
        entity = GetComponent<EntityBase>();
        entity.onDeath += PlayRandomKillSound;
        entity.onDamage += PlayRandomDamagedSound;
        entity.onHeal += PlayRandomHealedSound;
    }
    protected virtual void PlayRandomKillSound(EntityBase arg)
    {
        if(killSounds.Length > 0)
            globalSoundSpawner.PlaySoundEffect(killSounds[Random.Range(0, killSounds.Length)]);
        entity.onDeath -= PlayRandomKillSound;
    }
    protected virtual void PlayRandomDamagedSound(string arg)
    {
        if (damagedSounds.Length > 0)
            source.PlayOneShot(damagedSounds[Random.Range(0, damagedSounds.Length)], options.CurrentConfig.SFX);
    }
    protected virtual void PlayRandomHealedSound()
    {
        if (healedSounds.Length > 0)
            source.PlayOneShot(healedSounds[Random.Range(0, healedSounds.Length)], options.CurrentConfig.SFX);
    }
    protected virtual void OnDisable()
    {
        entity.onDamage -= PlayRandomDamagedSound;
        entity.onHeal -= PlayRandomHealedSound;
    }
}

