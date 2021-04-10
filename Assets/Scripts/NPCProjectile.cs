using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProjectile : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("ShootPlayer", collision.collider); 
        }


    }

}
