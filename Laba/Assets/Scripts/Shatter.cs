using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public float explotionForce = 18500;
    public float lifeTime = 6;
    public float timer = 0;

    List<GameObject> childs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject go = this.transform.GetChild(i).gameObject;
            childs.Add(go);
        }
        this.transform.DetachChildren();

        for (int i = 0; i < childs.Count; i++)
        {
            GameObject go = childs[i];
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddExplosionForce(explotionForce, this.transform.position, 38);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                Destroy(childs[i].gameObject);
            }
            Destroy(this.gameObject);
            childs.Clear();
        }

    }
}
