using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatbar : MonoBehaviour
{
    StatBar display;
    EntityBase entity;
    private void Start()
    {
        entity = GetComponent<EntityBase>();
        display = ReferenceContainer.EntityDisplaySpawner.SpawnHPBar(entity);
        entity.onDestroy += RemoveDisplay;
    }
    private void RemoveDisplay()
    {
        entity.onDestroy -= RemoveDisplay;
        if(display)
        Destroy(display.gameObject);
    }
    public StatBar GetDisplay()
    {
        return display;
    }
}
