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
    
    [SerializeField]
    float WATER_LOSS = 0.1f;

    float currentWaterLose;
    DropZoneController Dropzone;

    Rigidbody2D m_Rigidbody;
    private int waterCost = 0;

    [SerializeField]
    private int damageTaken = 10;

    [SerializeField]
    private float knockForce = 100f;

    bool insideDropZone;

    bool isHit;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        currentWaterLose = WATER_LOSS;
        StartCoroutine(LoseWaterCoroutine());
    }
    
    public float GetWaterAmount()
    {
        return water;
    }

    public bool IsHit()
    {
        return isHit;
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
            case "Drop":
                
                if (other.GetComponent<DropZoneController>() != null)
                {
                    insideDropZone = true;
                    waterCost = other.GetComponent<DropZoneController>().Cost;
                }
                break;
            case "HotZone":
                HotZone hotZone = other.gameObject.GetComponent<HotZone>();
                currentWaterLose = hotZone.WaterLoss;

                Debug.Log("HotZONE");
                break;
            case "MagmaZone":
                SceneManager.LoadScene("GameOver");
                break;
            case "enemy":
                if (!isHit)
                {
                    if (transform.position.x <= other.transform.position.x)
                        m_Rigidbody.AddForce(new Vector2(-1, 1) * knockForce);
                    else
                        m_Rigidbody.AddForce(new Vector2(1, 1) * knockForce);

                    LoseWater(damageTaken);

                    transform.Find("Container").GetComponent<Animator>().SetTrigger("Hit");
                    isHit = true;
                    StartCoroutine(PreventHit());
                }
                break;
            default:
                break;


        }
    }

    public IEnumerator PreventHit()
    {
        transform.Find("Container").GetComponent<SpriteRenderer>().color = Color.white * 0.5f;
        yield return new WaitForSeconds(0.1f);

        isHit = false;
        transform.Find("Container").GetComponent<SpriteRenderer>().color = Color.white;

        if (GetComponent<CharacterMovement>().IsWalking())
        {
            transform.Find("Container").GetComponent<Animator>().SetTrigger("Walk");
        }
        else
        {
            transform.Find("Container").GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (transform.position.x <= other.transform.position.x)
                m_Rigidbody.AddForce(new Vector2(-1, 1) * knockForce);
            else
                m_Rigidbody.AddForce(new Vector2(1, 1) * knockForce);

            LoseWater(damageTaken);

            GetComponent<Animator>().SetTrigger("Hit");
            isHit = true;
        }
    }*/

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
            case "Drop":
                waterCost = 0;
                insideDropZone = false;
                Dropzone = null;
                break;
            case "HotZone":
                currentWaterLose = WATER_LOSS;
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

        if (Input.GetKeyUp(KeyCode.Z) && insideDropZone)
        {
            LoseWater(waterCost);
        }
    }
   
    public void LoseWater(float amount)
    {
        /*water -= amount;
        gameController.UpdatePlayerHUD();

        if(water <= 0)
            SceneManager.LoadScene("GameOver");*/

        water -= amount;
        gameController.UpdatePlayerHUD();

        transform.localScale = new Vector3(water / MAX_WATER, water / MAX_WATER, 1);// Vector3.Lerp(transform.localScale, new Vector3(water / MAX_WATER, water / MAX_WATER, water / MAX_WATER), 10f * Time.deltaTime);

        if (water <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void GainWater(float amount)
    {
        if (water + amount <= MAX_WATER)
            water += amount;
        else
            water = MAX_WATER;
        gameController.UpdatePlayerHUD();

        transform.localScale = new Vector3(water / MAX_WATER, water / MAX_WATER, 1); //Vector3.Lerp(transform.localScale, new Vector3(water / MAX_WATER, water / MAX_WATER, water / MAX_WATER), 10f * Time.deltaTime);
    }

    public IEnumerator LoseWaterCoroutine()
    {
        //const float WATER_LOSS = 0.1f;

        while (true)
        {
            yield return new WaitForSeconds(1);

            //LoseWater(WATER_LOSS);

            LoseWater(currentWaterLose);
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

    public void HandleLandEnded()
    {
        if (GetComponent<CharacterMovement>().IsWalking())
        {
            transform.Find("Container").GetComponent<Animator>().SetTrigger("Walk");
        }
        else
        {
            transform.Find("Container").GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
