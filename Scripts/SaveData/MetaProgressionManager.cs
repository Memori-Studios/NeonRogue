using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaProgressionManager : MonoBehaviour
{
    public List<MetaProgressionContainer> metaProgressionContainers;
    public List<PlayerBaseStats> playerBaseStats;
    public List<MetaProgressionContainer> LoadMetaProgression()
    {
        metaProgressionContainers = GameManager.instance.saveSystem.GetMetaProgression();
        return metaProgressionContainers;
    }
    public void UnlockMetaProgression(MetaProgressionSO metaProgressionSO)
    {
        for (int i = 0; i < metaProgressionContainers.Count; i++)
        {
            if(metaProgressionContainers[i].metaProgressionSO == metaProgressionSO)
            {
                for (int j = 0; j < metaProgressionContainers[i].metaLevels.Count; j++)
                {
                    if (!metaProgressionContainers[i].metaLevels[j].unlocked)
                    {
                        metaProgressionContainers[i].metaLevels[j].unlocked = true;
                        break;
                    }
                }
                break;
            }
        }
        SaveMetaProgression();
    }
    public void SaveMetaProgression()
    {
        GameManager.instance.saveSystem.SaveMetaProgression();
        Debug.Log($"Saved Meta Progression");
    }
}
[System.Serializable] public class MetaProgressionContainer
{
    public MetaProgressionSO metaProgressionSO;
    public List<MetaLevel> metaLevels;
    public float GetHighestUnlockedLevel(MetaProgressionSO metaProgressionSO)
    {
        float highestLevel = GameManager.instance.metaProgressionManager.playerBaseStats.Find(x => x.metaProgressionSO == metaProgressionSO).baseValue;
        for (int i = 0; i < metaLevels.Count; i++)
        {
            if (metaLevels[i].unlocked)
            {
                highestLevel = metaLevels[i].modificaitonAmount;
            }
        }
        if(highestLevel>=100)
            return highestLevel/100;

        return highestLevel;
    }
}
[System.Serializable] public class MetaLevel
{
    public int level;
    public float cost;
    public float modificaitonAmount;
    public bool unlocked;
}
[System.Serializable] public class PlayerBaseStats
{
    public MetaProgressionSO metaProgressionSO;
    public float baseValue;
}
