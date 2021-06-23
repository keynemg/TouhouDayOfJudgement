using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entities
{
    public Rigidbody myRigidBody;
    //public Animator MyAnimator;
    public NavMeshAgent myNavMeshAgent;
    public int e_Gold_Dropped;

    [Range(0,1)][SerializeField]
    private float damageResistance = 0;

    public override void Awake()
    {
        base.Awake();
        //MyAnimator = GetComponent<Animator>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    public void GoToLocation(Transform Destination)
    {
        if (myNavMeshAgent)
        {
            myNavMeshAgent.SetDestination(Destination.position);
        }
    }

    public override void RecieveDamage(float damage)
    {
        base.RecieveDamage(damage * (1 - damageResistance));
    }

    public override void OnDeath()
    {
        base.OnDeath();
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2);
    }
}
