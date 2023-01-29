using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    string filename = "SaveData";
    public List<WeaponType> weapons;

    public WeaponUnlocks GetWeaponUnlock(WeaponType weapon)
    {
        SaveData saveData = GetSaveDataFromJson();
        WeaponUnlocks weaponUnlock = saveData.weaponUnlocks.Find(x => x.weapon == weapon);
        return weaponUnlock;
    }
    public void UnlockWeaponModification(WeaponUnlocks newWeaponUnlock)
    {
        SaveData saveData = GetSaveDataFromJson();
        saveData.UpdateWeaponModification(newWeaponUnlock);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
        Debug.Log($"saved weapon modification");
    }
    public List<MetaProgressionContainer> GetMetaProgression()
    {
        SaveData saveData = GetSaveDataFromJson();
        return saveData.LoadMetaProgression();
    }
    public void RecordRecentGame(int rewardAmount, float timeSurvived, int enemiesKilled, int level)
    {
        SaveData saveData = GetSaveDataFromJson();
        saveData.recentGame = new RecentGame(rewardAmount, timeSurvived, enemiesKilled, level);
        saveData.SaveEddies(rewardAmount);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
    }
    public RecentGame GetRecentGame()
    {
        SaveData saveData = GetSaveDataFromJson();
        return saveData.recentGame;
    }

    public void SaveMetaProgression()
    {
        SaveData saveData = GetSaveDataFromJson();
        saveData.SaveMetaProgression(GameManager.instance.metaProgressionManager.metaProgressionContainers);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
    }
    private SaveData GetSaveDataFromJson()
    {
        SaveData saveData = JSONFileHandler.ReadListFromJSON<SaveData>(filename);
        if(saveData==null)
            saveData = new SaveData(weapons, GameManager.instance.metaProgressionManager.metaProgressionContainers);
        
        return saveData;
    }
    public void WipeSaveData()
    {
        SaveData saveData = new SaveData(weapons, GameManager.instance.metaProgressionManager.metaProgressionContainers);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
    }
    public void AddResources()
    {
        SaveData saveData = GetSaveDataFromJson();
        saveData.SaveEddies(9999);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
        Debug.Log($"saved resources");
    }
    public int GetCurrency()
    {
        SaveData saveData = GetSaveDataFromJson();
        return saveData.currency;
    }
    public void AddCurrency(int amount)
    {
        SaveData saveData = GetSaveDataFromJson();
        saveData.SaveEddies(amount);
        JSONFileHandler.SaveToJSON<SaveData> (saveData, filename);
        Debug.Log($"saved currency");
    }
}
[System.Serializable] public class SaveData
{
    public int currency;
    public List<WeaponUnlocks> weaponUnlocks = new List<WeaponUnlocks>();
    public List<MetaProgressionContainer> metaProgressionContainers = new List<MetaProgressionContainer>();
    public RecentGame recentGame;
    public SaveData(List<WeaponType> weapons, List<MetaProgressionContainer> _metaProgressionContainers)
    {
        // for (int i = 0; i < weapons.Count; i++)
        //     weaponUnlocks.Add(new WeaponUnlocks(weapons[i]));
        for (int i = 0; i < _metaProgressionContainers.Count; i++)
        {
            for (int j = 0; j < _metaProgressionContainers[i].metaLevels.Count; j++)
            {
                _metaProgressionContainers[i].metaLevels[j].unlocked = false;
            }
        }

        this.metaProgressionContainers = _metaProgressionContainers;
    }
    public void SaveEddies(int amount)
    {
        currency+=amount;
    }
    public void UpdateWeaponModification(WeaponUnlocks newWeaponUnlock)
    {
        for (int i = 0; i < weaponUnlocks.Count; i++)
        {
            if(weaponUnlocks[i].weapon==newWeaponUnlock.weapon)
            {
                weaponUnlocks[i].upgradeLevels = newWeaponUnlock.upgradeLevels;
            }
        }
    }
    public void SaveMetaProgression(List<MetaProgressionContainer> metaProgressionContainers)
    {
        this.metaProgressionContainers = metaProgressionContainers;
    }
    public List<MetaProgressionContainer> LoadMetaProgression()
    {
        return this.metaProgressionContainers;
    }
}
[System.Serializable] public class WeaponUnlocks
{
    public WeaponType weapon;
    public List<int> upgradeLevels = new List<int>(4);
}
[System.Serializable] public struct RecentGame
{
    public int rewardAmount;
    public float timeSurvived;
    public int enemiesKilled;
    public int playerLevel;
    public RecentGame(int rewardAmount, float timeSurvived, int enemiesKilled, int playerLevel)
    {
        this.rewardAmount = rewardAmount;
        this.timeSurvived = timeSurvived;
        this.enemiesKilled = enemiesKilled;
        this.playerLevel = playerLevel;
    }
}

