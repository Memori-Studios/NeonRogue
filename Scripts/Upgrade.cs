using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="ScriptableObjects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    public Sprite upgradeIcon;
    public UpgradeLevel[] upgradlevels;
    public UpgradeClass upgradeClass;
    public enum UpgradeClass {Common,Rare,Legendary,Exotic};
    public UpgradeType upgradeType;
    public enum UpgradeType {stat, weapon, cyberware};
    public WeaponType weaponType;
}
[System.Serializable] public struct UpgradeLevel
{
    // public int upgradeLevel;
    public float upgradeModifier;
}
