using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TallyScore : MonoBehaviour
{
    [SerializeField]
    Text hiScoreText, stageText, playerScoreText, smallTankScoreText,  smallTanksDestroyed, totalTanksDestroyed;
    int smallTankScore, fastTankScore, bigTankScore, armoredTankScore;
    GameManager masterTracker;
    int smallTankPointsWorth, fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;
    // Use this for initialization
    void Start()
    {
        masterTracker = GameObject.Find("GameManager").GetComponent<GameManager>();
        smallTankPointsWorth = masterTracker.smallTankPointsWorth;
        stageText.text = "STAGE " + GameManager.stageNumber;
        playerScoreText.text = GameManager.playerScore.ToString();
        StartCoroutine(UpdateTankPoints());
    }
    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i <= GameManager.smallTanksDestroyed; i++)
        {
            smallTankScore = smallTankPointsWorth * i;
            smallTankScoreText.text = smallTankScore.ToString();
            smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        
        totalTanksDestroyed.text = (GameManager.smallTanksDestroyed.ToString());
        GameManager.playerScore += (smallTankScore);
        yield return new WaitForSeconds(5f);
        if (GameManager.stageCleared)
        {
            ClearStatistics();
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            ClearStatistics();
            //Load GameOver Scene
        }
    }
    void ClearStatistics()
    {
        GameManager.smallTanksDestroyed = 0;
        GameManager.stageCleared = false;
    }
}
