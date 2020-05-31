using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isAlive = true;
    public float lifeTime = 5;
    public float damage = 10;
    private float time = 0;

    private SphereCollider bulletCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        this.bulletCollider = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (isAlive)
        {
            Guy g = collision.collider.GetComponent<Guy>();
            if (g != null)
            {
                g.DealDamage(damage);
            }
        }
        isAlive = false;
        Destroy(this.gameObject);
    }
}
