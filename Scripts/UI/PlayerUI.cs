using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar, expBar, reloadBar;
    public TMP_Text expLevelNumber;
    public Transform upgradeBarTarget;
    PlayerStats playerStats;
    UpgradeManager upgradeManager;
    private void Start()
    {
        upgradeBarTarget = PlayerManager.instance.transform;
        playerStats = PlayerManager.instance.playerStats;
        upgradeManager = PlayerManager.instance.upgradeManager;

        healthBar.value = PlayerManager.instance.Health;
        UpdateExpBar();
    }
    private void Update()
    {
        ShowOnUI(upgradeBarTarget);
        healthBar.value = PlayerManager.instance.Health;
        healthBar.maxValue = PlayerManager.instance.startingHealth * playerStats.BonusHealth/100;
    }
    public void ShowOnUI(Transform target)
    {
        if(target==null)
            return;
            
        Vector3 pos = Camera.main.WorldToScreenPoint(target.position);
        this.transform.position = pos;
    }
    public void DisplayExp()
    {
        expBar.value = playerStats.Exp;
    }
    public void UpdateExpBar()
    {
        expLevelNumber.text = playerStats.expLevel.levelNumber.ToString();
        expBar.minValue = playerStats.expLevel.minExp;
        expBar.maxValue = playerStats.expLevel.maxExp;
        expBar.value = playerStats.Exp;
    }
    
}