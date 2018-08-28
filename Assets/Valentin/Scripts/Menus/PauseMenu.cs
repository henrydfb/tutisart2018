using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    public void ResumeAction()
    {
        GameManager.instance.GameState = EGameState.InGame;
        MenuManager.instance.GetMenu(EMenu.PauseMenu).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
