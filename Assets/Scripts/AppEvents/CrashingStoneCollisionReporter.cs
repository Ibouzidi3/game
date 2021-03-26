using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashingStoneCollisionReporter : MonoBehaviour
{
    



    void OnTriggerEnter(Collider c)
    {

        if (c.attachedRigidbody != null)
        {

            if (c.gameObject.tag == "Player")
            {
                EventManager.TriggerEvent<CrashingStoneEvent, Vector3>(transform.position);
            }


        }


    }

}
