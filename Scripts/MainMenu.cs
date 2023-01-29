using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup main, options, sound, video, controls, ripperDoc;
    [SerializeField] private GameObject ripperDocPrefab, gunShopPrefab;
    public void PlayGame()
    {
        GameStateManager.instance.SwitchGameState(GameStateEnum.InGame);
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
    public void OpenRipperDoc()
    {
        StartCoroutine(RipperDocTransition(true));
    }
    public void BackToGunShop()
    {
        ripperDoc.Disable();
        StartCoroutine(RipperDocTransition(false));
    }
    public IEnumerator RipperDocTransition(bool toRipperDoc)
    {
        GameStateManager.instance.QuickFadeIn();

        yield return GameStateManager.instance.pauseLength;

        ripperDocPrefab.SetActive(toRipperDoc);
        gunShopPrefab.SetActive(!toRipperDoc);

        GameStateManager.instance.QuickFadeOut();
        if(!toRipperDoc)
            ReturnToMain();
        else
        {
            ripperDoc.Enable();
            main.Disable();
        }
    }
    public void ReturnToOptions()
    {
        video.Disable();
        sound.Disable();
        controls.Disable();
        ripperDoc.Disable();
        options.Enable();
    }
    public void Quit()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
