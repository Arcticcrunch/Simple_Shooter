using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner INSTANCE;
    public static int SCORE_POINTS = 0;

    public int maxEnemies = 20;
    public GameObject enemy;
    public Guy player;
    public Text score;

    //public int scorePoints = 0;
    public float minSpawnTime = 3;
    public float maxSpawnTime = 9;

    public int aliveEnemies = 0;
    private float spawnTimer = 0;
    private float selectedSpawnTime = 0;
    private int currentEnemies = 0;
    private bool isSpawning = false;

    public float addHealth = 15;
    public int addHealthKills = 10;
    public int addHealthCounter = 0;

    public float width = 0;
    public float height = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawner.INSTANCE = this;
        width = this.transform.localScale.x;
        height = this.transform.localScale.z;
        PopulateStartEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveEnemies != maxEnemies && isSpawning == false)
        {
            isSpawning = true;
            selectedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        if(isSpawning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= selectedSpawnTime)
            {
                spawnTimer = 0;
                isSpawning = false;
                SpawnEnemy();
            }
        }

        score.text = Spawner.SCORE_POINTS.ToString();
    }

    void PopulateStartEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(0, width), this.transform.position.y + (this.transform.localScale.y * 0.5f), Random.Range(0, height));
        pos.x = this.transform.position.x + pos.x - (pos.x * 0.5f);
        pos.z = this.transform.position.z + pos.z - (pos.z * 0.5f);

        Ray ray = new Ray(pos, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 60000))
        {
            GameObject.Instantiate(enemy);
            enemy.transform.position = hit.point + Vector3.up * 0.08f;
            enemy.GetComponent<Guy>().spawner = this;
            aliveEnemies++;
            //print("Enemy spawned");
        }
    }

    public void OnEnemyDied(GameObject enemy)
    {
        aliveEnemies--;
        Spawner.SCORE_POINTS++;
        addHealthCounter++;
        if (addHealthCounter >= addHealthKills)
        {
            player.currentHealth = Mathf.Clamp(player.currentHealth + addHealth, 0, player.maxHealth);
            addHealthCounter = 0;
        }
        //print("Enemy died!");
    }
}
