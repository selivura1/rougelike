using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int CurrentScore { get; private set; } = 0;
    [SerializeField] int killScore = 100;
    [SerializeField] int stageCompleteScore = 1000;
    [SerializeField] int levelUpScore = 200;
    [SerializeField] int roomClearScore = 50;
    public static int Kills { get; private set; } = 0;
    public static int LevelUps { get; private set; } = 0;
    public static int StageComplete { get; private set; } = 0;
    public static int RoomsCleared { get; private set; } = 0;
    PlayerEntity player;
    PlayerLevels playerLevels;
    Combat playerCombat;
    DungeonGenerator dungeonGenerator;
    public static System.Action onScoreChanged;
    private void Start()
    {
        player = ReferenceContainer.PlayerSpawner.GetPlayer();
        playerLevels = player.GetComponent<PlayerLevels>();
        dungeonGenerator = ReferenceContainer.DungeonGenerator;

        player.onKill += OnKill;
        playerLevels.onLevelUp += OnlevelUp;
        dungeonGenerator.onRoomPreDestroy += RemoveRoomListener;
        dungeonGenerator.onRoomSpawned += AddRoomListener;
        dungeonGenerator.onStageCompleted += OnStageComplete;
    }
    void RemoveRoomListener()
    {
        dungeonGenerator.CurrentRoom.onRoomClear -= OnRoomClear;
    }
    void AddRoomListener()
    {
        dungeonGenerator.CurrentRoom.onRoomClear += OnRoomClear;
    }
    private void OnDestroy()
    {
        player.onKill += OnKill;
        playerLevels.onLevelUp -= OnlevelUp;
        dungeonGenerator.onRoomPreDestroy -= RemoveRoomListener;
        dungeonGenerator.onRoomSpawned -= AddRoomListener;
        dungeonGenerator.onStageCompleted -= OnStageComplete;
    }
    public void OnlevelUp()
    {
        LevelUps++;
        AddScore(levelUpScore);
    }
    public void OnStageComplete()
    {
        StageComplete++;
        AddScore(stageCompleteScore);
    }
    public void OnRoomClear()
    {
        RoomsCleared++;
        AddScore(roomClearScore);
    }
    public void OnKill()
    {
        Kills++;
        AddScore(killScore);
    }
    void AddScore(int amount)
    {
        CurrentScore += amount;
        onScoreChanged?.Invoke();
    }
    public static void ResetScore()
    {
        CurrentScore = 0;
    }
}
