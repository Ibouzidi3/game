using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{ 
    
    public GameObject projectile;    // this is a reference to your projectile prefab
    public Transform spawnTransform; // this is a reference to the transform where the prefab will spawn

    private void Update()
    {
        //CharacterControls cc = GetComponent<CharacterControls>(); 
        //if (Input.GetButtonDown("Fire1"))
        //if (cc.attack ==true)
        //{
        //    Instantiate(projectile, spawnTransform.position, spawnTransform.rotation);


       // }
    }
}