using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpUI : MonoBehaviour
{
    UpgradeSelect upgradeSelect;
    [SerializeField] UpgradeSlotUI upgradeSlotPref;
    [SerializeField] Transform grid;
    List<UpgradeSlotUI> spawnedSlots = new List<UpgradeSlotUI>();
    PlayerLevels playerLevels;
    [SerializeField] int slotAmount = 3;
    int points = 0;
    bool levelingUp = false;
    private void Start()
    {
        upgradeSelect = FindObjectOfType<UpgradeSelect>();
        playerLevels = ReferenceContainer.PlayerSpawner.GetPlayer().GetComponent<PlayerLevels>();
        playerLevels.onLevelUp += OnLevelUp;
    }
    private void OnDestroy()
    {
        playerLevels.onLevelUp -= OnLevelUp;
    }
    public void Refresh()
    {
        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            Destroy(spawnedSlots[i].gameObject);
        }
        spawnedSlots.Clear();
        for (int i = 0; i < upgradeSelect.Slots.Count; i++)
        {
            var slot = upgradeSelect.Slots[i];
            var spawned = Instantiate(upgradeSlotPref, grid);
            spawned.Setup(i);
            spawnedSlots.Add(spawned);
        }
    }
    public void UsePoint()
    {
        points--;
        levelingUp = false;
        if (points > 0)
        {
            StartLevelUp();
        }
        else
        {
            ReferenceContainer.HUDActivator.SetActiveLevelUp(false);
        }
    }
    public void OnLevelUp()
    {
        points++;
        StartLevelUp();
    }
    void StartLevelUp()
    {
        if (levelingUp)
            return;
        ReferenceContainer.HUDActivator.SetActiveLevelUp(true);
        upgradeSelect.SetRandomSlots(slotAmount);
        Refresh();
        levelingUp = true;
    }
}
