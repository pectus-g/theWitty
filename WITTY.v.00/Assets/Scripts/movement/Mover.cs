 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Attributes;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction, ISaveable
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 6f;
    NavMeshAgent navMeshAgent;
    Health health;
    void Awake(){
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

    public object CaptureState()//what you want to save this field will capture but must have serialiblevector 3
    {
    Dictionary<string,object> data =new Dictionary<string,object>();
    data["position"] = new SerializableVector3(transform.position);
    data["rotation"] = new SerializableVector3(transform.eulerAngles);
    return data;
//return new SerializableVector3(transform.position);//captures pos
    }
    public void RestoreState(object state)
    {//this is optional for capturing multiple parameters
            Dictionary<string,object> data =(Dictionary<string,object>)state;
            GetComponent<NavMeshAgent>().enabled=false;
            transform.position=((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles=((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled=true;
            /*SerializableVector3 position =(SerializableVector3)state;//approve the data is serialized
            GetComponent<NavMeshAgent>().enabled=false;//navmesh sometimes gives some probles we will froze for a moment
            transform.position=position.ToVector();
            GetComponent<NavMeshAgent>().enabled=true;*/
    }
}

} 