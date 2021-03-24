using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumAnimation : MonoBehaviour
{
    private Animator anim;


    void Awake()
    {

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");


    }

    void Start()
    {
        anim.SetBool("isActive", false);

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {

            if (c.gameObject.tag == "Player")
            { 
                anim.SetBool("isActive", true);
                EventManager.TriggerEvent<SwingingPendulumEvent, Vector3>(transform.position);

            }


        }


    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            if (c.gameObject.tag == "Player")
            { 
                anim.SetBool("isActive", false);

            }


        }

    }

}
