using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int smallTanksInThisLevel,stageNumber;
    public static int smallTanks;
    [SerializeField]
    float spawnRateInThisLevel = 5;       
    public static float spawnRate { get; private set; }

    private void Awake()
    {
        GameManager.stageNumber = stageNumber;
        smallTanks = smallTanksInThisLevel;
        spawnRate = spawnRateInThisLevel;
    }
}
