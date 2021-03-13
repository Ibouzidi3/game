using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public int speedUpEffect;
    private void OnTriggerEnter(Collider collider)
    {
        if (isAbleToCollectPowerup(collider))
            handleSpeedUpPowerup(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (isAbleToCollectPowerup(collider))
           handleSpeedUpPowerup(collider);
    }

    private void handleSpeedUpPowerup(Collider collider)
    {

        EventManager.TriggerEvent<SpeedPowerupEvent, Vector3>(collider.transform.position);
        SetSpeedUp(collider);
        gameObject.SetActive(false);
    }

    private void SetSpeedUp(Collider collider)
    {
        MinionBasicControlScript basicControlScript = collider.GetComponent<MinionBasicControlScript>();
        basicControlScript.SpeedUp(speedUpEffect);
    }

    private bool isAbleToCollectPowerup(Collider collider)
    {
        return collider.GetComponent<MinionBasicControlScript>() != null; 

    }
}
