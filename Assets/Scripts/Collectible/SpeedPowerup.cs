using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    public void Start()
    {
    }
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
        SpeedCollector speedCollector = collider.GetComponent<SpeedCollector>();
        speedCollector.SpeedUp();

    }

    private bool isAbleToCollectPowerup(Collider collider)
    {
        return collider.GetComponent<SpeedCollector>() != null; 

    }
}
