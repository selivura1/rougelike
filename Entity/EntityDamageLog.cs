using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageLog : MonoBehaviour
{
    EntityBase entity;
    private void Start()
    {
        entity = GetComponent<EntityBase>();
        entity.onDamage += SpawnPop;
    }
    private void SpawnPop(string text)
    {
        ReferenceContainer.PopUpSpawner.SpawnPopUp(transform.position, text, Color.white);
    }
}
