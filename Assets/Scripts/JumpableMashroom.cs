using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpableMashroom : MonoBehaviour
{

    private Animator anim;
    public float jumpForce = 5.0f;


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



    void OnCollisionEnter(Collision c )
    {
        
        if (c.collider.attachedRigidbody != null)
        {

            if (c.collider.gameObject.tag == "Player")
            {
                anim.SetBool("isActive", true);
                Vector3 velocity = c.collider.GetComponent<Rigidbody>().velocity;
                c.collider.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, Mathf.Sqrt(2 * 10.0f * jumpForce), velocity.z);
                EventManager.TriggerEvent<JumpableMashroomEvent, Vector3>(transform.position);
            }


        }


    }



    void OnCollisionExit(Collision c)
    {
        if (c.collider.attachedRigidbody != null)
        {
            if (c.collider.gameObject.tag == "Player")
            {
                anim.SetBool("isActive", false);

            }


        }

    }


}
