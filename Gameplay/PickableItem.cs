using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] Item _item;
    public void Setup(Item item)
    {
        _item = item;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inv = collision.GetComponent<Inventory>();
        if(inv)
        {
            bool sucess = false;
            if(_item.canBeActive)
            {
                ReferenceContainer.ItemSpawner.SpawnItem(inv.GetActiveItem(), transform.position);
            }
            else 
            if (inv.GetInventory().Count > 4)
            {
                sucess = false;
            }
            if (inv.GetComponent<PlayerControl>())
            {
                inv.Give(_item);
                sucess = true;
            }
            if(sucess)
            {
                Terminate();
            }
        }
    }
    public void Terminate()
    {
        Destroy(gameObject);
    }
}
