using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    private void Awake() 
    {
        if(instance==null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public event Action onButtonClicked, onClearTreesButtonClicked, onProcessWoodClicked;
    public event Action<bool> onOpenUI;
    public void ButtonClicked()
    {
        if(onButtonClicked!=null)
            onButtonClicked();
    }
    public void TreesButtonClicked()
    {
        if(onClearTreesButtonClicked!=null)
            onClearTreesButtonClicked();
    }
    public void OnOpenUI(bool openUI)
    {
        if(onOpenUI!=null)
            onOpenUI(openUI);
    }
    public void OnProcessWood()
    {
        if(onProcessWoodClicked!=null)
            onProcessWoodClicked();
    }
}
