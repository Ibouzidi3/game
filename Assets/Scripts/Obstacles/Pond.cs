using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{

    public GameObject splashEffect;
    private ParticleSystem particle;
 
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FootStepEmitter footstep = collider.GetComponent<FootStepEmitter>();
            footstep.waterSurface = true;
        }

        playSplashEffect(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            FootStepEmitter footstep = collider.GetComponent<FootStepEmitter>();
            footstep.waterSurface = false;
        }

        playSplashEffect(collider);
    }

    private void playSplashEffect (Collider collider)
    {
        GameObject splash = Instantiate(splashEffect, collider.transform.position, Quaternion.identity);
        particle = splash.GetComponent<ParticleSystem>();
        particle.Play();

    }
}
