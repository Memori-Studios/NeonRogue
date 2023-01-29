using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SaveSystem saveSystem;
    public MetaProgressionManager metaProgressionManager;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }
}
