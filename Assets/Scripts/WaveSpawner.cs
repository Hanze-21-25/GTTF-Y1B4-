using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 10f;

    public Text waveCountdownText;

    private int waveIndex = 0;
    private int waveUIc = 1;
    private void Start()
    {
        EnemiesAlive = 0;
    }


    void Update ()
    {
        if (EnemiesAlive <= 0)
        {
            if (waveIndex == waves.Length)
            {
                this.enabled = false;
                FindObjectOfType<GameManager>().WinLevel();
            }
        }
        if (EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex >= waves.Length)
        {
            return;
        }

            if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("Wave " + waveUIc + " in: " + "{0:0.0}", countdown);
    }

    IEnumerator SpawnWave ()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
        PlayerStats.Rounds++;
        waveUIc = PlayerStats.Rounds + 1;
    }

    void SpawnEnemy (GameObject enemy)
    {

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }


}
