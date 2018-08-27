using System.Collections;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public DropController dropPrefab;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Rain());
	}
	
    public IEnumerator Rain()
    {
        const float CLOUD_WIDTH = 1.5f;
        float waitTime, x, y;
        int drops = 0;

        waitTime = Random.Range(0.5f, 1f);
        while (drops < 20)
        {
            yield return new WaitForSeconds(waitTime);

            x = Random.Range(transform.position.x - CLOUD_WIDTH / 2, transform.position.x + CLOUD_WIDTH / 2);
            y = transform.position.y;

            Instantiate(dropPrefab, new Vector3(x, y), Quaternion.identity);

            waitTime = Random.Range(0.5f, 1f);
            drops++;
        }

        Destroy(gameObject);
    }
}
