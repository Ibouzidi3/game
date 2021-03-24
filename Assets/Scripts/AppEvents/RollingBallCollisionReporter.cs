using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallCollisionReporter : MonoBehaviour
{

    void OnCollisionEnter(Collision c)
    {

        if (c.impulse.magnitude > 0.25f)
        {
            EventManager.TriggerEvent<RollingBallEvent, Vector3>(c.transform.position);
        }
         
    }
}
