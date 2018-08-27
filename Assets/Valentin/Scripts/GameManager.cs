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

    //public static GameManager instance = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
