using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStoreManager : MonoBehaviour
{
    private List<WeaponType> weaponTypes;
    [SerializeField] private CanvasGroup upgradesCanvasGroup, weaponCanvasGroup;
    // [SerializeField] private List<WeaponStatsUI> weaponStatsUIs;
    public void Start()
    {
        weaponTypes = GameManager.instance.saveSystem.weapons;
    }
    public void LoadWeaponScreen(WeaponType weaponType)
    {
        // load the weapon page
        // for (int i = 0; i < weaponStatsUIs.Count; i++)
        //     weaponStatsUIs[i].LoadStat(weaponType, weaponType.weaponModifications[i].weaponModifier);
        
        upgradesCanvasGroup.Enable();
        weaponCanvasGroup.Disable();
    }
    public void CloseWeaponScreen()
    {
        upgradesCanvasGroup.Disable();
        weaponCanvasGroup.Enable();
    }
}
