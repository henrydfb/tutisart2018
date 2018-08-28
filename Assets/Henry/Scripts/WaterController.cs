using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public float amountFill = 0.1f;
    public GameObject limit;

    protected bool reachedLImit;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Drop")
        {
            Destroy(other.gameObject);
            
            if(transform.Find("top").transform.position.y < limit.transform.position.y)
                transform.position += new Vector3(0,amountFill);
        }
    }
}
