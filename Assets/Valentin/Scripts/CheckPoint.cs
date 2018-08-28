using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private int numCP = 0;

    private bool isChecked = false;

    public int Index
    {
        get { return numCP; }
    }

    public bool IsChecked
    {
        get { return isChecked; }
        set { isChecked = value; }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
