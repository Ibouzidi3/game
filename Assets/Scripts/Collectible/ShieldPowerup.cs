using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    public int shieldEffect;
    private void OnTriggerEnter(Collider collider)
    {
        if (isAbleToCollectPowerup(collider))
            handleShieldPowerup(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (isAbleToCollectPowerup(collider))
            handleShieldPowerup(collider);
    }

    private void handleShieldPowerup(Collider collider)
    {

        EventManager.TriggerEvent<ShieldPowerupEvent, Vector3>(collider.transform.position);
        SetShield(collider);
        gameObject.SetActive(false);
    }

    private void SetShield(Collider collider)
    {
        MinionBasicControlScript basicControlScript = collider.GetComponent<MinionBasicControlScript>();
        basicControlScript.ShieldPowerup(shieldEffect);
    }

    private bool isAbleToCollectPowerup(Collider collider)
    {
        return collider.GetComponent<MinionBasicControlScript>() != null;

    }
}
