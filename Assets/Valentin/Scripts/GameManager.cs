using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    //public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
}
