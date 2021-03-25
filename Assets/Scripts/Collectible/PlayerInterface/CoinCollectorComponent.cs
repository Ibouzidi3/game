using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    private int numberOfCoins = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Coin"))
        {

            EventManager.TriggerEvent<CoinCollectionEvent, Vector3>(other.transform.position);
            other.gameObject.SetActive(false);
            numberOfCoins++;
        }

    }
}
