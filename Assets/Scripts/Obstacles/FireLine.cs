using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject fire = GameObject.FindGameObjectWithTag("Fire");
            if (fire.activeSelf)
            {
                other.SendMessage("ShootPlayer", other);
            }
            
        }


    }
}
