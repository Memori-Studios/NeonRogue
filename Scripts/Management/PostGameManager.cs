using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostGameManager : MonoBehaviour
{
    public CanvasGroup postgameCanvasGroup;
    public TMP_Text timeSurvivedText, enemiesKilledText, totalScoreText, levelText;
    private void Start()
    {
        RecentGame recentGame = GameManager.instance.saveSystem.GetRecentGame();
        timeSurvivedText.text = $"{recentGame.timeSurvived.ToString("F2")}s";
        enemiesKilledText.text = $"{recentGame.enemiesKilled}";
        
        totalScoreText.text = $"{recentGame.rewardAmount}";
        StartCoroutine(postgameCanvasGroup.FadeIn());
    }
    public void PlayAgain()
    {
        GameStateManager.instance.SwitchGameState(GameStateEnum.InGame);
    }
    public void ReturnToMainMenu()
    {
        GameStateManager.instance.SwitchGameState(GameStateEnum.MainMenu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
