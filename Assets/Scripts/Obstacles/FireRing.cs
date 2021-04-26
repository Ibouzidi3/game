using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    public GameObject smokeEffect;
    private ParticleSystem particle;

    void OnParticleCollision(GameObject other)
    {
        Rigidbody body = other.GetComponent<Rigidbody>();
        if (other.tag == "Player")
        {
            Debug.Log("Player touched Fire Ring");
            EventManager.TriggerEvent<FlameEvent, Vector3>(other.transform.position);
            GameObject smoke = Instantiate(smokeEffect, other.transform.position, Quaternion.identity);
            particle = smoke.GetComponent<ParticleSystem>();
            particle.Play();
        }
    }
}
