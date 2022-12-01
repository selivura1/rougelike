using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevels : MonoBehaviour
{
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; } = 0;
    public int ExperienceToNextLevel { get; private set; } = 5;
    [SerializeField] int scaling = 5;
    [SerializeField] int startExpToNextLevel = 5;
    public Action onLevelUp;
    public Action onExperienceChange;
    private void Awake()
    {
        ExperienceToNextLevel = startExpToNextLevel;
    }
    private void Update()
    {
#if UNITY_EDITOR
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                AddExperience(1);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                AddExperience(10);
            }
        }
#endif
    }
    public void AddExperience(int amount)
    {
        Experience += amount;
        CheckForLevelUp();
        onExperienceChange?.Invoke();
    }
    private void CheckForLevelUp()
    {
        if(Experience >= ExperienceToNextLevel)
        {
            Experience = Experience - ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel += Level * scaling;
            onLevelUp?.Invoke();
        }
    }
}
