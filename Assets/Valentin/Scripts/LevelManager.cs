using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private int index;

	// Use this for initialization
	void Start ()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(index);

        Level.IndexLevel = index;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
