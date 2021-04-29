using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCMoveState
{
    Idle,
    NavMesh,
    Manual,

    InParabola,

}

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    private (float, float) VelocityRanges = (7, 12);
    public Vector3 spawnPoint;
    public bool enableSpawnPoint = true;

    public GameObject goal;
    private GameObject[] waypoints;
    public int currentWaypoint = 0;

    // for shooting projectile
    public GameObject player;
    public GameObject fireProjectile;
    public float projectileLifespan = 3.0f;
    public float range = 10.0f;
    public bool canShoot = true;
    public float delayInSeconds = 3.0f;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;


    private Dictionary<int, float> distancesToGoal;

    private SortedDictionary<float, int> sortedDistancesToGoal;

    private Dictionary<int, bool> isReachable;

    private NavMeshAgent agent;

    private Rigidbody rb;
    private float distToGround;
    public NPCMoveState moveState = NPCMoveState.NavMesh;

    private GameObject manualOrigin;
    public GameObject manualDestination;

    public int speed = 6;

    private float minDistance = 10;

    private float normalizedTime = 0.0f;

    private float parabolaHeight = 1.0f;

    private float parabolaTime = 0.5f;
    private Animator anim;
    private bool previouslyGrounded;

    int FindClosestToGoal ()
    {
        float[] keys = new float[this.sortedDistancesToGoal.Count];

        var isReachable = false;

        var myDistanceToGoal = Vector3.Distance (this.transform.position, this.goal.transform.position);

        this.sortedDistancesToGoal.Keys.CopyTo (keys, 0);

        foreach (KeyValuePair<float, int> pair in this.sortedDistancesToGoal)
        {

            if (myDistanceToGoal > pair.Key)
            {
                NavMeshPath navMeshPath = new NavMeshPath ();
                isReachable = agent.CalculatePath (this.waypoints[pair.Value].transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;

                //Debug.Log("   Is reachable? "+ this.waypoints[pair.Value].name + " = "+isReachable);

                if (isReachable)
                {
                    return pair.Value;
                }
            }


        }

        return -1;
    }


    void CalculateDistancesToGoal ()
    {
        this.sortedDistancesToGoal = new SortedDictionary<float, int> ();
        this.distancesToGoal = new Dictionary<int, float> ();
        for (int i = 0; i < this.waypoints.Length; i++)
        {
            var distance = Vector3.Distance (this.waypoints[i].transform.position, this.goal.transform.position);
            this.distancesToGoal.Add (i, distance);
            this.sortedDistancesToGoal.Add (distance, i);
        }
    }

    private void Awake ()
    {
        anim = transform.Find ("Player NPC").GetComponent<Animator> ();
        anim.applyRootMotion = false;
    }

    IEnumerator UpdateSpeed()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        System.Random random = new System.Random();
        var (minimum, maximum) = this.VelocityRanges;
        agent.speed = (float) random.NextDouble() * (maximum - minimum) + minimum;

        yield return UpdateSpeed();
    }

    void Start ()
    {
        distToGround = GetComponent<Collider> ().bounds.extents.y;
        moveState = NPCMoveState.NavMesh;
        if (enableSpawnPoint)
        {
            this.transform.position = this.spawnPoint;
        }

        waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");

        agent = GetComponent<NavMeshAgent> ();
        rb = GetComponent<Rigidbody> ();

        CalculateDistancesToGoal ();

        this.currentWaypoint = FindClosestToGoal ();
        if (this.currentWaypoint > 0)
        {
            agent.SetDestination (waypoints[this.currentWaypoint].transform.position);
        }
        else
        {
            Debug.Log ("No accessible waypoints");
        }
        StartCoroutine(UpdateSpeed());
    }

    void UpdateNavMeshNav ()
    {

        if (this.currentWaypoint >= waypoints.Length)
        {
            Debug.Log ("Reached the goal!");
            return;
        }

        if (agent.enabled)
        {
            var nextWaypoint = FindClosestToGoal ();
            if (nextWaypoint != this.currentWaypoint)
            {
                if (this.currentWaypoint >= 0 && this.currentWaypoint < waypoints.Length)
                {
                    agent.SetDestination (waypoints[this.currentWaypoint].transform.position);
                }
            }
            transform.LookAt (waypoints[this.currentWaypoint].transform);

        }


    }

    void UpdateManualNav ()
    {
        transform.LookAt (this.goal.transform);
        Vector3 manualDestPosition = this.manualDestination.transform.position;
        manualDestPosition.y = transform.position.y;
        // Calculate direction vector.
        Vector3 dirction = this.transform.position - manualDestPosition;

        if (dirction.magnitude > minDistance)
        {
            // Debug.Log ("WAIT! it's still " + dirction.magnitude + " and min distance is " + minDistance);
            this.transform.position = this.manualOrigin.transform.position + (Vector3.up * 1.2f);
        }
        else
        {
            //rb.isKinematic = true;
            // Debug.Log("NOWGO!!! "+this.transform.position + " - distance: "+dirction.magnitude + " (min dis):"+minDistance);

            // Normalize resultant vector to unit Vector.
            dirction = -dirction.normalized;

            // Move in the direction of the direction vector every frame.
            //this.transform.position += dirction * Time.deltaTime * speed;

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards (transform.position, manualDestPosition, step);

            //transform.LookAt(this.manualDestination.transform);

            //this.transform.position += new Vector3(0,4.5f,0);

            //Debug.Log("Adding "+ dirction * Time.deltaTime * speed);

            //Vector3 jump;
            //float jumpForce = 1.0f;
            //rb.AddForce(dirction * jumpForce, ForceMode.Impulse);
        }

        //Debug.Log("TO REACH :" + Vector3.Distance(this.transform.position, manualDestPosition));

        if (Vector3.Distance (this.transform.position, manualDestPosition) < 1)
        {

            if (this.manualDestination.tag == "MovingWP")
            {
                this.transform.position = this.manualDestination.transform.position + (Vector3.up * 1.2f);
            }
            /*

            this.moveState = NPCMoveState.NavMesh;
            this.agent.enabled = true;
            

            this.currentWaypoint = System.Array.IndexOf(waypoints, this.manualDestination) + 1;
            Debug.Log("Move to " + currentWaypoint);

            agent.SetDestination(waypoints[this.currentWaypoint].transform.position);
            Debug.Log("Move to navmesh");

            */
        }
    }


    public void Update ()
    {
        if (Time.deltaTime > 1e-5f)
            velocity = agent.velocity / Time.deltaTime;


        bool jump = previouslyGrounded && !IsGrounded ();
        previouslyGrounded = IsGrounded ();

        anim.SetBool ("jump", jump);
        anim.SetBool ("grounded", IsGrounded ());
        anim.SetFloat ("speedY", Math.Abs (velocity.x));

        if (Vector3.Distance (transform.position, player.transform.position) < range && canShoot == true)
        {
            GameObject prj = Instantiate (fireProjectile, transform.position, transform.rotation);
            Destroy (prj, projectileLifespan);
            Vector3 shoot = (player.transform.position - transform.position).normalized;
            prj.GetComponent<Rigidbody> ().AddForce (shoot * 500.0f);

            canShoot = false;
            StartCoroutine (ShootDelay ());
        }

    }

    bool IsGrounded ()
    {
        return Physics.Raycast (transform.position, -Vector3.up, distToGround + 1f);
    }


    IEnumerator ShootDelay ()
    {
        yield return new WaitForSeconds (delayInSeconds);
        canShoot = true;
    }



    // Update is called once per frame
    void FixedUpdate ()
    {
        switch (moveState)
        {
            case NPCMoveState.NavMesh:
                this.UpdateNavMeshNav ();
                break;
            case NPCMoveState.Manual:
                this.UpdateManualNav ();
                break;
            case NPCMoveState.InParabola:
                this.UpdateParabola (this.manualOrigin.transform.position, manualDestination.transform.position, parabolaHeight, parabolaTime);
                break;
        }
    }

    public void MoveManuallyTo (GameObject positionFrom, GameObject positionTo, float minDistance = 10)
    {
        Debug.Log ("Move manually from" + positionFrom.name + " to " + positionTo.name);

        moveState = NPCMoveState.Manual;
        this.manualDestination = positionTo;
        this.manualOrigin = positionFrom;
        this.minDistance = minDistance;

        this.agent.enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void ParabolaTo (GameObject positionFrom, GameObject positionTo, float height = 1.0f, float duration = 0.5f)
    {
        moveState = NPCMoveState.InParabola;
        this.manualDestination = positionTo;
        this.manualOrigin = positionFrom;
        this.agent.enabled = false;
        this.normalizedTime = 0.0f;
        this.parabolaHeight = height;
        this.parabolaTime = duration;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void ResumeNameMesh ()
    {
        this.moveState = NPCMoveState.NavMesh;
        this.agent.enabled = true;

        this.currentWaypoint = FindClosestToGoal ();
        Debug.Log ("Move to " + waypoints[this.currentWaypoint].name);

        agent.SetDestination (waypoints[this.currentWaypoint].transform.position);
        Debug.Log ("Move to navmesh");
    }

    void MoveTo (Vector3 destination)
    {

        // Calculate direction vector.
        Vector3 dirction = this.transform.position - destination;

        // Normalize resultant vector to unit Vector.
        dirction = -dirction.normalized + Vector3.up;

        // Move in the direction of the direction vector every frame.
        this.transform.position += dirction * Time.deltaTime * speed;

        if (Vector3.Distance (this.transform.position, destination) < 2)
        {
            this.moveState = NPCMoveState.NavMesh;
            this.agent.enabled = true;

            this.currentWaypoint = System.Array.IndexOf (waypoints, this.manualDestination) + 1;
            Debug.Log ("Move to " + currentWaypoint);

            agent.SetDestination (waypoints[this.currentWaypoint].transform.position);
            Debug.Log ("Move to navmesh");
        }
    }


    void UpdateParabola (Vector3 from, Vector3 to, float height, float duration)
    {
        int baseOffset = 1;
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = from;
        Vector3 endPos = to + Vector3.up * baseOffset;
        if (normalizedTime < 1.0f)
        {
            float yOffset = height * 3.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
        }
        else
        {
            this.moveState = NPCMoveState.Manual;
        }
    }

}
