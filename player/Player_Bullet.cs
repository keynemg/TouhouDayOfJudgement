using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public bool lightOrDark;
    Rigidbody rb;
    public float bullet_Speed;
    public Vector3 dir;
    public string enemyTag;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,1f);
    }

    void Update()
    {
        rb.velocity = dir * bullet_Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            other.GetComponent<Enemy_Controller>().OnHit(lightOrDark);
            Destroy(gameObject);
        }
    }
}
