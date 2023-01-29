using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<Upgrade> statUpgrades = new List<Upgrade>();
    private List<Upgrade> upgradesIncludingWeapons = new List<Upgrade>();
    public List<Upgrade> weaponUpgrades = new List<Upgrade>();
    public List<Upgrade> activeCyberwareUpgrades = new List<Upgrade>();

    [Header("Cyberware Upgrades")]
    [SerializeField] private CyberwareUpgrade sandevistanUpgrade;
    [SerializeField] private CyberwareUpgrade angelFireUpgrade;

    private void Start()
    {
        foreach(Upgrade u in statUpgrades)
            upgradesIncludingWeapons.Add(u);

        foreach(Upgrade u in weaponUpgrades)
            upgradesIncludingWeapons.Add(u);
    }

    public List<Upgrade> GetRandomUpgrades(float amount)
    {
        if(PlayerManager.instance.playerStats.WeaponSlots > PlayerManager.instance.weaponController.WeaponCount)
            return AllUpgrades(amount);
        else 
            return StatUpgrades(amount);
    }
    public List<Upgrade> StatUpgrades(float amount)
    {
        List<Upgrade> selectedUpgrades = new List<Upgrade>();
        for (int i = 0; i < amount; i++)
        {
            Upgrade u = statUpgrades[Random.Range(0, statUpgrades.Count)];

            if(!selectedUpgrades.Contains(u) && SpaceAvailableForWeapon(u))
            {
                selectedUpgrades.Add(u);
            }
            else
                i--;

            if(statUpgrades.Count==i+1)
            {
                Debug.Log($"Not enough left {amount}");
                return selectedUpgrades;
            }
        }
        return selectedUpgrades;
    }
    public List<Upgrade> AllUpgrades(float amount)
    {
        List<Upgrade> selectedUpgrades = new List<Upgrade>();
        for (int i = 0; i < amount; i++)
        {
            Upgrade u = upgradesIncludingWeapons[Random.Range(0, upgradesIncludingWeapons.Count)];

            if(!selectedUpgrades.Contains(u))
            {
                selectedUpgrades.Add(u);
            }
            else
                i--;

            if(upgradesIncludingWeapons.Count==i+1)
            {
                Debug.Log($"Not enough left {amount}");
                return selectedUpgrades;
            }
        }
        return selectedUpgrades;
    }
    public List<Upgrade> GetWeaponUpgrades(float amount)
    {
        List<Upgrade> selectedUpgrades = new List<Upgrade>();
        for (int i = 0; i < amount; i++)
        {
            Upgrade u = weaponUpgrades[Random.Range(0, weaponUpgrades.Count)];

            if(!selectedUpgrades.Contains(u))
                selectedUpgrades.Add(u);
            else
                i--;

            if(weaponUpgrades.Count==i+1)
            {
                // Debug.Log($"Not enough weaponUpgrades left {amount}");
                return selectedUpgrades;
            }
        }
        return selectedUpgrades;
    }
    private bool SpaceAvailableForWeapon(Upgrade u)
    {
         if(u.upgradeType==Upgrade.UpgradeType.weapon)
           return PlayerManager.instance.playerStats.WeaponSlots > PlayerManager.instance.weaponController.WeaponCount;

        return true;
    }
    public void LoadCyberware(CyberwareUpgrade cyberwareUpgrade)
    {
        activeCyberwareUpgrades.Add(cyberwareUpgrade);
        PlayerManager.instance.uIManager.LoadCyberUpgrade(cyberwareUpgrade.cyberwareType, activeCyberwareUpgrades.Count);
        PlayerManager.instance.cyberwareManager.LoadCyberware(cyberwareUpgrade);
    }
    public bool SandevistanOnline()
    {
        return activeCyberwareUpgrades.Contains(sandevistanUpgrade);
    }
    public bool AngelFireOnline()
    {
        return activeCyberwareUpgrades.Contains(angelFireUpgrade);
    }
}
