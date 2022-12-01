using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] PopUp damagePopup;
    private Transform HUDTransform;
    void GetReferences()
    {
        HUDTransform = ReferenceContainer.HUDActivator.GetHUD().transform;
    }
    public void SpawnPopUp(Vector3 position, string text, Color color)
    {
        GetReferences();
        var spawned = Instantiate(damagePopup, HUDTransform);
        spawned.Initialize(text, color, position);
        spawned.transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
