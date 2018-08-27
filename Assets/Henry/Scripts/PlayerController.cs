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

    Rigidbody2D m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
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
        if (other.gameObject.tag == "enemy")
        {
            m_Rigidbody.AddForce(new Vector2(-transform.forward.x, 0), ForceMode2D.Impulse);
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

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(water / MAX_WATER, water / MAX_WATER, water / MAX_WATER), 10f * Time.deltaTime);

        if(water <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void GainWater(float amount)
    {
        water += amount;
        gameController.UpdatePlayerHUD();
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(water / MAX_WATER, water / MAX_WATER, water / MAX_WATER), 10f * Time.deltaTime);
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
