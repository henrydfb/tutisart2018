using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{
    public void Restart()
    {
        Level.LastCheckpoint = 0;
        GameManager.instance.LoadLevel(Level.IndexLevel);
    }

    public void RestartLastCheckPoint()
    {
        GameManager.instance.LoadLevel(Level.IndexLevel);
    }
}
