using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float bullet_Speed;
    public GameObject target;
    float timer = 0f;
    Vector3 spawnpos;

    private void Start()
    {
        spawnpos = transform.position;
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(spawnpos, target.transform.position, timer);
            timer += Time.deltaTime * 30 / (Vector3.Distance(spawnpos, target.transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Stats.Instance.RecieveDamage(1);
            Destroy(gameObject);
        }
    }
}
