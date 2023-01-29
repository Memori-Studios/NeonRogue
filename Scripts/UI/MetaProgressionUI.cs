using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MetaProgressionUI : MonoBehaviour
{
    public MetaProgressionContainer metaProgressionContainer;
    [SerializeField] private Image outlineImage;
    [SerializeField] private TMP_Text statName, statIncreaseAmount, statLevel, statCost;
    [SerializeField] private Color inProgressColor, highlightedColor, maxedColor;
    [SerializeField] private Slider slider;
    private Color desiredColor;
    private int currentCost;
    private bool pointerUp;
    public void LoadMetaUI(MetaProgressionContainer metaProgressionContainer)
    {
        this.metaProgressionContainer = metaProgressionContainer;
        RefreshUI();
    }
    private void RefreshUI()
    {
        statName.text = metaProgressionContainer.metaProgressionSO.name;

        for (int i = 0; i < metaProgressionContainer.metaLevels.Count; i++)
        {
            if(!metaProgressionContainer.metaLevels[i].unlocked)
            {
                desiredColor = inProgressColor;
                outlineImage.color = desiredColor;
                float currentAmount;
                if(i!=0)
                    currentAmount = metaProgressionContainer.metaLevels[i-1].modificaitonAmount;
                else //get the base value
                    currentAmount = GameManager.instance.metaProgressionManager.playerBaseStats.Find(x => x.metaProgressionSO == metaProgressionContainer.metaProgressionSO).baseValue;

                float nextAmount = metaProgressionContainer.metaLevels[i].modificaitonAmount;
                string percentage = "";
                if(nextAmount>=100)
                    percentage = "%";

                statIncreaseAmount.text = $"{currentAmount}{percentage} > {nextAmount}{percentage}";
                
                statLevel.text = $"{i} / {metaProgressionContainer.metaLevels.Count}";
                currentCost = (int)metaProgressionContainer.metaLevels[i].cost;
                statCost.text = currentCost.ToString();
                break;
            }

            if(i == metaProgressionContainer.metaLevels.Count-1)
            {
                desiredColor = maxedColor;
                outlineImage.color = desiredColor;

                float currentAmount = metaProgressionContainer.metaLevels[i].modificaitonAmount;
                string percentage = "";
                if(currentAmount>=100)
                    percentage = "%";

                statIncreaseAmount.text = $"{currentAmount}{percentage}";
                statLevel.text = $"{metaProgressionContainer.metaLevels.Count} / {metaProgressionContainer.metaLevels.Count}";
                statCost.text = "";
                slider.gameObject.SetActive(false);
            }
        }
    }
    public void OnPointerEnter()
    {
        outlineImage.color = highlightedColor;
    }
    public void OnPointerExit()
    {
        outlineImage.color = desiredColor;
    }
    public void OnPointerDown()
    {
        pointerUp = false;
        if(GameManager.instance.saveSystem.GetCurrency() >= currentCost)
            StartCoroutine(OnPointerDownRoutine());
    }
    public void OnPointerUp()
    {
        pointerUp = true;
        slider.value = 0f;
    }
    private IEnumerator OnPointerDownRoutine()
    {
        slider.value = 0f;
        float duration = 0.5f;
        float startTime = Time.time;
        float endTime = startTime + duration;
        while (Time.time < endTime)
        {
            if(pointerUp)
                yield break;

            float t = (Time.time - startTime) / duration;
            slider.value = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        slider.value = 0f;
        GameManager.instance.saveSystem.AddCurrency(-currentCost);
        GameManager.instance.metaProgressionManager.UnlockMetaProgression(metaProgressionContainer.metaProgressionSO);
        LoadMetaUI(GameManager.instance.metaProgressionManager.metaProgressionContainers.Find(x => x.metaProgressionSO == metaProgressionContainer.metaProgressionSO));
    }
}
