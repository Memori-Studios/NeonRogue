using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    [SerializeField] private List<WeaponUI> weaponsUIs = new List<WeaponUI>();
    [SerializeField] private List<BaseWeaponStats> possibleWeapons;
    public Dictionary<WeaponType, List<WeaponLevel>> weaponDictionary = new Dictionary<WeaponType, List<WeaponLevel>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        foreach (BaseWeaponStats item in possibleWeapons)
            weaponDictionary.Add(item.weaponType, item.weaponLevels);

        possibleWeapons = null;
    }
    public void LoadWeaponUI(Weapon weaponToEquip)
    {
        weaponsUIs[PlayerManager.instance.weaponController.WeaponCount-1].LoadWeaponUI(weaponToEquip);
    }
    public void LoadWeaponsUnlocked()
    {
        for (int i = 0; i < PlayerManager.instance.playerStats.WeaponSlots; i++)
        {
            weaponsUIs[i].UnlockUI();
        }
    }
}


[System.Serializable] public struct BaseWeaponStats
{
    public WeaponType weaponType;
    public List<WeaponLevel> weaponLevels;
}
[System.Serializable] public struct WeaponLevel
{
    public float gunRateOfFire;
    public float muzzleVelocity;
    public int projectilePiercing;
    public float projectileVariance;
    public float damage;
    public float reloadSpeed;
    public float maxAmmoCount;
}
