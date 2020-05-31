using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shotForce = 50;

    public Transform shotPoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        GameObject b = GameObject.Instantiate(bullet);
        b.transform.position = shotPoint.position;
        Vector3 forwardDir = this.shotPoint.position - this.transform.position;
        b.transform.LookAt(this.shotPoint.position + forwardDir);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.AddForce(forwardDir.normalized * shotForce);

        AudioSource ac = this.GetComponent<AudioSource>();
        if (ac != null)
        {
            ac.Play();
        }
    }
}
