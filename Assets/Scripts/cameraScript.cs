﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    GameObject Target;

    [SerializeField]
    private Vector3 BeginPos;
    [SerializeField]
    private Vector3 EndPos;

    [SerializeField]
    private float smoothing = 5f;

    //public Vector3 Offset { get; set; }

    public bool isInLevel { get; set; }
    public bool isInBeginning { get; set; }
    public bool isInEnd { get; set; }

    void Awake()
    {
        smoothing = 5f;
        isInBeginning = true;
        isInLevel = false;
        isInEnd = false;
    }

    void Start()
    {
        

        smoothing = 5f;
        isInBeginning = true;
        isInLevel = false;
        isInEnd = false;
    }

    void FixedUpdate()
    {
        //if (isInLevel)
        //{

        // Vector3 targetCamPos = Target.position + Offset;
        Target = GameObject.Find("Player(Clone)");
        transform.position = Vector3.Lerp(transform.position, new Vector3 (Target.transform.position.x, Target.transform.position.y, -10), smoothing * Time.deltaTime);
        //}
        //else if (isInEnd)
        //{
        //    transform.position = Vector3.Lerp(transform.position, EndPos, smoothing * Time.deltaTime);
        //}
        //else if (isInBeginning)
        //{
        //    transform.position = Vector3.Lerp(transform.position, BeginPos, smoothing * Time.deltaTime);
        //}
    }
}
