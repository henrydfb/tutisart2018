using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<CheckPoint> checkPoints;
    [SerializeField]
    GameObject player;

    private int index;
    private int checkPoint;

    public static LevelManager instance = null;

    public int CheckPoint
    {
        get { return checkPoint; }
        set { checkPoint = value; }
    }

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        Level.IndexLevel = index;

        Instantiate(player, checkPoints[Level.LastCheckpoint].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnDestroy()
    {
        Level.LastCheckpoint = checkPoint;
    }
}
