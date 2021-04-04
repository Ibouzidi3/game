using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepEmitter : MonoBehaviour
{
    public bool waterSurface = false; 


    // Start is called before the first frame update
    public void ExecutePlayerFootstep()
    {
        if (waterSurface)
        {
            EventManager.TriggerEvent<FootStepWaterEvent, Vector3>(transform.position);
        }
        else
        {
            EventManager.TriggerEvent<FootStepEvent, Vector3>(transform.position);
        }
    }
}