using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState
{
    InGame,
    Menu,
    Pause,
    Count
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    EGameState gameState = EGameState.Menu;

    public static GameManager instance = null;

    public EGameState GameState
    {
        get { return gameState; }
        set { gameState = value; }
    }

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == EGameState.InGame)
        {
            gameState = EGameState.Pause;
            MenuManager.instance.GetMenu(EMenu.PauseMenu).gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameState == EGameState.Pause)
        {
            gameState = EGameState.InGame;
            MenuManager.instance.GetMenu(EMenu.PauseMenu).gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LoadLevel(int numLevel)
    {
        SceneManager.LoadScene(numLevel);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
