using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<Menu> menus;

    public static MenuManager instance = null;

    public Menu GetMenu(EMenu type)
    {
        foreach (Menu menu in menus)
        {
            if (menu.Type == type)
                return menu;
        }
        return null;
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
	}
}
