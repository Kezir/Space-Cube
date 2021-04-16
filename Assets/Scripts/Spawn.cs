using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    public GameObject[] bossPrefabs;
    public Wave_Data[] wavesData;
    public GameObject[] Waypoints;
    public GameObject[] SetPoints;
    public GameObject[] Weapons;
    public GameObject Player;
    public GameObject[] SpiralStartPoints;

    private GameObject[] respawns, powers;
    private GameObject Waypoint;
    private GameObject[] enemyForWave;
    private string[] possibleWaves = { "Waypoints", "Wave", "Spiral" };

    private float spawnRangeX = 2.5f;
    private float spawnPozY = 6.0f;
    private float startDelay = 2;
    private float spawnInterval = 3f;
    private int childCount;
    public int wave;
    public int score;
    private Vector3 spawnPos;
    private float timeDelay;
    private bool powerUp, nextWave;
    private Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Player, new Vector3(0, -3.23f, 0), Player.transform.rotation);
        GameManager.Instance.Score = score;
        wave = 0;
        powerUp = true;
        nextWave = true;
        timeDelay = 0.0f;
        wavesData = new Wave_Data[30];
        SetWavesData();
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        respawns = GameObject.FindGameObjectsWithTag("Enemy");
        powers = GameObject.FindGameObjectsWithTag("Power");

        if (nextWave == true)
        {
            nextWave = false;
            wave++;
            //Debug.Log(wave);
            if(wave < 10 && powerUp == false)
            {
                SpawnRandomWeapon();
                powerUp = true;
                timeDelay = 6.0f;
            }

            if(wave == 10 || wave == 20)
            {
                spawnPos = new Vector3(0f, 6f, 0f);
                SpawnBoss();
                Invoke("SpawnNextWave", 6f);
            }
            else
            {
                Invoke("SpawnNextWave", 0f);

            }
            
        }
        else if(respawns.Length == 0 && powers.Length == 0)
        {
            nextWave = true;
        }
        
    }
    
    private void SpawnNextWave()
    {
        if (wavesData[wave].way == "Waypoints")
        {
            Waypoint = GetWaypoint();
            spawnPos = Waypoint.transform.GetChild(0).position;
            childCount = Waypoint.transform.childCount;
            InvokeRepeating("SpawnWaypoints", 2, 0.5f);

        }
        else if (wavesData[wave].way == "Wave")
        {
            Waypoint = SetPoints[Random.Range(0, SetPoints.Length)];
            spawnPos = new Vector3(Random.Range(-2.0f, 2.0f), 6.0f, 0.0f);
            childCount = Waypoint.transform.childCount;
            InvokeRepeating("SpawnWave", 2, 0.5f);

        }
        else if (wavesData[wave].way == "Spiral")
        {
            Waypoint = SpiralStartPoints[Random.Range(0, SpiralStartPoints.Length)];
            spawnPos = Waypoint.transform.position;
            childCount = 10;
            InvokeRepeating("SpawnSpiral", 2, 1.0f);

        }
        else if (wavesData[wave].way == "Random")
        {
            childCount = 20;
            InvokeRepeating("SpawnRandom", 2, 1.0f);

        }
        powerUp = false;
    }
    private void SpawnRandomWeapon()
    {
        int weapon = Random.Range(0, Weapons.Length);
        float Y = 6.0f;
        float X = Random.Range(-2, 2);
        Instantiate(Weapons[weapon], new Vector3(X,Y,0), Weapons[weapon].transform.rotation);

    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(wavesData[wave].enemy[0], spawnPos, wavesData[wave].enemy[0].transform.rotation);
        Enemy _boss = boss.GetComponent<Enemy>();
        _boss.type = "Boss";
        _boss.movement = "FreeRoam";
    }
    void SpawnSpiral()
    {
        if (childCount-- == 0)
            CancelInvoke("SpawnSpiral");
        else
        {
            int index = Random.Range(0, wavesData[wave].enemy.Length);
            GameObject enemy1 = Instantiate(wavesData[wave].enemy[index], spawnPos, wavesData[wave].enemy[index].transform.rotation);
            Enemy _enemy1 = enemy1.GetComponent<Enemy>();
            _enemy1.type = "Soldier";
            _enemy1.movement = "Spiral";
            _enemy1.Waypoint = Waypoint;
            _enemy1.time = 3.14f;

            GameObject enemy2 = Instantiate(wavesData[wave].enemy[index], spawnPos, wavesData[wave].enemy[index].transform.rotation);
            Enemy _enemy2 = enemy2.GetComponent<Enemy>();
            _enemy2.type = "Soldier";
            _enemy2.movement = "Spiral";
            _enemy2.Waypoint = Waypoint;
            _enemy2.time = 0f;
        }
    }
    void SpawnWave()
    {
        if (childCount-- == 0)
            CancelInvoke("SpawnWave");
        else
        {
            int index = Random.Range(0, wavesData[wave].enemy.Length);
            GameObject enemy = Instantiate(wavesData[wave].enemy[index], spawnPos, wavesData[wave].enemy[index].transform.rotation);
            Enemy _enemy = enemy.GetComponent<Enemy>();
            _enemy.type = "Soldier";
            _enemy.movement = "Wave";
            _enemy.targetPosition = (Vector2)Waypoint.transform.GetChild(childCount).position;
            _enemy.Waypoint = Waypoint;
        }
              
        
    }

    void SpawnRandom()
    {
        if (childCount-- == 0)
            CancelInvoke("SpawnRandom");
        else
        {
            spawnPos = new Vector3(Random.Range(-2.6f, 2.6f), 6f, 0f);
            int index = Random.Range(0, wavesData[wave].enemy.Length);
            GameObject enemy = Instantiate(wavesData[wave].enemy[index], spawnPos, wavesData[wave].enemy[index].transform.rotation);
            Enemy _enemy = enemy.GetComponent<Enemy>();
            _enemy.type = "Soldier";
            _enemy.movement = "Random";
            _enemy.speed = 0.75f;
            
        }
    }

    void SpawnWaypoints()
    {
        if (childCount-- == 0)
            CancelInvoke("SpawnWaypoints");
        else
        {
            int index = Random.Range(0, wavesData[wave].enemy.Length);
            GameObject enemy = Instantiate(wavesData[wave].enemy[index], spawnPos, wavesData[wave].enemy[index].transform.rotation);
            Enemy _enemy = enemy.GetComponent<Enemy>();
            _enemy.type = "Soldier";
            _enemy.movement = "Waypoints";
            _enemy.Waypoint = Waypoint;
        }
    }
    public GameObject GetWaypoint()
    {
        int random = Random.Range(0, Waypoints.Length);
        return Waypoints[random];

    }


    private void SetWavesData()
    {
        for(int i = 1;i<wavesData.Length -1;i++)
        {
            if (i == 10 || i == 20)
            {
                wavesData[i] = new Wave_Data(bossPrefabs,"Boss","FreeRoam");
            }
            else
            {
                if(i < 2)
                {
                    enemyForWave = new GameObject[] { enemiesPrefabs[0] };
                    possibleWaves = new string[] {"Waypoints"};
                }
                else if(i<5)
                {
                    enemyForWave = new GameObject[] { enemiesPrefabs[0], enemiesPrefabs[1] };
                    possibleWaves = new string[] { "Waypoints", "Wave" };
                }
                else if(i < 10)
                {
                    enemyForWave = new GameObject[] { enemiesPrefabs[0], enemiesPrefabs[1] };
                    possibleWaves = new string[] { "Waypoints", "Wave", "Random" };
                }
                else if (i < 20 && i != 10)
                {
                    enemyForWave = new GameObject[] { enemiesPrefabs[0], enemiesPrefabs[1], enemiesPrefabs[2] };
                    possibleWaves = new string[] { "Waypoints", "Wave", "Spiral", "Random" };
                }
                wavesData[i] = new Wave_Data(enemyForWave, "Soldier", possibleWaves[Random.Range(0,possibleWaves.Length)]);

            }
            
        }
    }
}
