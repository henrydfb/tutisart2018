using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{   
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Platform" || other.gameObject.name == "Player" || other.gameObject.name == "Wall")
            Destroy(gameObject);
    }
}
