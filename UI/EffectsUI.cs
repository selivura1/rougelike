using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsUI : MonoBehaviour
{
    List<Image> _spawned = new List<Image>();
    PotionHandler _potionHandler;
    private PlayerSpawner _playerSpawner;
    private void Awake()
    {
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
        _potionHandler = _playerSpawner.GetPlayer().GetComponent<PotionHandler>();
    }
    private void Update()
    {
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < _spawned.Count; i++)
        {
            Destroy(_spawned[i].gameObject);
        }
        _spawned.Clear();
        foreach (var item in _potionHandler.PotionEffects)
        {
            var go = new GameObject().AddComponent<Image>();
            go.transform.SetParent(transform);
            go.sprite = item.sprite;
            go.transform.localScale = Vector3.one;
            _spawned.Add(go);
        }
    }
}
