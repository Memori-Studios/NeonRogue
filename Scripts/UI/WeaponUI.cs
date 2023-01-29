using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public Weapon weapon;
    public Sprite activeBackground, inactiveBackground, autoFireSprite, manualFireSprite;
    public Image weaponIcon, firingModeIcon, lockedIcon;
    public TMP_Text weaponText;
    public void UnlockUI()
    {
        lockedIcon.enabled = false;
    }
    public void LoadWeaponUI(Weapon _weapon)
    {
        weapon = _weapon;
        weapon.weaponUI = this;
        weaponIcon.sprite = weapon.icon;
        weaponIcon.enabled = true;
        firingModeIcon.enabled = true;

        if(weapon.weaponClass != Weapon.WeaponClass.melee)
            UpdateAmmo(weapon.MaxAmmoCount);
    }
    public void UpdateAmmo(int amount)
    {
        if(weapon!=null)
            weaponText.text = amount +"/"+ weapon.MaxAmmoCount.ToString();
    }
    public void ToggleFireMode()
    {
        if(firingModeIcon.sprite==autoFireSprite)
            firingModeIcon.sprite = manualFireSprite;
        else
            firingModeIcon.sprite = autoFireSprite;
    }
}
