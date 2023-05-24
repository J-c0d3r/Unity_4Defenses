using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem_Controller : MonoBehaviour
{
    public static WaveSystem_Controller wsc;

    private bool isWaveFinished;
    private bool canRunCycle;
    private int amountPlayers;
    private int spawnCycle;
    private float WaveLengthModifierAmountPlayers;
    private int amountEnemiesToSpawn;
    private int amountEnemiesSpawnedInScene;
    private int snapshotAmountEnemiesSpawnedInScene;
    private int currentWave;
    private int totalWave;
    private int currentAmountEnemies;
    private int totalAmountEnemies;
    private float intervalCountTime;
    //private float timeCountSpawn;
    //[SerializeField] private float timeSpawn;
    [SerializeField] private float timeSpawnEnemy;

    [SerializeField] private int[] baseAmountEnemiesPerWave;
    [SerializeField] private float intervalTime;


    //private List<GameObject> currentMinionsList;
    [SerializeField] private GameObject txtBossIsComing;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject bossCam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private Transform spawnBoss;
    [SerializeField] private List<GameObject> minionsPrefabList;
    [SerializeField] private List<Transform> spawnList;
    [SerializeField] private Text currentWaveTxt;
    [SerializeField] private Text currentAmountEnemiesTxt;
    [SerializeField] private Text currentTimeTxt;

    //private void Awake()
    //{
    //    if (wsc != null)
    //    {
    //        wsc = this;
    //    }
    //}

    void Start()
    {
        //formula for amount enemies = actualWave * baseAmountEnemies * amountPlayers

        amountPlayers = 1; //get it in game manager when implement multiplayer        
        currentWave = 0;
        totalWave = 5;
        intervalCountTime = 5f; //just for first wave
        isWaveFinished = true;
        currentTimeTxt.text = intervalCountTime.ToString("00");
    }


    void Update()
    {
        if (isWaveFinished)
        {
            if (currentWave < totalWave)
            {
                intervalCountTime -= Time.deltaTime;
                currentTimeTxt.text = intervalCountTime.ToString("00");
            }
            if(currentWave == totalWave)
            {
                isWaveFinished = false;
                FindObjectOfType<GameManager>().ShowVictory();
            }
        }

        if (intervalCountTime <= 0)
        {
            currentWave++;
            currentWaveTxt.text = currentWave.ToString();
            intervalCountTime = intervalTime;

            if (currentWave < totalWave)
            {
                EnemyGeneratorConfig();
            }

            if (currentWave == 5)
            {
                StartCoroutine(BossGenerator());
            }
        }

        if (currentAmountEnemies > 0)
            EnemyGeneratorInWaveForDemand();
    }

    IEnumerator BossGenerator()
    {
        isWaveFinished = false;
        currentAmountEnemies = 1;
        currentAmountEnemiesTxt.text = currentAmountEnemies.ToString();

        txtBossIsComing.SetActive(true);
        yield return new WaitForSeconds(3f);
        txtBossIsComing.SetActive(false);

        //Camera.main.enabled = false;
        playerCam.SetActive(false);
        bossCam.SetActive(true);

        PlayerController player = FindObjectOfType<PlayerController>();
        //Transform oldPosCam = Camera.main.transform; //corrigir isto
        GameObject boss = Instantiate(bossPrefab, spawnBoss.position, Quaternion.identity);

        boss.GetComponent<GreenBoss>().canMove = false;
        player.canMove = false;

        //Camera.main.transform.position = new Vector3(spawnBoss.position.x, spawnBoss.position.y);
        yield return new WaitForSeconds(3.2f);
        //Camera.main.transform.position = oldPosCam.position;
        
        bossCam.SetActive(false);
        playerCam.SetActive(true);

        boss.GetComponent<GreenBoss>().canMove = true;
        player.canMove = true;
    }

    private void EnemyGeneratorConfig()
    {
        isWaveFinished = false;
        spawnCycle = 1;
        canRunCycle = true;
        switch (amountPlayers)
        {
            case 1:
                WaveLengthModifierAmountPlayers = 1f;
                break;

            case 2:
                WaveLengthModifierAmountPlayers = 2f;
                break;

            case 3:
                WaveLengthModifierAmountPlayers = 2.5f;
                break;

            case 4:
                WaveLengthModifierAmountPlayers = 3f;
                break;

            default:
                break;
        }


        totalAmountEnemies = (int)(baseAmountEnemiesPerWave[currentWave - 1] * WaveLengthModifierAmountPlayers);
        currentAmountEnemies = totalAmountEnemies;
        currentAmountEnemiesTxt.text = currentAmountEnemies.ToString();
    }

    private void EnemyGeneratorInWaveForDemand()
    {
        if (spawnCycle <= 3 && canRunCycle)
        {
            if (spawnCycle < 3)
            {
                spawnCycle++;
                canRunCycle = false;
                StartCoroutine(StartSpawnCycle());
            }
            else
            {
                spawnCycle++;
                canRunCycle = false;
                StartCoroutine(LastSpawnCycle());
            }
        }
    }

    public void UpdateAmountEnemiesInScene()
    {
        currentAmountEnemies--;
        amountEnemiesSpawnedInScene--;

        if (spawnCycle <= 3 && amountEnemiesSpawnedInScene <= snapshotAmountEnemiesSpawnedInScene / 2)
        {
            canRunCycle = true;
        }

        if (currentAmountEnemies == 0)
            isWaveFinished = true;

        currentAmountEnemiesTxt.text = currentAmountEnemies.ToString();
    }

    IEnumerator StartSpawnCycle()
    {
        amountEnemiesToSpawn = Mathf.FloorToInt(totalAmountEnemies / 3f);

        amountEnemiesSpawnedInScene += amountEnemiesToSpawn;
        snapshotAmountEnemiesSpawnedInScene = amountEnemiesSpawnedInScene;

        //for (int i = 1; i <= Mathf.FloorToInt(totalAmountEnemies / 3f); i++)
        for (int i = 1; i <= amountEnemiesToSpawn; i++)
        {
            Instantiate(minionsPrefabList[GenerateIndexMinionList()], spawnList[Random.Range(0, spawnList.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(timeSpawnEnemy);
        }
    }

    IEnumerator LastSpawnCycle()
    {
        int lastAmountEnemiesToSpawn = totalAmountEnemies - (Mathf.FloorToInt(totalAmountEnemies / 3f) * 2);

        amountEnemiesSpawnedInScene += lastAmountEnemiesToSpawn;

        for (int i = 1; i <= lastAmountEnemiesToSpawn; i++)
        {
            Instantiate(minionsPrefabList[GenerateIndexMinionList()], spawnList[Random.Range(0, spawnList.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(timeSpawnEnemy);
        }
    }

    private int GenerateIndexMinionList()
    {
        switch (currentWave)
        {
            case 1:
                return 0;

            case 2:
                return 1;

            case 3:
                return 2;

            default:
                return Random.Range(0, minionsPrefabList.Count);
        }
    }

}
