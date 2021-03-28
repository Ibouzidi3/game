using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour
{
    private Animator anim;

    public float speed = 3f;
    public float airVelocity = 8f;
    public float gravity = 10.0f;
    public float jumpHeight = 2.0f;
    private bool jump = false;
    private bool isJumping = false;
    public float rotateSpeed = 25f; //Speed the player rotate
    public float threshold = -5f; // How low the character falls before respawning
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
        return Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Awake ()
    {
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody> ();
        rb.freezeRotation = true;
        anim.applyRootMotion = true;
        checkPoint = transform.position;
        Cursor.visible = false;
    }

    void ApexReached ()
    {
    }

    void FixedUpdate ()
    {
        if (IsGrounded () && jump)
        {
            Debug.Log ("jump");
            jump = false;
            anim.SetTrigger ("jump");
            rb.AddForce (new Vector3 (anim.velocity.x, anim.velocity.y * jumpHeight, anim.velocity.z) * GetSpeed(), ForceMode.VelocityChange);
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

        if ((anim.GetNextAnimatorStateInfo (0).IsName ("Jump State") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump State")))
        {
            newRootPosition.y = transform.position.y + anim.deltaPosition.y * jumpHeight;
            Debug.Log ("We are in jump state");
        }


        float newX = Mathf.LerpUnclamped (transform.position.x, newRootPosition.x, GetSpeed());
        float newZ = Mathf.LerpUnclamped (transform.position.z, newRootPosition.z, GetSpeed());
        this.transform.position = new Vector3 (newX, newRootPosition.y, newZ);
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

        if (Input.GetButtonDown ("Jump"))
        {
            jump = true;
        }

        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");

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

    private void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.tag == "Moving Bench" && transform.parent != null && this.transform.parent.Equals (collision.gameObject.transform))
        {
            this.transform.SetParent (null);

        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log ("Collision with " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Moving Bench")
        {
            Debug.Log ("entered collision");

            //transform.position= other.transform.position;
            //transform.position = new Vector3 (other.transform.position.x, transform.position.y, other.transform.position.z);
            this.transform.SetParent (collision.gameObject.transform);
        }

    }

    private float GetSpeed ()
    {
        //if (powerUpsCollector == null)
            return speed;
        //return powerUpsCollector.GetSpeed ();
    }


}
