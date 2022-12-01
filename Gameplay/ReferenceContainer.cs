using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceContainer : MonoBehaviour
{
    public static GlobalSoundSpawner SoundSpawner { get; private set; }
    public static EntitySpawner EntitySpawner { get; private set; }
    public static HUDActivator HUDActivator { get; private set; }
    public static DungeonGenerator DungeonGenerator { get; private set; }
    public static ItemSpawner ItemSpawner { get; private set; }
    public static CollectibleSpawner CollectibleSpawner { get; private set; }
    public static EntityStatbarSpawner EntityDisplaySpawner { get; private set; }
    public static PopUpSpawner PopUpSpawner { get; internal set; }
    public static PlayerSpawner PlayerSpawner { get; internal set; }
    public static Database Database { get; internal set; }
    public static Options Options { get; internal set; }
    public static EffectSpawner EffectSpawner { get; internal set; }

    private void Awake()
    {
        SoundSpawner = FindObjectOfType<GlobalSoundSpawner>();
        EntitySpawner = FindObjectOfType<EntitySpawner>();
        HUDActivator = FindObjectOfType<HUDActivator>();
        DungeonGenerator = FindObjectOfType<DungeonGenerator>();
        ItemSpawner = FindObjectOfType<ItemSpawner>();
        CollectibleSpawner = FindObjectOfType<CollectibleSpawner>();
        EntityDisplaySpawner = FindObjectOfType<EntityStatbarSpawner>();
        PopUpSpawner = FindObjectOfType<PopUpSpawner>();
        PlayerSpawner = FindObjectOfType<PlayerSpawner>();
        Database = FindObjectOfType<Database>();
        Options = FindObjectOfType<Options>();
        EffectSpawner = FindObjectOfType<EffectSpawner>();
    }
}
