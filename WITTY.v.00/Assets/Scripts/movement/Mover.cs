 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 6f;
    NavMeshAgent navMeshAgent;
    Health health;
    void Start(){
    navMeshAgent=GetComponent<NavMeshAgent>();
    health=GetComponent<Health>();
   }
    void Update()
    {
     navMeshAgent.enabled=!health.IsDead();
      UpdateAnimator();
       
    }
    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
         GetComponent<ActionScheduler>().StarAction(this);
       
        MoveTo(destination,speedFraction);
    }
   
    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMeshAgent.destination=destination;
        navMeshAgent.speed=maxSpeed*Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped=false;
    }
    public void Cancel(){//player must stop near of enemy//we need this to inherit drom iaction
     navMeshAgent.isStopped=true;
    }
    
    private void UpdateAnimator(){
        Vector3 velocity=navMeshAgent.velocity;
        Vector3 localVelocity=transform.InverseTransformDirection(velocity);
        float speed=localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed",speed);
    }
}

}