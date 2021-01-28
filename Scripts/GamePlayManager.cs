using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Transform tankReservePanel;
    [SerializeField]
    Text playerLivesText, stageNumber;
    GameObject tankImage;

    [SerializeField]
    Image fullHeatlh, midHealth, lowHealth;

    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    GameObject[] spawnPoints, spawnPlayerPoints;
    bool stageStart = false;

    void Start()
    {
        stageNumberText.text = "STAGE " + GameManager.stageNumber.ToString();
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        UpdateTankReserve();
        SetPlayerHealth();
        UpdatePlayerLives();
        UpdateStageNumber();
        StartCoroutine(StartStage());
    }

    private void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("enemy") == null)
        {
            GameManager.stageCleared = true;
            LevelCompleted();
        }
    }
    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        SceneManager.LoadScene("Score");
    }

    public void SpawnPlayer()
    {
        if (GameManager.playerLives > 0)
        {
            if (!stageStart)
            {
                GameManager.playerLives--;
            }
            stageStart = false;
            Animator anime = spawnPlayerPoints[0].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
            SetPlayerHealth();
            UpdatePlayerLives();
        }
        else
        {
            StartCoroutine(GameOver());
        }

    }
    public void UpdatePlayerHealth(int currentHealth)
    {
        if (currentHealth == 0) lowHealth.gameObject.SetActive(false);
        else if (currentHealth == 1) midHealth.gameObject.SetActive(false);
        else if (currentHealth == 2) fullHeatlh.gameObject.SetActive(false);
    }
    void SetPlayerHealth()
    {
        lowHealth.gameObject.SetActive(true);
        midHealth.gameObject.SetActive(true);
        fullHeatlh.gameObject.SetActive(true);
    }

    public void UpdatePlayerLives()
    {
        playerLivesText.text = GameManager.playerLives.ToString();
    }
    void UpdateStageNumber()
    {
        stageNumber.text = GameManager.stageNumber.ToString();
    }

    bool tankReserveEmpty = false;
    void SpawnEnemy()
    {
        if (LevelManager.smallTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            CancelInvoke();
            tankReserveEmpty = true;
        }
    }
    void UpdateTankReserve()
    {
        int j;
        int numberOfTanks = LevelManager.smallTanks;
        for (j = 0; j < numberOfTanks; j++)
        {
            tankImage = tankReservePanel.transform.GetChild(j).gameObject;
            tankImage.SetActive(true);
        }
    }
    public void RemoveTankReserve()
    {
        int numberOfTanks = LevelManager.smallTanks;
        tankImage = tankReservePanel.transform.GetChild(numberOfTanks).gameObject;
        tankImage.SetActive(false);
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnPlayer();
    }
    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 40f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        GameManager.stageCleared = false;
        LevelCompleted();
    }

}
