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
    public float jumpHorizMultiplier = 0.05f;
    public GameObject projectile;    // projectile prefab
    public GameObject fireProjectile;
    public Transform spawnTransform; // transform where the prefab will spawn

    // Button 
    public GameObject buttonPressStandingSpot;
    public float buttonCloseEnoughForMatchDistance = 2f;
    public float buttonCloseEnoughForPressDistance = 0.22f;
    public float buttonCloseEnoughForPressAngleDegrees = 5f;
    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;
    public GameObject buttonObject;
    public GameObject fire;


    private GameObject currentProjectile;
    private bool jump = false;
    public bool attack = false;
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
        currentProjectile = projectile;
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
    }

    void ApexReached ()
    {
        jump = false;
    }

    public void LaunchProjectile ()
    {
        Debug.Log ("Launching projectile");
        Instantiate (currentProjectile, spawnTransform.position, spawnTransform.rotation);
    }

    void FixedUpdate ()
    {
        if (jump)
        {
            jump = false;
            if(IsGrounded())
                anim.SetTrigger ("jump");
        }
        if (attack)
        {
            attack = false;
            anim.SetTrigger ("attack");
            //rb.AddForce (new Vector3 (anim.velocity.x, anim.velocity.y * jumpHeight, anim.velocity.z) * GetSpeed (), ForceMode.VelocityChange);
        }

        if (!canMove)
        {
            rb.velocity = pushDir * pushForce;
        }
    }

    private bool IsInAnimationState (string state)
    {
        return anim.GetNextAnimatorStateInfo (0).IsName (state)
            || anim.GetCurrentAnimatorStateInfo (0).IsName (state);
    }

    private void OnAnimatorMove ()
    {
        Vector3 newRootPosition;

        newRootPosition = anim.rootPosition;

        if (IsInAnimationState ("Jump State") || IsInAnimationState ("Falling"))
        {
            float multiplier = jumpHorizMultiplier;
            newRootPosition.y = transform.position.y + anim.deltaPosition.y * jumpHeight;
            float newX = Mathf.LerpUnclamped (transform.position.x, transform.position.x + transform.forward.x * speedY, GetSpeed () * multiplier);
            float newZ = Mathf.LerpUnclamped (transform.position.z, transform.position.z + transform.forward.z * speedY, GetSpeed () * multiplier);
            this.transform.position = new Vector3 (newX, newRootPosition.y, newZ);
        }
        else
        {
            float newX = Mathf.LerpUnclamped (transform.position.x, newRootPosition.x, GetSpeed ());
            float newZ = Mathf.LerpUnclamped (transform.position.z, newRootPosition.z, GetSpeed ());
            this.transform.position = new Vector3 (newX, newRootPosition.y, newZ);

        }

    }



    void OnAnimatorIK (int layerIndex)
    {

        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo (0);
            if (astate.IsName ("ButtonPress"))
            {

                float buttonWeight = anim.GetFloat ("buttonClose");
                // Set the look target position, if one has been assigned
                if (buttonObject != null)
                {
                    anim.SetLookAtWeight (buttonWeight);
                    anim.SetLookAtPosition (buttonObject.transform.position);
                    anim.SetIKPositionWeight (AvatarIKGoal.RightHand, buttonWeight);
                    anim.SetIKPosition (AvatarIKGoal.RightHand,
                    buttonObject.transform.position);
                }
            }
            else
            {
                anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight (0);
            }
        }
    }

    private void Update ()
    {
        bool doButtonPress = false;

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

        // throw projectile
        if (Input.GetButtonDown ("Fire1"))
        {
            attack = true;
        }

        //change projectile
        if (Input.GetButtonDown ("Fire2"))
        {
            if (currentProjectile == fireProjectile)
            {
                currentProjectile = projectile;
            }
            else
            {
                currentProjectile = fireProjectile;
            }
        }

        // push button
        float buttonDistance = float.MaxValue;
        float buttonAngleDegrees = float.MaxValue;

        if (buttonPressStandingSpot != null)
        {
            buttonDistance = Vector3.Distance (transform.position, buttonPressStandingSpot.transform.position);
        }

        if (Input.GetButtonDown ("Fire3"))
        {
            Debug.Log ("Action pressed");
            if (buttonDistance <= buttonCloseEnoughForMatchDistance)
            {
                Debug.Log ("Button press initiated");
                anim.SetTrigger ("doButtonPress");
                doButtonPress = true;
            }


        }


        var animState = anim.GetCurrentAnimatorStateInfo (0);
        if (animState.IsName ("ButtonPress"))
        {
            fire.SetActive (false);
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
            this.transform.SetParent (null, true);
        }
    }

    private void OnCollisionEnter (Collision collision)
    {

        if (collision.gameObject.tag == "Moving Bench")
        {
            this.transform.SetParent (collision.gameObject.transform, true);
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        PowerUpsCollectorComponent powerup = GetComponent<PowerUpsCollectorComponent> ();

        if (collider.gameObject.tag == "TNT" && (!powerup.shield))
        {
            rb.AddExplosionForce (25000.0F, collider.transform.position, 5.0F, 30);
            ShootPlayer ();
        }

    }

    private void ShootPlayer ()
    {
        PowerUpsCollectorComponent powerup = GetComponent<PowerUpsCollectorComponent> ();
        if (!powerup.shield)
        {
            anim.SetTrigger ("die");
            anim.SetBool ("shot", true);
            StartCoroutine (Delay (3));

        }
    }

    IEnumerator Delay (int time)
    {
        yield return new WaitForSeconds (time);
        anim.SetTrigger ("resurrect");
        ResetPosition ();


    }

    private float GetSpeed ()
    {
        if (powerUpsCollector == null)
            return speed;

        return powerUpsCollector.GetSpeed ();
    }


}
