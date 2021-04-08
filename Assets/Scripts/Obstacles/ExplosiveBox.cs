using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : MonoBehaviour
{
	// Start is called before the first frame update    CFX_GroundSmokeExplosionAlt

	public GameObject explosionEffect;

	private ParticleSystem particle;

    private void OnTriggerEnter(Collider collider)
    {
        destroyBox(collider);



    }

    private void OnTriggerStay(Collider collider)
    {
        destroyBox(collider);
    }

    private void destroyBox(Collider collider)
    {
        Animator animator = this.GetComponent<Animator>();
        bool destroyed = animator.GetBool("destroyed");
        if (!destroyed)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            particle = explosion.GetComponent<ParticleSystem>();
            particle.Play(); 
            EventManager.TriggerEvent<ExplosiveBoxEvent, Vector3>(collider.transform.position);
            EventManager.TriggerEvent<BoxDestructionEvent, Vector3>(collider.transform.position);

            animator.SetBool("destroyed", true);
            transform.gameObject.SetActive(false);
        }
    }

}
