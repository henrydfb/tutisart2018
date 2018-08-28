using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{
    public void Restart()
    {
        GameManager.instance.LoadLevel(Level.IndexLevel);
    }

}
