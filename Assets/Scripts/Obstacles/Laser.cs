using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>(); 

        if (lr == null)
            Debug.Log("LineRenderer could not be found");
    }

    // Update is called once per frame
    void Update()
    { 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                EventManager.TriggerEvent<LaserHitEvent, Vector3>(hit.point);
            }
        } 

    }
}
