using UnityEngine;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int currentWave = 0;
    public int maxWaves = 10;

    public int waveBudget = 20;

    public Transform[] spawnPoints;

    public Transform[][] paths;

    public Transform[] leftPath;
    public Transform[] rightPath;

    public GameObject[] enemyPrefabs;

    public TMP_Text waveText;

    private bool waveRunning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateWaveUI();
    }

    void Update()
    {
        if (currentWave >= maxWaves && !waveRunning)
        {
            GameManager.Instance.WinGame();
        }
    }

    public void StartNextWave()
    {
        if (waveRunning)
            return;

        if (currentWave >= maxWaves)
        {
            Debug.Log("All waves completed!");
            return;
        }

        currentWave++;
        waveRunning = true;

        waveBudget = 10 + currentWave * 5;

        UpdateWaveUI();

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (waveBudget > 0)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            Transform spawn = spawnPoints[spawnIndex];

            Transform[] path =
                (spawnIndex == 0) ? leftPath : rightPath;

            GameObject enemy = Instantiate(
                enemyPrefabs[enemyIndex],
                spawn.position,
                Quaternion.identity
            );

            Move move = enemy.GetComponent<Move>();

            if (move != null)
            {
                move.waypoints = path;
            }

            waveBudget--;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitUntil(() =>
            FindObjectsOfType<EnemyHealth>().Length == 0
        );

        waveRunning = false;
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
            waveText.text = $"Wave: {currentWave}/{maxWaves}";
    }
}