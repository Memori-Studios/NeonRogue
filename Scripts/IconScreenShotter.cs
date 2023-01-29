using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using TMPro;

public class IconScreenShotter : MonoBehaviour
{
    public bool takeScreenShots;
    public GameObject[] itemsToScreenShot;
    ScreenshotHandler screenshotHandler;
    public void DisplayGameObjects(int i)
    {
        foreach(GameObject item in itemsToScreenShot)
            item.SetActive(false);

        itemsToScreenShot[i].SetActive(true);
    }
    private void Start()
    {
        screenshotHandler = GetComponent<ScreenshotHandler>();
        // #if UNITY_EDITOR
        //         EditorUtility.SetDirty(scriptableObject);
        // #endif
        if(takeScreenShots)
            StartCoroutine(GenerateScreenShots());
            
        // AssetDatabase.SaveAssets();
    }
    public IEnumerator GenerateScreenShots()
    {
        yield return new WaitForSeconds(1f);
        
        int i = 0;
        while (i < itemsToScreenShot.Length)
        {
            DisplayGameObjects(i);
            yield return new WaitForSeconds(.25f);

            screenshotHandler.TakeScreenshot(512,512, itemsToScreenShot[i].name);
            i++;

            yield return new WaitForSeconds(.25f);
        }
    }
}
