using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float shotChance = 0.03f;
    public float aggroDistance = 15f;
    public float shotCooldown = 4;
    public float shotTimer = 0;

    public float moveSpeed = 5;
    public float maxRandomTurnTime = 10;
    public float minRandomTurnTime = 1.5f;

    public Guy guy;
    public Guy player;

    bool isAttacking = false;

    private float timerToTurn = 0;
    private float timeToTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.guy = this.GetComponent<Guy>();
        this.player = Spawner.INSTANCE.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Magnitude(player.transform.position - guy.transform.position) < aggroDistance)
        {
            if (Random.Range((float)0, (float)1) < shotChance)
            {
                isAttacking = true;
            }
        }
        else
        {
            isAttacking = false;
        }
        if (isAttacking)
        {
            this.guy.transform.position += this.guy.transform.right * -moveSpeed * Time.deltaTime;
            this.guy.transform.LookAt(player.transform.position + Vector3.up * 0.1f);

            shotTimer += Time.deltaTime;
            if (shotTimer >= shotCooldown)
            {
                print("Guy shot!");
                guy.weapon.Shot();
                shotTimer = 0;
            }
        }
        else
        {
            this.guy.transform.position += this.guy.transform.forward * moveSpeed * Time.deltaTime;
            this.guy.transform.LookAt(this.guy.transform.forward + this.guy.transform.position);
        }

        timerToTurn += Time.deltaTime;
        if (timerToTurn >= timeToTurn)
        {
            this.guy.transform.Rotate(0, Random.Range(0, 360), 0);
            timerToTurn = 0;
            timeToTurn = Random.Range(minRandomTurnTime, maxRandomTurnTime);
        }
    }
}
