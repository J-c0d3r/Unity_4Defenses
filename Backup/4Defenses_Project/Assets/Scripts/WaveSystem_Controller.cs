using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem_Controller : MonoBehaviour
{
    public static WaveSystem_Controller instance;

    private bool isPlayingIntervalWaves;
    private bool isWaveFinished;
    private bool canRunCycle;
    private int amountPlayers;
    private int spawnCycle;
    private float WaveLengthModifierAmountPlayers;
    private int amountEnemiesToSpawn;
    private int amountEnemiesSpawnedInScene;
    private int snapshotAmountEnemiesSpawnedInScene;
    [SerializeField] private int currentWave; //temporary
    [SerializeField] private int totalWave;
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
    [SerializeField] private GameObject waveTxt;
    [SerializeField] private GameObject bossTxt;
    [SerializeField] private Text totalWaveTxt;
    [SerializeField] private Text currentWaveTxt;
    [SerializeField] private Text currentAmountEnemiesTxt;
    [SerializeField] private Text currentTimeTxt;


    [Header("Audios")]
    [SerializeField] private AudioClip bossSong;
    [SerializeField] private AudioClip intervalWave;
    [SerializeField] private List<AudioClip> waveSongs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //formula for amount enemies = actualWave * baseAmountEnemies * amountPlayers

        amountPlayers = 1; //get it in game manager when implement multiplayer        
        currentWave = 0;
        //totalWave = 5;
        intervalCountTime = 5f; //just for first wave
        isWaveFinished = true;
        currentTimeTxt.text = intervalCountTime.ToString("00");

        bossTxt.SetActive(false);
        waveTxt.SetActive(true);
        totalWaveTxt.text = totalWave.ToString();
    }


    void Update()
    {
        //interval waves
        if (isWaveFinished)
        {
            if (currentWave <= totalWave)
            {
                intervalCountTime -= Time.deltaTime;
                currentTimeTxt.text = intervalCountTime.ToString("00");

                if (!isPlayingIntervalWaves)
                {
                    isPlayingIntervalWaves = true;
                    Audio_Controller.instance.PlayMusic(intervalWave, true);
                }                
            }
            if (currentWave == totalWave + 1)
            {
                isWaveFinished = false;
                //FindObjectOfType<GameManager>().ShowVictory();
                GameManager.instance.ShowVictory();
            }
        }

        //start new wave
        if (intervalCountTime <= 0)
        {
            currentWave++;
            currentWaveTxt.text = currentWave.ToString();
            intervalCountTime = intervalTime;

            if (currentWave <= totalWave)
            {
                EnemyGeneratorConfig();
            }

            if (currentWave == totalWave + 1)
            {
                StartCoroutine(BossGenerator());
            }
        }

        if (currentAmountEnemies > 0)
            EnemyGeneratorInWaveForDemand();
    }

    IEnumerator BossGenerator()
    {
        bossTxt.SetActive(true);
        waveTxt.SetActive(false);
        Audio_Controller.instance.PlayMusic(bossSong, true);
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
        isPlayingIntervalWaves = false;
        Audio_Controller.instance.PlayMusic(waveSongs[Random.Range(0, waveSongs.Count)], true);
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

        if (currentAmountEnemiesTxt != null)
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
