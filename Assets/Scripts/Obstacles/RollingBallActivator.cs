using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallActivator : MonoBehaviour
{

    public GameObject ball;

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            if (c.gameObject.tag == "Player")
            {
                ball.active = true; 

            }


        }


    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {

            if (c.attachedRigidbody != null)
            {
                if (c.gameObject.tag == "Player")
                {
                    ball.active = false;

                }


            }

        }

    }

}
