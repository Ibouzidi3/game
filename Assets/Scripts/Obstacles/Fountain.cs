using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public float damping = 2f;
    public float force = 10.0f;
    private Rigidbody rb ;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Vector3 velocity = rb.velocity;
                velocity += transform.up * force * Time.deltaTime;
                velocity -= velocity * damping * Time.deltaTime;
                rb.velocity = velocity;
                StartCoroutine("Float");
            }

        }


    }

    IEnumerator Float()
    {
        Debug.Log("Floating");
        rb.useGravity = false;
        yield return new WaitForSeconds(2);
        rb.useGravity = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
