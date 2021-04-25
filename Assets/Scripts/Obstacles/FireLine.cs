using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour
{
    public GameObject instruction;
    public GameObject fire;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            instruction.SetActive(false);
            //GameObject fire = GameObject.FindGameObjectWithTag("Fire");
            if (fire.activeSelf)
            {
                other.SendMessage("ShootPlayer", other);
            }

            
        }


    }
}
