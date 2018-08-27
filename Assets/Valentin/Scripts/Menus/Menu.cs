using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EMenu
{
    PauseMenu,
    TitleMenu,
    LevelMenu,
    Count
}

public class Menu : MonoBehaviour
{
    [SerializeField]
    List<Button> buttons;
    [SerializeField]
    EMenu type;

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    protected void Start ()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        buttonSelected = true;
    }
	
	// Update is called once per frame
	protected void Update ()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
