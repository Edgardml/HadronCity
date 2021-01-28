using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int playerLives = 3;
    public static int playerScore = 0;

    public static bool stageCleared = false;

    [SerializeField]
    int tankPoints = 100;
    public int smallTankPointsWorth { get { return tankPoints; } }
    public static int smallTanksDestroyed, smallTanks;


    public static int stageNumber;


    static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
