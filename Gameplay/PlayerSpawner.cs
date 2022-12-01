using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawner : MonoBehaviour
{
    PlayerEntity player;
    [SerializeField] GameObject playerPref;
    private HUDActivator HUDActivator;
    //[SerializeField] float gameOverDelay = 2;
    private void Awake()
    {
        HUDActivator = FindObjectOfType<HUDActivator>();
    }
    private void Start()
    {
        SpawnPlayer();
    }
    public PlayerEntity GetPlayer()
    {
        if (player == null)
        {
            return SpawnPlayer();
        }
        else
        return player;
    }
    PlayerEntity SpawnPlayer()
    {
        if (player != null) return player;
        player = Instantiate(playerPref).GetComponent<PlayerEntity>();
        player.onDeath += GameOverInvoker;
        return player;
    }
    public void GameOverInvoker(EntityBase fix)
    {
        //Invoke(nameof(GameOver), gameOverDelay);
        GameOver();
    }
    private void GameOver()
    {
        var ents = FindObjectsOfType<EntityBase>();
        foreach (var item in ents)
        {
            item.gameObject.SetActive(false);
        }
        HUDActivator.SetActiveHUD(false);
        HUDActivator.ShowGameOverUI();
        player.onDeath -= GameOverInvoker;
    }
    public void SetPlayerControls(bool value)
    {
        player.GetComponent<PlayerControl>().enabled = value;
    }
   
}
