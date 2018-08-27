using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private bool exitBegin = true;
    private bool enterEnd = true;

    void Update ()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 1.0f * Time.deltaTime, transform.position.y, transform.position.z);
        }
    
        if(Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 1.0f * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject child = transform.Find("targetCam").gameObject;
        if (collision.tag == "beginTrigger")
        {
            if (exitBegin)
            {   
                child.SetActive(true);
                Camera.main.GetComponent<cameraScript>().isInLevel = true;
                Camera.main.GetComponent<cameraScript>().isInBeginning = false;
                Camera.main.GetComponent<cameraScript>().Offset = Camera.main.transform.position - child.transform.position;
                Camera.main.GetComponent<cameraScript>().Target = child.transform;
                exitBegin = !exitBegin;
            }
            else
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInBeginning = true;
                Camera.main.GetComponent<cameraScript>().Offset = new Vector3(0,0,0);
                exitBegin = !exitBegin;
            }
            
        }
        if (collision.tag == "endTrigger")
        {
            if (enterEnd)
            {
                child.SetActive(false);
                Camera.main.GetComponent<cameraScript>().isInLevel = false;
                Camera.main.GetComponent<cameraScript>().isInEnd = true;
                Camera.main.GetComponent<cameraScript>().Offset = new Vector3(0, 0, 0);
                enterEnd = !enterEnd;
            }
            else
            {
                child.SetActive(true);
                Camera.main.GetComponent<cameraScript>().isInLevel = true;
                Camera.main.GetComponent<cameraScript>().isInEnd = false;
                Camera.main.GetComponent<cameraScript>().Offset = Camera.main.transform.position - child.transform.position;
                Camera.main.GetComponent<cameraScript>().Target = child.transform;
                enterEnd = !enterEnd;
            }
            
        }
    }
}
