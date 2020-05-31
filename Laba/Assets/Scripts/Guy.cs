using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Guy : MonoBehaviour
{
    public GameObject shutteredCapsule;
    public Transform spawnPoint;
    public bool isPlayer = false;
    public float maxHealth = 10;
    public float currentHealth = 10;
    public Weapon weapon;
    public Rigidbody rb;

    public Spawner spawner;

    public event DieEvent DieEvent;

    // Start is called before the first frame update
    void Start()
    {
        this.weapon = this.GetComponentInChildren<Weapon>();
        this.rb = this.GetComponent<Rigidbody>();

        if (spawner != null)
        {
            this.DieEvent += spawner.OnEnemyDied;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isPlayer)
        {
            Debug.LogWarning("GAME OVER!");
            this.transform.position = spawnPoint.position;
            Spawner.SCORE_POINTS = 0;
            currentHealth = maxHealth;
        }
        else
        {
            DieEvent?.Invoke(this.gameObject);
            GameObject go = GameObject.Instantiate(shutteredCapsule);
            go.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }


}
public delegate void DieEvent(GameObject enemy);
