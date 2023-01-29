using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RipperDoc : MonoBehaviour
{
    public List<MetaProgressionUI> metaProgressionUIs;
    public TMP_Text currencyText;

    private void Start()
    {
        LoadMetaProgression();
    }
    private void Update()
    {
        currencyText.text = GameManager.instance.saveSystem.GetCurrency().ToString();
    }
    public void LoadMetaProgression()
    {
       List<MetaProgressionContainer> metaProgressionContainers = GameManager.instance.metaProgressionManager.LoadMetaProgression();
       for (int i = 0; i < metaProgressionContainers.Count; i++)
        {
            metaProgressionUIs[i].LoadMetaUI(metaProgressionContainers[i]);
        }
    }
}
