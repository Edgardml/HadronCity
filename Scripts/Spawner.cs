using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;
    [SerializeField]
    bool isPlayer;
    [SerializeField]
    GameObject smallTank;
    enum tankType
    {
        smallTank
    };

    void Start()
    {
            tanks = new GameObject[1] { smallTank };
    }

    public void StartSpawning()
    {
        if (!isPlayer)
        {
            List<int> tankToSpawn = new List<int>();
            tankToSpawn.Clear();
            if (LevelManager.smallTanks > 0) tankToSpawn.Add((int)tankType.smallTank);
            int tankID = tankToSpawn[Random.Range(0, tankToSpawn.Count)];
            tank = Instantiate(tanks[tankID], transform.position, transform.rotation);
            LevelManager.smallTanks--;

            GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
            GPM.RemoveTankReserve();
        }
        else
        {
            tank = Instantiate(tanks[0], transform.position, transform.rotation);
        }
    }

    public void SpawnNewTank()
    {
        if (tank != null) tank.SetActive(true);
    }
}
