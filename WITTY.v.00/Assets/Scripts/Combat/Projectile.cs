using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Attributes;

namespace RPG.Combat
{
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed =1;

    [SerializeField] bool isHoming = true;
    [SerializeField] GameObject hitEffect =null;
    [SerializeField] float maxLifeTime=10;//destroy projectiles after a while
    [SerializeField] GameObject[] destroyOnHit=null;
    [SerializeField] float lifeAfterImpact =2;//bunu yanar görünüm için arttır
    [SerializeField] UnityEvent onHit;
    Health target = null;
   Vector3 targetPoint;
    GameObject instigator=null;
    float damage =0;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }
    // Update is called once per frame
    void Update()
    {
          if (target != null && isHoming && !target.IsDead())
       {
         transform.LookAt(GetAimLocation());
       }
        transform.Translate(Vector3.forward*speed*Time.deltaTime);//move to the targer

    }
    public void SetTarget(Health target,GameObject instigator, float damage)
     {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, null, targetPoint);
        }

        public void SetTarget(GameObject instigator, float damage, Health target=null, Vector3 targetPoint=default)
    {
        this.target=target;
          this.targetPoint = targetPoint;
        this.damage = damage;
        this.instigator=instigator;

        Destroy(gameObject,maxLifeTime);
    }
    private Vector3 GetAimLocation()
    { if (target == null)
            {
                return targetPoint;
            }
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule==null) {return target.transform.position;}
        return target.transform.position + Vector3.up * targetCapsule.height/2;
    }
    private void OnTriggerEnter(Collider other)
    {
             Health health = other.GetComponent<Health>();
            if (target != null && health != target) return;
            if (health == null || health.IsDead()) return;
            if (other.gameObject == instigator) return;
            health.TakeDamage(instigator, damage);

        speed=0;
        onHit.Invoke();
        if(hitEffect!=null)
        {
            Instantiate(hitEffect,GetAimLocation(),transform.rotation);
        }
        foreach (GameObject toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }

        Destroy(gameObject,lifeAfterImpact);
    }
}}
