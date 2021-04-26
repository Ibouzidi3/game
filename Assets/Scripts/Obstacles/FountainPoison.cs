using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainPoison : MonoBehaviour
{
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            EventManager.TriggerEvent<FountainSplashEvent, Vector3>(transform.position);
            other.SendMessage("ShootPlayer", other);

        }

    } 
}
