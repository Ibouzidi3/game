using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    public GameObject bottle;
    public GameObject[] bottles;
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
            bottle.SetActive(false);
            bottle = GetRandomBottle();
            bottle.SetActive(true);
            EventManager.TriggerEvent<BoxDestructionEvent, Vector3>(collider.transform.position);
            animator.SetBool("destroyed", true);
        }
    }

    private GameObject GetRandomBottle()
    {
        if (bottles == null || bottles.Length == 0)
            return bottle;
        return bottles[Random.Range(0, bottles.Length)];
    }
}
