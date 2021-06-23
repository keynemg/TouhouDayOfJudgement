using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Controller : Enemy
{
    public bool enemy_LightOrDark;
    public Transform target;
    public AudioClip successHitSFX;
    public AudioClip failHitSFX;
    public GameObject enemy_Shot_Prefab;
    public float FoV = 50;
    public float dmg = 10;
    public float atkspeed = 1f;
    public float counter;

    void FixedUpdate()
    {
        if (target)
        {
            GoToLocation(target);
        }
        if (myNavMeshAgent.destination != null)
        {
           //transform.LookAt(myNavMeshAgent.destination);
        }
    }

    public void OnHit(bool _lightOrDark)
    {
        if (enemy_LightOrDark == _lightOrDark)
        {
            AudioManager.instance.PlaySingle(successHitSFX);
            transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
            OnDeath();
        }
        else
        {
            AudioManager.instance.PlaySingle(failHitSFX);
            transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
            GameObject shot = Pooling.Instantiate(enemy_Shot_Prefab,transform.position,transform.rotation);
            if (Player_Stats.Instance)
            {
                shot.GetComponent<Enemy_Bullet>().target = Player_Stats.Instance.gameObject;
            }
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Enemy_Controller>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player_Stats.Instance.RecieveDamage(1);
            OnDeath();
        }             
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aggro"))
        {
            target = Player_Stats.Instance.transform;
        }
    }

    IEnumerator Aggrodash()
    {
        yield return new WaitForSeconds(1f);
    }

}
