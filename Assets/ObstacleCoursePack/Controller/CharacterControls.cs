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
    public float rotateSpeed = 25f; //Speed the player rotate
    public float threshold = -5f; // How low the character falls before respawning
    private Rigidbody rb;

    private float distToGround;

    private bool canMove = true; //If player is not hitted
    private bool isStuned = false;
    private bool wasStuned = false; //If player was stunned before get stunned another time
    private float pushForce;
    private Vector3 pushDir;
    private float speedY;

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
        jump = false;
    }

    void FixedUpdate ()
    {
        if (IsGrounded () && jump)
        {
            jump = false;
            anim.SetTrigger ("jump");
            //rb.AddForce (new Vector3 (anim.velocity.x, anim.velocity.y * jumpHeight, anim.velocity.z) * GetSpeed (), ForceMode.VelocityChange);
        }

        if (!IsGrounded ())
        {
            rb.velocity = new Vector3 (transform.forward.x * GetSpeed () * 0.5f * speedY, rb.velocity.y, transform.forward.z * GetSpeed () * 0.5f * speedY);
        }

        if (!canMove)
        {
            rb.velocity = pushDir * pushForce;
        }
    }

    private void OnAnimatorMove ()
    {
        Vector3 newRootPosition;

        newRootPosition = anim.rootPosition;

        if ((anim.GetNextAnimatorStateInfo (0).IsName ("Jump State") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump State")))
        {
            newRootPosition.y = transform.position.y + anim.deltaPosition.y * jumpHeight;
            float newX = Mathf.LerpUnclamped (transform.position.x, transform.position.x + transform.forward.x * speedY, GetSpeed () * 0.05f);
            float newZ = Mathf.LerpUnclamped (transform.position.z, transform.position.z + transform.forward.z * speedY, GetSpeed () * 0.05f);
            this.transform.position = new Vector3 (newX, newRootPosition.y, newZ);

            Debug.Log ("We are in jump state");
        }
        else
        {
            float newX = Mathf.LerpUnclamped (transform.position.x, newRootPosition.x, GetSpeed ());
            float newZ = Mathf.LerpUnclamped (transform.position.z, newRootPosition.z, GetSpeed ());
            this.transform.position = new Vector3 (newX, newRootPosition.y, newZ);

        }

    }


    private void Update ()
    {
        float h = Input.GetAxis ("Horizontal");
        speedY = Input.GetAxis ("Vertical");

        anim.SetFloat ("speedY", speedY);
        anim.SetBool ("grounded", IsGrounded ());

        if (transform.position.y < threshold)
        {
            ResetPosition ();
            return;
        }

        if (Input.GetButtonDown ("Jump"))
        {
            jump = true;
        }




        rb.MoveRotation (rb.rotation * Quaternion.AngleAxis (h * Time.deltaTime * rotateSpeed, Vector3.up));



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

        if (collision.gameObject.tag == "Moving Bench")
        {
            this.transform.SetParent (collision.gameObject.transform);
        }
    }



    private float GetSpeed ()
    {
        if (powerUpsCollector == null)
            return speed;

        return powerUpsCollector.GetSpeed ();
    }


}
