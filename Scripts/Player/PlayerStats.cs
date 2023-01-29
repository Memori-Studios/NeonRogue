using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJAudio;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private MetaProgressionSO maxHealthSO;
    [SerializeField] private MetaProgressionSO healthRegenSO, moveSpeedSO, reloadSpeedSO, rollChargesSO;

    [Header("Meta Progression")]
    [SerializeField] private StatStructure baseHealth;
    [SerializeField] private StatStructure healthRegenRate, moveSpeed, reloadSpeed, rollCharges;
    public float BaseHealth => baseHealth.activeStat;
    public float HealthRegenRate => healthRegenRate.activeStat;
    public float MoveSpeed => moveSpeed.activeStat;
    public float ReloadSpeed => reloadSpeed.activeStat;
    public float RollCharges => rollCharges.activeStat;

    [Header("Other Stats")]
    [SerializeField] private StatStructure pickUpRadius;
    [SerializeField] private StatStructure upgradesToDraw, rateOfFire, rollChargeRate, sandevistanRechargeRate, 
        angelFireRechargeRate, expMultiplier, critChance, critDamage, projectilePiercing, armor, bonusHealth;
    public float PickUpRadius => pickUpRadius.activeStat;
    public float UpgradesToDraw => upgradesToDraw.activeStat;
    public float RateOfFire => rateOfFire.activeStat;
    public float RollChargeRate => rollChargeRate.activeStat;
    public float SandevistanRechargeRate => sandevistanRechargeRate.activeStat;
    public float AngelFireRechargeRate => angelFireRechargeRate.activeStat;
    public float ExpMultiplier => expMultiplier.activeStat;
    public float CritChance => critChance.activeStat;
    public float CritDamage => critDamage.activeStat;
    public float ProjectilePiercing => projectilePiercing.activeStat;
    public float Armor => armor.activeStat;
    public float BonusHealth => bonusHealth.activeStat;

    [Header("Exp")]
    [SerializeField] private float exp;
    public float Exp => exp;
    public float currentExp;
    public ExpLevel expLevel;

    [SerializeField] private PlayerUI playerUI;

    [Header("Random Stats")]
    public float WeaponSlots = 1;
    public float healthRegenSpeedMultiplier = 1f;
    PlayerManager playerManager;
    UIManager uIManager;
    List<StatStructure> statStructures = new List<StatStructure>();

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        uIManager = PlayerManager.instance.uIManager;

        List<MetaProgressionContainer> metaProgressionContainers = GameManager.instance.metaProgressionManager.LoadMetaProgression();
        ApplyMetaProgressionStats(metaProgressionContainers);

        statStructures.Add(baseHealth);
        statStructures.Add(healthRegenRate);
        statStructures.Add(moveSpeed);
        statStructures.Add(reloadSpeed);
        statStructures.Add(rollCharges);

        statStructures.Add(pickUpRadius);
        statStructures.Add(upgradesToDraw);
        statStructures.Add(rateOfFire);
        statStructures.Add(upgradesToDraw);
        statStructures.Add(rateOfFire);
        statStructures.Add(rollChargeRate);
        statStructures.Add(sandevistanRechargeRate);
        statStructures.Add(angelFireRechargeRate);
        statStructures.Add(expMultiplier);
        statStructures.Add(critChance);
        statStructures.Add(critDamage);
        statStructures.Add(projectilePiercing);
        statStructures.Add(armor);
        statStructures.Add(bonusHealth);

        SetUpStatStructures();
        uIManager.CreateRollCharges();
        uIManager.PauseForUpgrade();
    }
    private void SetUpStatStructures()
    {
        foreach(StatStructure statStructure in statStructures)
            statStructure.SetUp();
    }
    private void ApplyMetaProgressionStats(List<MetaProgressionContainer> metaProgressionContainers)
    {
        foreach(MetaProgressionContainer metaProgressionContainer in metaProgressionContainers)
        {
            if(metaProgressionContainer.metaProgressionSO == maxHealthSO)
            {
                // Debug.Log($"found max health {metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO)}");
                baseHealth.baseStat = metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO);
                // maxHealth.activeStat = maxHealth.baseStat;
            }
            else if(metaProgressionContainer.metaProgressionSO == healthRegenSO)
            {
                // Debug.Log($"found health regen {metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO)}");
                healthRegenRate.modifierStat = metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO);
                // healthRegen.activeStat = healthRegen.baseStat;
            }
            else if(metaProgressionContainer.metaProgressionSO == moveSpeedSO)
            {
                // Debug.Log($"found move speed {metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO)}");
                moveSpeed.modifierStat = metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO);
                // moveSpeed.activeStat = moveSpeed.baseStat;
            }
            else if(metaProgressionContainer.metaProgressionSO == reloadSpeedSO)
            {
                // Debug.Log($"found reload speed {metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO)}");
                reloadSpeed.modifierStat = metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO);
                // reloadSpeed.activeStat = reloadSpeed.baseStat;
            }
            else if(metaProgressionContainer.metaProgressionSO == rollChargesSO)
            {
                // Debug.Log($"found roll charges {metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO)}");
                rollCharges.modifierStat = metaProgressionContainer.GetHighestUnlockedLevel(metaProgressionContainer.metaProgressionSO);
            }
        }
    }
    public void ApplyUpgrade(ActiveUpgrades activeUpgrade)
    {
        foreach(StatStructure statStructure in statStructures)
        {
            if(statStructure.upgrade == activeUpgrade.upgrade)
            {
                statStructure.Modify(activeUpgrade);
                break;
            }
        }
        uIManager.UnpauseAfterUpgrade();
    }
    public void GainExp(float amount)
    {
        exp += amount;
        playerUI.DisplayExp();

        if(exp>=expLevel.maxExp)
        {
            LevelUp();
            playerUI.UpdateExpBar();
        }
    }
    private void LevelUp()
    {
        expLevel.levelNumber++;
        expLevel.minExp = expLevel.maxExp;
        expLevel.maxExp += (expLevel.levelExpIncrease * expLevel.levelNumber);
        uIManager.PauseForUpgrade();
        IAudioRequester.instance.PlaySFX("levelUp");
    }
}
[System.Serializable] public struct ExpLevel
{
    public float levelNumber;
    public float minExp, maxExp;
    public float levelExpIncrease;
}
[System.Serializable] public class ActiveUpgrades
{
    public Upgrade upgrade;
    public int level;
}

[System.Serializable] public class StatStructure
{
    public Upgrade upgrade;
    public float baseStat = 1;
    public float activeStat = 1;
    public float modifierStat = 1;

    public void Modify(ActiveUpgrades upgradeStructure)
    {
        if(upgradeStructure.upgrade.upgradeType == Upgrade.UpgradeType.weapon)
        {

        }
        else if(upgradeStructure.upgrade.upgradeType == Upgrade.UpgradeType.stat)
        {
            modifierStat = upgradeStructure.upgrade.upgradlevels[upgradeStructure.level].upgradeModifier;
            activeStat = baseStat * modifierStat;
        }
    }
    public void SetUp()
    {
        activeStat = baseStat * modifierStat;
    }
}
