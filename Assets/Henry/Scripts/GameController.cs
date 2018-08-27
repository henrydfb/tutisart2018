using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Image playerWaterHUD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlayerHUD()
    {
        PlayerController player;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerWaterHUD.fillAmount = (player.GetWaterAmount() / PlayerController.MAX_WATER);
    }
}
