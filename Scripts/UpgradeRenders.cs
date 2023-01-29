using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRenders : MonoBehaviour
{
    public Camera cam;
    public RenderTexture renderTexture;
    public Transform upgradePrefabsHolder;
    public List<UpgradeAndObject> upgradeAndObjects;
    public void LoadUpgrade(Upgrade upgrade)
    {
        foreach (var item in upgradeAndObjects)
        {
            if (item.upgrades == upgrade)
            {
                item.upgradeObject.SetActive(true);
            }
            else
            {
                item.upgradeObject.SetActive(false);
            }
        }
        // cam.targetTexture = renderTexture;
        // cam.Render();
        // cam.targetTexture = null;
    }
}
[System.Serializable] public struct UpgradeAndObject
{
    public Upgrade upgrades;
    public GameObject upgradeObject;
}
