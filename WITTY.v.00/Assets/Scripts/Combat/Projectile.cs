using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed =1;
    Health target = null;
    float damage =0;

    // Update is called once per frame
    void Update()
    {
        if(target ==null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward*speed*Time.deltaTime);//move to the targer

    }
    public void SetTarget(Health target,float damage)
    {
        this.target=target;
        this.damage = damage;
    }
    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule==null) {return target.transform.position;}
        return target.transform.position + Vector3.up * targetCapsule.height/2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>()!=target) return; // if collision object has health component take damage if not return null
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}}
