using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float health = 25;
    public MedKitSpawner spawner;
    public DieEvent DieEvent;

    public float magnitude = 0.7f;

    public Transform gfx;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner != null)
        {
            this.DieEvent += spawner.OnMedkitPick;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gfx.localPosition = new Vector3(0, (Mathf.Sin(Time.time) * magnitude) + 1.25f, 0);
        gfx.Rotate(0, 2.5f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController pc = collision.collider.GetComponent<PlayerController>();
        if(pc != null && isAlive)
        {
            pc.guy.currentHealth = Mathf.Clamp(pc.guy.currentHealth + health, 0, pc.guy.maxHealth);
            isAlive = false;
            DieEvent?.Invoke(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
