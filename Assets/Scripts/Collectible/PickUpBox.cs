using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
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
            EventManager.TriggerEvent<BoxDestructionEvent, Vector3>(collider.transform.position);
            animator.SetBool("destroyed", true);
        }
    }
}
