using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator), typeof (Rigidbody), typeof (CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;

    void Awake ()
    {

        anim = GetComponent<Animator> ();
        rbody = GetComponent<Rigidbody> ();

        //cinput = GetComponent<CharacterInputController> ();
        //if (cinput == null)
        //    Debug.Log ("CharacterInput could not be found");
    }


    void OnAnimatorMove ()
    {
        Vector3 newRootPosition = anim.rootPosition;
        this.transform.position = newRootPosition;
    }

    private void Update ()
    {
        anim.speed = 1f;

        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");


        anim.SetFloat ("speedY", v);
        anim.SetFloat ("speedX", h);

    }
}
