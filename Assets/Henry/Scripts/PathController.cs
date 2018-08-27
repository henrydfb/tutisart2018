using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

    public enum PathType
    {
        Open,
        Close
    }

    public enum MoveType
    {
        Forward,
        Backward
    }

    public CloudController cloudPrefab;
    public CloudAimController aimPrefab;
    public PathType type = PathType.Open;
    public MoveType move = MoveType.Forward;
    public float time = 1;

    public int aimPointIdx = 0;
    protected CloudAimController aim;
    PlayerController player;
    
    // Use this for initialization
    void Start ()
    {
        aim = Instantiate(aimPrefab, transform.GetChild(aimPointIdx).position, Quaternion.identity);

        StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx + 1).position, 1, ReachedPointCallback));


        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    public void ReachedPointCallback()
    {
        switch (move)
        {
            case MoveType.Forward:
                aimPointIdx++;

                if (aimPointIdx == transform.childCount - 1)
                {
                    switch (type)
                    {
                        case PathType.Close:
                            aimPointIdx = -1;
                            StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx + 1).position, time, ReachedPointCallback));
                            break;
                        case PathType.Open:
                            move = MoveType.Backward;
                            StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx - 1).position, time, ReachedPointCallback));
                            break;
                    }   
                }
                else
                    StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx + 1).position, time, ReachedPointCallback));
                
                break;

            case MoveType.Backward:
                
                    aimPointIdx--;

                    if (aimPointIdx == 0)
                    {
                        switch (type)
                        {
                            case PathType.Close:
                                break;
                            case PathType.Open:
                                move = MoveType.Forward;
                                StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx + 1).position, time, ReachedPointCallback));
                                break;
                        }

                        
                    }
                    else
                        StartCoroutine(Helper.MoveRoutine(aim.gameObject, transform.GetChild(aimPointIdx - 1).position, time, ReachedPointCallback));
                break;
        }
    }

    public void CreateCloud()
    {
        Instantiate(cloudPrefab, aim.transform.position, Quaternion.identity);
    }
}
