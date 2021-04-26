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
            other.SendMessage("ShootPlayer", other);

        }

    } 
}
