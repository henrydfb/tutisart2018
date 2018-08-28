using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public void StartJumpEnded()
    {
        transform.parent.GetComponent<CharacterMovement>().StartJumpEnded();
    }

    public void SpawnCloud()
    {
        transform.parent.GetComponent<PlayerController>().SpawnCloud();
    }

    public void EndSpawnCloud()
    {
        transform.parent.GetComponent<PlayerController>().EndSpawningCloud();
    }

    public void EndLand()
    {
        transform.parent.GetComponent<PlayerController>().HandleLandEnded();
    }
}
