using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour
{
    private Animator anim;

    public float speed = 10.0f;
    public float airVelocity = 8f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;
    public float maxFallSpeed = 20.0f;
    public float rotateSpeed = 25f; //Speed the player rotate
    public float threshold = -5f; // How low the character falls before respawning
    private Transform baseObject;
    private Vector3 moveDir;
    public GameObject cam;
    private Rigidbody rb;

    private float distToGround;

    private bool canMove = true; //If player is not hitted
    private bool isStuned = false;
    private bool wasStuned = false; //If player was stunned before get stunned another time
    private float pushForce;
    private Vector3 pushDir;

    public Vector3 checkPoint;
    private bool slide = false;
    private PowerUpsCollector powerUpsCollector;

    void Start ()
    {
        // get the distance to ground
        distToGround = GetComponent<Collider> ().bounds.extents.y;
        powerUpsCollector = GetComponent<PowerUpsCollector> ();
    }

    bool IsGrounded ()
    {
        return Physics.Raycast (transform.position, -Vector3.up, distToGround);
    }

    void Awake ()
    {
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody> ();
        baseObject = this.transform.Find ("Base");
        rb.freezeRotation = true;

        checkPoint = transform.position;
        Cursor.visible = false;
    }

    void FixedUpdate ()
    {
        // Jump
        if (IsGrounded () && Input.GetButtonDown ("Jump"))
        {
            //anim.SetBool ("jump", true);
            rb.AddForce (new Vector3 (0, jumpHeight, 0), ForceMode.Impulse);
        }
        else
        {
            anim.SetBool ("jump", false);
        }

        if (!canMove)
        {
            rb.velocity = pushDir * pushForce;
        }
    }

    private void OnAnimatorMove ()
    {
        Vector3 newRootPosition;

        //if (IsGrounded ())
        //{
        //use root motion as is if on the ground		
        newRootPosition = anim.rootPosition;

        this.transform.position = Vector3.LerpUnclamped (this.transform.position, newRootPosition, speed);
        //this.transform.rotation = Quaternion.LerpUnclamped (this.transform.rotation, newRootRotation, rootTurnSpeed);

        //}
    }


    private void Update ()
    {
        if (transform.position.y < threshold)
        {
            ResetPosition ();
            return;
        }

        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");

        Vector3 v2 = v * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
        Vector3 h2 = h * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
        moveDir = (v2 + h2).normalized; //Global position to which I want to move in magnitude 1

        rb.MoveRotation (rb.rotation * Quaternion.AngleAxis (h * Time.deltaTime * rotateSpeed, Vector3.up));

        anim.SetFloat ("speedY", v);


        RaycastHit hit;
        if (Physics.Raycast (transform.position, -Vector3.up, out hit, distToGround + 0.1f))
        {
            if (hit.transform.tag == "Slide")
            {
                slide = true;
            }
            else
            {
                slide = false;
            }
        }
    }

    public void HitPlayer (Vector3 velocityF, float time)
    {
        rb.velocity = velocityF;

        pushForce = velocityF.magnitude;
        pushDir = Vector3.Normalize (velocityF);
        StartCoroutine (Decrease (velocityF.magnitude, time));
    }

    public void LoadCheckPoint ()
    {
        transform.position = checkPoint;
    }

    private IEnumerator Decrease (float value, float duration)
    {
        if (isStuned)
            wasStuned = true;
        isStuned = true;
        canMove = false;

        float delta = 0;
        delta = value / duration;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return null;
            if (!slide) //Reduce the force if the ground isnt slide
            {
                pushForce = pushForce - Time.deltaTime * delta;
                pushForce = pushForce < 0 ? 0 : pushForce;
                //Debug.Log(pushForce);
            }
            rb.AddForce (new Vector3 (0, -gravity * GetComponent<Rigidbody> ().mass, 0)); //Add gravity
        }

        if (wasStuned)
        {
            wasStuned = false;
        }
        else
        {
            isStuned = false;
            canMove = true;
        }
    }

    void ResetPosition ()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        transform.position = checkPoint;
        transform.rotation = Quaternion.LookRotation (new Vector3 ());
    }

    private void OnTriggerStay (Collider other)
    {

        if (other.gameObject.tag == "Moving Bench")
        {

            //transform.position= other.transform.position;

            transform.position = new Vector3 (other.transform.position.x, transform.position.y, transform.position.z);

        }
    }


    private void OnTriggerEnter (Collider other)
    {

        if (other.gameObject.tag == "Moving Bench")
        {

            //transform.position= other.transform.position;

            transform.position = new Vector3 (other.transform.position.x, transform.position.y, transform.position.z);
        }
    }




    private float GetSpeed ()
    {
        if (powerUpsCollector == null)
            return speed;

        return powerUpsCollector.GetSpeed ();
    }


}
