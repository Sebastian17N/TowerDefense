using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{   
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;
    public bool IsUnlocked { get; set; }

    private int CurrentProgres;
    public void  AddProgress(int amount)
    {
        CurrentProgres += amount;
        AchievementManager.OnProgressUpdated?.Invoke(this);
        CheckUnlockStatus();
    }
    private void CheckUnlockStatus()
    {
        if(CurrentProgres >= ProgressToUnlock)
        {
            UnlockAchievement();
        }
    }
    private void UnlockAchievement()
    {
        IsUnlocked = true;
        AchievementManager.OnAchievementUnlocked?.Invoke(this);
    }
    public string GetProgress()
    {
        return $"{CurrentProgres} / {ProgressToUnlock}";
    }
    public string GetProgressComplited()
    {
        return $"{ProgressToUnlock} / {ProgressToUnlock}";
    }
    private void OnEnable()
    {
        IsUnlocked = false;
        CurrentProgres = 0;
    }
}
