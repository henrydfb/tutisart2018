using UnityEngine;

public class HealingDropController : DropController {

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.name == "Player")
            other.gameObject.GetComponent<PlayerController>().GainWater(10);
    }
}
