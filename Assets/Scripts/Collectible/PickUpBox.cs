using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    public GameObject bottle;
    public GameObject[] bottles;
    private Vector3 originalPosition;
    private bool resetPosition = false;
    public void Update()
    {
        // this is code is to reset the position of pickup boxes with rigid body
        if (resetPosition && !IsDestroyed())
        {
            gameObject.transform.position = originalPosition;
            resetPosition = false;
        }
    }
    public void Awake()
    {
        originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

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
        if (!IsDestroyed())
        {
            bottle.SetActive(false);
            bottle = GetRandomBottle();
            bottle.SetActive(true);
            EventManager.TriggerEvent<BoxDestructionEvent, Vector3>(collider.transform.position);
            resetPosition = true;
            SetDestroyed();
        }
    }

    private GameObject GetRandomBottle()
    {
        if (bottles == null || bottles.Length == 0)
            return bottle;
        return bottles[Random.Range(0, bottles.Length)];
    }

    private bool IsDestroyed()
    {
        Animator animator = this.GetComponent<Animator>();
        return animator.GetBool("destroyed");
    }

    private void SetDestroyed()
    {
        Animator animator = this.GetComponent<Animator>();
        animator.SetBool("destroyed", true);
    }
}
