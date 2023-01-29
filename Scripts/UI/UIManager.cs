using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerUI playerUI;
    [SerializeField] private List<UpgradeUI> upgradeUIs;
    public TMP_Text timerText;

    [Header("Major Canvas Groups")]
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup upgradeCanvasGroup, pause, endGameCanvasGroup;

    [Header("Minor Canvas Groups")]
    [SerializeField] private CanvasGroup main;
    [SerializeField] private CanvasGroup options, sound, video, controls;
    public bool Paused => pause.alpha == 1;
    PlayerStats playerStats;
    UpgradeManager upgradeManager;
    public Texture2D cursorTexture, crossHairTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    [SerializeField] private List<Slider> rollChargeSliders;
    [SerializeField] private GameObject rollChargePrefab;
    [SerializeField] private Transform rollChargeParent;

    [Header("Cyber Upgrades")]
    [SerializeField] private GameObject cyberUpgradePrefab;
    [SerializeField] private Transform cyberUpgradeParent;
    [SerializeField] private CyberUpgradesUI sandevistanUI, angelFireUI, droneUI;


    [Header("Sandevistan")]
    [SerializeField] private Sprite sandevistanIcon;
    [SerializeField] private Color sandyActiveColor, sandyInactiveColor;

    [Header("AngelFire")]
    [SerializeField] private Sprite angelFireIcon;
    [SerializeField] private Color angelFireActiveColor, angelFireInactiveColor;

    [Header("Drone")]
    [SerializeField] private Sprite droneIcon;
    [SerializeField] private Color droneActiveColor, droneInactiveColor;

    [SerializeField]

    private void Start()
    {
        playerStats = PlayerManager.instance.playerStats;
        upgradeManager = PlayerManager.instance.upgradeManager;

        playerUI.healthBar.maxValue = PlayerManager.instance.startingHealth;
        playerUI.healthBar.value = PlayerManager.instance.startingHealth;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    public void DisplayHealth(float newValue){
        playerUI.healthBar.value = newValue;
    }
    public void CreateRollCharges()
    {
        for (int i = 0; i < playerStats.RollCharges; i++)
        {
            GameObject rollCharge = Instantiate(rollChargePrefab, rollChargeParent);
            rollChargeSliders.Add(rollCharge.GetComponent<Slider>());
            Debug.Log($"added");
        }
    }
    public void AddRollCharges()
    {
        GameObject rollCharge = Instantiate(rollChargePrefab, rollChargeParent);
        rollChargeSliders.Add(rollCharge.GetComponent<Slider>());
    }
    public void DisplayRollCharge(float amount)
    {
        for (int i = 0; i < rollChargeSliders.Count; i++)
        {
            rollChargeSliders[i].value = amount;
            amount-=1;
        }
    }
    public void PauseForUpgrade()
    {
        Time.timeScale = 0;

        List<Upgrade> randomUpgrades = new List<Upgrade>();

        //select a weapon at first
        if(playerStats.expLevel.levelNumber==0)
            randomUpgrades =  upgradeManager.GetWeaponUpgrades(playerStats.UpgradesToDraw);
        else
            randomUpgrades =  upgradeManager.GetRandomUpgrades(playerStats.UpgradesToDraw);


        for (int i = 0; i < upgradeUIs.Count; i++)
            upgradeUIs[i].gameObject.SetActive(false);

        for (int i = 0; i < randomUpgrades.Count; i++)
            upgradeUIs[i].gameObject.SetActive(true);

        for (int i = 0; i < randomUpgrades.Count; i++)
            upgradeUIs[i].LoadUpgradeUI(randomUpgrades[i]);

        mainCanvasGroup.Disable();
        upgradeCanvasGroup.Enable();
        main.Enable();
    }
    public void UnpauseAfterUpgrade()
    {
        Cursor.SetCursor(crossHairTexture, hotSpot, cursorMode);
        Time.timeScale = 1;
        upgradeCanvasGroup.Disable();
        mainCanvasGroup.Enable();
    }
    
    public void LoadCyberUpgrade(CyberwareType cyberUpgradeType, int cyberwareIndex)
    {
        CyberUpgradesUI newUpgrade = Instantiate(cyberUpgradePrefab, cyberUpgradeParent).GetComponent<CyberUpgradesUI>();
        newUpgrade.cyberUpgradeType = cyberUpgradeType;

        switch(cyberUpgradeType)
        {
            case CyberwareType.Sandevistan:
                sandevistanUI = newUpgrade;
                sandevistanUI.cyberUpgradeImage.sprite = sandevistanIcon;
                sandevistanUI.activeColor = sandyActiveColor;
                sandevistanUI.inactiveColor = sandyInactiveColor;
                sandevistanUI.hotkeyText.text = cyberwareIndex.ToString();
                break;
            case CyberwareType.AngelFire:
                angelFireUI = newUpgrade;
                angelFireUI.cyberUpgradeImage.sprite = angelFireIcon;
                angelFireUI.activeColor = angelFireActiveColor;
                angelFireUI.inactiveColor = angelFireInactiveColor;
                angelFireUI.hotkeyText.text = cyberwareIndex.ToString();
                angelFireUI.hotkeyImage.enabled = true;
                break;
            case CyberwareType.Drone:
                droneUI = newUpgrade;
                droneUI.cyberUpgradeImage.sprite = droneIcon;
                droneUI.activeColor = droneActiveColor;
                droneUI.inactiveColor = droneInactiveColor;
                droneUI.hotkeyText.text = "";
                droneUI.hotkeyImage.enabled = true;
                break;
        }
    }
    public IEnumerator RechargeCyberUpgradeUI(CyberUpgradesUI cyberUpgradesUI, float startValue, float endValue, float lerpDuration)
    {
        cyberUpgradesUI.cyberUpgradeSlider.value = startValue;
        cyberUpgradesUI.cyberUpgradeImage.color = cyberUpgradesUI.inactiveColor;

        float currentLerpTime = 0f, lerpValue = 0f;
  
        while (lerpValue != endValue)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime < lerpDuration)
            {
                lerpValue = Mathf.Lerp(startValue, endValue, currentLerpTime / lerpDuration);
            }
            else
            {
                lerpValue = endValue;
            }
            cyberUpgradesUI.cyberUpgradeSlider.value = 1-lerpValue;
            yield return null;
        }
        cyberUpgradesUI.cyberUpgradeImage.color = cyberUpgradesUI.activeColor;

        switch (cyberUpgradesUI.cyberUpgradeType)
        {
            case CyberwareType.Sandevistan:
                Sandevistan.instance.OnCoolDown = false;
                break;
        }
    }
    public void RechargeCyberUpgradeUI(CyberwareType cyberUpgradeType)
    {
        switch (cyberUpgradeType)
        {
            case CyberwareType.Sandevistan:
                StartCoroutine(RechargeCyberUpgradeUI(sandevistanUI,0,1,PlayerManager.instance.playerStats.SandevistanRechargeRate));
                break;
            case CyberwareType.AngelFire:
                StartCoroutine(RechargeCyberUpgradeUI(angelFireUI,0,1,PlayerManager.instance.playerStats.AngelFireRechargeRate));
                break;
            default:
                break;
        }
    }
    #region CanvasGroup toggling
    public void OpenPauseCanvas()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        pause.Enable();
        mainCanvasGroup.Disable();
        Time.timeScale = 0;
    }
    public void ClosePauseCanvas()
    {
        Cursor.SetCursor(crossHairTexture, hotSpot, cursorMode);
        Time.timeScale = 1;
        ReturnToMain();
        pause.Disable();

        if(upgradeCanvasGroup.alpha == 0)
            mainCanvasGroup.Enable();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        PlayerManager.instance.inputHandler.inputActions.Disable();
        PlayerManager.instance.Die();
        Destroy(PlayerManager.instance.gameObject);
        GameStateManager.instance.SwitchGameState(GameStateEnum.Restart);
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        PlayerManager.instance.inputHandler.inputActions.Disable();
        Destroy(PlayerManager.instance.gameObject);
        GameStateManager.instance.SwitchGameState(GameStateEnum.MainMenu);
    }
    public void Options()
    {
        main.Disable();
        options.Enable();
    }
    public void ReturnToMain()
    {
        options.Disable();
        main.Enable();
    }
    public void OpenSound()
    {
        options.Disable();
        sound.Enable();
    }
    public void OpenVideo()
    {
        options.Disable();
        video.Enable();
    }
    public void OpenControls()
    {
        options.Disable();
        controls.Enable();
    }
    public void ReturnToOptions()
    {
        video.Disable();
        sound.Disable();
        controls.Disable();
        options.Enable();
    }
    #endregion
    public IEnumerator EndGameUI()
    {
        StartCoroutine(mainCanvasGroup.FadeOut(3));
        StartCoroutine(endGameCanvasGroup.FadeIn(3));
        yield return new WaitForSeconds(3);
        GameStateManager.instance.SwitchGameState(GameStateEnum.PostGame);
    }
}
public enum CyberwareType
{
    Sandevistan,
    AngelFire,
    Drone
}