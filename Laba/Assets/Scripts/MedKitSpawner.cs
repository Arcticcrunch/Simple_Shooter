using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedKitSpawner : MonoBehaviour
{
    public static MedKitSpawner INSTANCE;

    public int maxMedKits = 15;
    public GameObject medkit;
    //public Guy player;
    //public Text score;

    //public int scorePoints = 0;
    public float minSpawnTime = 5;
    public float maxSpawnTime = 20;

    public int aliveMedkits = 0;
    private float spawnTimer = 0;
    private float selectedSpawnTime = 0;
    //private int currentEnemies = 0;
    private bool isSpawning = false;

    public float width = 0;
    public float height = 0;
    // Start is called before the first frame update
    void Start()
    {
        MedKitSpawner.INSTANCE = this;
        width = this.transform.localScale.x;
        height = this.transform.localScale.z;
        PopulateStartMedkits();
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveMedkits != maxMedKits && isSpawning == false)
        {
            isSpawning = true;
            selectedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        if (isSpawning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= selectedSpawnTime)
            {
                spawnTimer = 0;
                isSpawning = false;
                SpawnMedkit();
            }
        }
    }

    void PopulateStartMedkits()
    {
        for (int i = 0; i < maxMedKits; i++)
        {
            SpawnMedkit();
        }
    }

    void SpawnMedkit()
    {
        Vector3 pos = new Vector3(Random.Range(0, width), this.transform.position.y + (this.transform.localScale.y * 0.5f), Random.Range(0, height));
        pos.x = this.transform.position.x + pos.x - (pos.x * 0.5f);
        pos.z = this.transform.position.z + pos.z - (pos.z * 0.5f);

        Ray ray = new Ray(pos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 60000))
        {
            GameObject.Instantiate(medkit);
            medkit.transform.position = hit.point + Vector3.up * 0.08f;
            medkit.GetComponent<Medkit>().spawner = this;
            //enemy.GetComponent<Guy>().spawner = this;
            aliveMedkits++;
            //print("Enemy spawned");
        }
    }

    public void OnMedkitPick(GameObject medkit)
    {
        aliveMedkits--;
        //Spawner.SCORE_POINTS++;
        //print("Enemy died!");
    }
}
