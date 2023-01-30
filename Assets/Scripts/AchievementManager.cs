using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public static Action<Achievement> OnAchievementUnlocked;
    public static Action<Achievement> OnProgressUpdated;
    [SerializeField] private AchievementCard achievementCardPrefab;
    [SerializeField] private Transform achievementPanelContainer;
    [SerializeField] private Achievement[] achievement;

    private void Start()
    {
        LoadAchievement();
    }
    private void LoadAchievement()
    {
        for (int i = 0; i < achievement.Length; i++)
        {
            AchievementCard card = Instantiate(achievementCardPrefab, achievementPanelContainer);
            card.SetUpAchievement(achievement[i]);  
        }
    }
    public void AddProgress(string achievementID, int amount)
    {
        Achievement achievementWhanted = AchievementExists(achievementID);
        if (achievementWhanted != null)
        {
            achievementWhanted.AddProgress(amount);
        }
    }
    private Achievement AchievementExists(string achievementID)
    {
        for (int i = 0; i < achievement.Length; i++)
        {
            if (achievement[i].ID == achievementID)
            {
                return achievement[i];  
            }
        }

        return null;
    }

}
