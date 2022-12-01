using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TooltipTrigger))]
[ExecuteInEditMode]
public class EntityTooltip : MonoBehaviour
{
    private TooltipTrigger tooltipTrigger;
    [SerializeField] EntityBase entity;
    [SerializeField] bool isPlayer;
    string playerName;
    private void Awake()
    {
        entity = GetComponent<EntityBase>();
        tooltipTrigger = GetComponent<TooltipTrigger>();
        playerName = System.Environment.UserName;
        entity.onDeath += OnDeath;
    }

    private void OnDeath(EntityBase dead)
    {
        tooltipTrigger.OnMouseExit();
        entity.onDeath -= OnDeath;
    }

    private void Update()
    {
        tooltipTrigger.header = entity.name;
        if(isPlayer)
            tooltipTrigger.header = playerName;
        tooltipTrigger.content = entity.GetHealth().ToString("F0") + "/" + entity.EntityStats.Health.Value.ToString("F0");
    }
}
