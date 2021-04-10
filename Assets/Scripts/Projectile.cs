using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    public float speed = 10f;   // this is the projectile's speed
    public float lifespan = 3f; // this is the projectile's lifespan (in seconds)

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.Log("Projectile has no rigidbody");
    }

    void Start()
    {
        //rb.AddForce(-rb.transform.forward * speed);  
        transform.position = rb.transform.position + Camera.main.transform.forward * 2; 
        rb.velocity = Camera.main.transform.forward * 40;
        if (gameObject.tag == "Projectile")
        {
            EventManager.TriggerEvent<ProjectileEvent, Vector3>(transform.position);
        }
        else
        {
            EventManager.TriggerEvent<ProjectileFireEvent, Vector3>(transform.position);
        }
               
        Destroy(gameObject, lifespan);
    }


    private void OnCollisionEnter(Collision collision)
    {

        if ( collision.gameObject.tag == "NPC")
        {
            collision.gameObject.SetActive(false);
        }
         
    }

}

