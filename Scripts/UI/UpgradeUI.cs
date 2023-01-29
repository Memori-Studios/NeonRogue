using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private RenderTexture upgradeRender;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private TMP_Text upgradeNameText, upgradeDescription, levelText;
    [SerializeField] private UpgradeRenders upgradeRenders;

    public void LoadUpgradeUI(Upgrade _upgrade)
    {
        upgrade = _upgrade;
        upgradeRenders.LoadUpgrade(upgrade);
        rawImage.texture = upgradeRender;
        
        // upgradeImage.sprite = upgrade.upgradeIcon;
        upgradeNameText.text = upgrade.upgradeName;
        int upgradeLevel = PlayerManager.instance.GetUpgradeLevel(upgrade);

        if(upgrade.upgradlevels!=null && upgrade.upgradlevels.Length>0)
        {
            string modificationAmount = upgrade.upgradlevels[upgradeLevel].upgradeModifier.ToString();

            if(upgrade.upgradlevels[upgradeLevel].upgradeModifier>=100)
                modificationAmount += "%";

            if(upgrade.upgradeDescription.Contains("{stat}"))
            {
                upgradeDescription.text = upgrade.upgradeDescription.Replace("{stat}", modificationAmount);
            }
            else
            {
                upgradeDescription.text = upgrade.upgradeDescription;
            }
            levelText.text = "Level "+ upgradeLevel.ToString() + "/" + upgrade.upgradlevels.Length.ToString();
        }
        else // for cyberware
        {
            levelText.text = "";
            upgradeDescription.text = upgrade.upgradeDescription;
        }
        

    }
    public void AcquireUpgrade()
    {
        PlayerManager.instance.AcquireUpgrade(upgrade);
    }
}
