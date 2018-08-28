using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    private static int indexLevel;
    private static int lastCheckpoint = 0;

    public static int IndexLevel
    {
        get { return indexLevel; }
        set { indexLevel = value; }
    }

    public static int LastCheckpoint
    {
        get { return lastCheckpoint; }
        set { lastCheckpoint = value; }
    }
}
