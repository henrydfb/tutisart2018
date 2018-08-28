using System.Collections;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public DropController dropPrefab;

    bool isRaining;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Rain(20,true));
	}
	
    protected IEnumerator Rain(int dropsNumber = -1, bool destroy = false)
    {
        const float CLOUD_WIDTH = 1.5f;
        float waitTime, x, y;
        int drops = 0;

        isRaining = true;
        waitTime = Random.Range(0.1f, 0.5f);
        while (isRaining)
        {
            yield return new WaitForSeconds(waitTime);

            x = Random.Range(transform.position.x - CLOUD_WIDTH / 2, transform.position.x + CLOUD_WIDTH / 2);
            y = transform.position.y;

            Instantiate(dropPrefab, new Vector3(x, y), Quaternion.identity);

            waitTime = Random.Range(0.1f, 0.5f);
            drops++;

            if (dropsNumber > 0)
            {
                if (drops > dropsNumber)
                    isRaining = false;
            }
        }

        if(destroy)
            Destroy(gameObject);
    }

    public void StartRain()
    {
        StartCoroutine(Rain());
    }

    public void StopRain()
    {
        isRaining = false;
    }

}
