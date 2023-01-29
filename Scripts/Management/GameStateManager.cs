using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public enum SceneIndexes
{
    Start = 0,
    MainMenu = 1,
    InGame = 2,
    Map = 3,
    PostGame = 4
}
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStateEnum currentGameState;
    public bool sceneChangeInProgress;
    public CanvasGroup blackUIScreen;
    public WaitForSeconds pauseLength = new WaitForSeconds(0.5f);
    [HideInInspector] public int tavernSceneNumber = 1;
    public bool quickStart;

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
    private void Start()
    {
        if(quickStart)
        {
            PlayerManager.instance.StartGame();
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.InGame));
        }
        else
            SwitchGameState(GameStateEnum.MainMenu);
    }
    public void SwitchGameState(GameStateEnum gameStateEnum)
    {
        currentGameState = gameStateEnum;
        StartCoroutine(SwitchGameState());
    }
    public void QuickFadeIn()
    {
        StartCoroutine(blackUIScreen.FadeIn());
    }
    public void QuickFadeOut()
    {
        StartCoroutine(blackUIScreen.FadeOut());
    }
    public IEnumerator SwitchGameState()
    {
        sceneChangeInProgress = true;
        StartCoroutine(blackUIScreen.FadeIn());
        yield return pauseLength;

        switch (currentGameState)
        {
            case GameStateEnum.MainMenu:
                //check if scene is loaded, if not, load it
                if(!SceneManager.GetSceneByBuildIndex((int)SceneIndexes.MainMenu).isLoaded)
                {
                    StartCoroutine(LoadScene((int)SceneIndexes.MainMenu));
                }
                else
                {
                    yield return new WaitForSeconds(0.25f);
                    StartCoroutine(CompleteSceneLoad());
                }
                UnloadScene((int)SceneIndexes.InGame);
                UnloadScene((int)SceneIndexes.PostGame);
                break;
            case GameStateEnum.InGame:
                StartCoroutine(LoadScene((int)SceneIndexes.InGame));
                UnloadScene((int)SceneIndexes.PostGame);
                UnloadScene((int)SceneIndexes.MainMenu);
                break;
            case GameStateEnum.PostGame:
                StartCoroutine(LoadScene((int)SceneIndexes.PostGame));
                UnloadScene((int)SceneIndexes.InGame);
                UnloadScene((int)SceneIndexes.Map);
                break;
            case GameStateEnum.Restart:
                UnloadScene((int)SceneIndexes.InGame);
                UnloadScene((int)SceneIndexes.Map);
                GameStateManager.instance.SwitchGameState(GameStateEnum.InGame);
                break;
        }
    }
    private void UnloadScene(int sceneNumber)
    {
        if(SceneManager.GetSceneByBuildIndex(sceneNumber).isLoaded)
            SceneManager.UnloadSceneAsync(sceneNumber);
    }
    private IEnumerator LoadScene(int sceneNumber)
    {
        if(!SceneManager.GetSceneByBuildIndex(sceneNumber).isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
                yield return null;
        }
        StartCoroutine(CompleteSceneLoad());
    }
    private IEnumerator CompleteSceneLoad()
    {
        StartCoroutine(blackUIScreen.FadeOut());
        yield return pauseLength;
        sceneChangeInProgress = false;

        switch (currentGameState)
        {
            case GameStateEnum.Restart:
            Debug.Log($"restarted game");
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.InGame));
                PlayerManager.instance.StartGame();
                break;
            case GameStateEnum.InGame:
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.InGame));
                PlayerManager.instance.StartGame();
                break;
        }
    }
}
public enum GameStateEnum{MainMenu, InGame, Restart, PostGame}


