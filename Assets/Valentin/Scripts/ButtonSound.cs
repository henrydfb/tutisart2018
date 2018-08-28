using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, ISelectHandler
{
    public AudioSource mySource;

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        mySource.Play();
    }
}
