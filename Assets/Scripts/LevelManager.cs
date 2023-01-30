using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;
    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }
    private void ReducedLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }
    public void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
    }
    private void WaveComplited()
    {
        CurrentWave++;
        AchievementManager.Instance.AddProgress("Waves10", 1);
        AchievementManager.Instance.AddProgress("Waves20", 1);
        AchievementManager.Instance.AddProgress("Waves50", 1);
        AchievementManager.Instance.AddProgress("Waves100", 1);
    }
    private void OnEnable()
    {
        Enemy.OnEndReached += ReducedLives;
        Spawner.OnWaveComplete += WaveComplited;
    }
    private void OnDisable()
    {
        Enemy.OnEndReached -= ReducedLives;
        Spawner.OnWaveComplete -= WaveComplited;
    }
}
