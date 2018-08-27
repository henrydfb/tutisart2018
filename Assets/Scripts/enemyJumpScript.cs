using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyJumpScript : MonoBehaviour
{
    Vector3 _startingPos;

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float maxDistance = 3f;

    void Start()
    {
        _startingPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(_startingPos.x, _startingPos.y + Mathf.PingPong(Time.time * speed, maxDistance), _startingPos.z);
    }
}
