using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExecutePlayerFootstep()
    {

        EventManager.TriggerEvent<FootStepEvent, Vector3>(transform.position);
    }
}