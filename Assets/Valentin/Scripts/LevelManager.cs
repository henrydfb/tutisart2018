﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {


    }

    // Update is called once per frame
    void Update ()
    {
		
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
}