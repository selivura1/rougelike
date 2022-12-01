using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    EntityBase entity;
    [SerializeField] EffectHandler[] onDeathEffects;
    private void Start()
    {
        entity = GetComponent<EntityBase>();
        entity.onDeath += SpawnDeathEffects;
    }
    private void OnDestroy()
    {
        entity.onDeath -= SpawnDeathEffects;
    }
    public void SpawnEffect(EffectHandler effect)
    {
        EffectSpawner.Spawn(effect, transform.position);
    }
    public void SpawnDeathEffects(EntityBase fix)
    {
        foreach (var item in onDeathEffects)
        {
            EffectSpawner.Spawn(item, transform.position);
        }
    }
}
