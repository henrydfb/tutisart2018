using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public const float MAX_WATER = 100;

    float water = MAX_WATER;
    bool insideCloudeZone;
    GameController gameController;
    CloudZoneController zone;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(LoseWaterCoroutine());
    }

    public float GetWaterAmount()
    {
        return water;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CloudZone")
        {
            insideCloudeZone = true;
            zone = other.gameObject.GetComponent<CloudZoneController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "CloudZone")
        {
            insideCloudeZone = false;
            zone = null;
        }
    }

    public bool IsInCloudZone()
    {
        return insideCloudeZone;
    }

    private void Update()
    {
        //transform.position += new Vector3(Input.GetAxis("Horizontal"), 0) * 0.1f;

        if (Input.GetKeyUp(KeyCode.E) && insideCloudeZone)
        {
            if (zone != null)
            {
                LoseWater(10);
                zone.path.CreateCloud();
            }
        }
    }

    public void LoseWater(float amount)
    {
        water -= amount;
        gameController.UpdatePlayerHUD();

        if(water <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void GainWater(float amount)
    {
        water += amount;
        gameController.UpdatePlayerHUD();
    }

    public IEnumerator LoseWaterCoroutine()
    {
        const float WATER_LOSS = 0.1f;

        while (true)
        {
            yield return new WaitForSeconds(1);

            LoseWater(WATER_LOSS);
        }
    }
}
