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
    bool spawningCloud;

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
        switch (other.gameObject.tag)
        {
            case "CloudZone":
                insideCloudeZone = true;
                zone = other.gameObject.GetComponent<CloudZoneController>();
                if (zone != null)
                {
                    if (zone.path != null)
                        zone.path.Activate();
                }
                break;
            case "HealingCloud":
                other.gameObject.GetComponent<HealingCloudController>().StartRain();
                break;
            default:
                break;


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "CloudZone":
                if (zone != null)
                {
                    if (zone.path != null)
                        zone.path.Deactivate();
                }
                insideCloudeZone = false;
                zone = null;
                break;
            case "HealingCloud":
                other.gameObject.GetComponent<HealingCloudController>().StopRain();
                break;
            default:
                break;
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
                transform.Find("Container").GetComponent<Animator>().SetTrigger("SpawnCloud");
                spawningCloud = true;
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
        if (water + amount <= MAX_WATER)
            water += amount;
        else
            water = MAX_WATER;
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

    public void SpawnCloud()
    {
        if (zone != null)
        {
            if (zone.path != null)
            {
                LoseWater(10);
                zone.path.CreateCloud();
            }
        }
    }

    public void EndSpawningCloud()
    {
        spawningCloud = false;
    }

    public bool IsSpawningCloud()
    {
        return spawningCloud;
    }
}
