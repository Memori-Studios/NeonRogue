using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJ_Steamworks;

public class SteamAchievementManager : MonoBehaviour
{
    public static SteamAchievementManager instance;
    public UpgradeAchievement[] upgradeAchievements;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void CheckForUpgradeAchievement(Upgrade newUpgrade)
    {
        foreach(UpgradeAchievement upgradeAchievement in upgradeAchievements)
        {
            if(upgradeAchievement.upgrade == newUpgrade)
                UnlockAchievement(upgradeAchievement.achievement);
        }
    }
    public void UnlockAchievement(SteamAchievement achievement)
    {
        if(!SteamStatic.IsThisAchievementUnlocked(achievement.achievementId))
            SteamStatic.UnlockAchievement(achievement.achievementId);
    }
}
[System.Serializable] public struct UpgradeAchievement
{
    public Upgrade upgrade;
    public SteamAchievement achievement;
}