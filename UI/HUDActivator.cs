using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDActivator : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject stageClearUI;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gunSelect;
    [SerializeField] GameObject levelUpUI;
    private PlayerSpawner _playerSpawner;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuUI();
        }
    }
    private void Start()
    {
        gameOverUI.SetActive(false);
        gunSelect.SetActive(false);
        levelUpUI.SetActive(false);
        _playerSpawner = FindObjectOfType<PlayerSpawner>();
    }
    public void SetActiveHUD(bool value)
    {
        hud.SetActive(value);
    }
    public void SetActiveLevelUp(bool value)
    {
        levelUpUI.SetActive(value);
    }
    public  void SetActiveTutorial(bool value)
    {
        PauseControls(value);
        tutorialUI.SetActive(value);
    }
    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
    public void ShowStageClearUI()
    {
        stageClearUI.SetActive(true);
    }
    public GameObject GetHUD()
    {
        return hud;
    }
    public GameObject GetGunSelect()
    {
        return gunSelect;
    }
    public void SetActiveMenuUI(bool value)
    {
        PauseControls(value);
        menuUI.SetActive(value);
    }
    public void ToggleMenuUI()
    {
        SetActiveMenuUI(!menuUI.activeSelf);
    }
    public void SetActiveGunSelect(bool value)
    {
        PauseControls(value);
        gunSelect.SetActive(value);
    }
    void PauseControls(bool value)
    {
        TimeControl.SetPause(value);
        _playerSpawner.SetPlayerControls(!value);
    }
}
