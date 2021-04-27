using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeLine : MonoBehaviour
{
    public GameObject instruction; 


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instruction.SetActive(false);  

        }


    }
}
