using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCMoveState
{
    Idle,
    NavMesh,
    Manual

}

[RequireComponent (typeof (NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    public Vector3 spawnPoint;
    public bool enableSpawnPoint = true;

    public GameObject goal;
    public GameObject[] waypoints;
    public int currentWaypoint = 0;

    private Dictionary<int, float> distancesToGoal;

    private SortedDictionary<float, int> sortedDistancesToGoal;

    private Dictionary<int, bool> isReachable;

    private NavMeshAgent agent;

    private Rigidbody rb;

    public NPCMoveState moveState = NPCMoveState.NavMesh;

    private GameObject manualOrigin;
    public GameObject manualDestination;

    public int speed = 6;

    private float minDistance = 10;

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

    void Start ()
    {
        moveState = NPCMoveState.NavMesh;
        if (enableSpawnPoint)
        {
            this.transform.position = this.spawnPoint;
        }

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
            this.currentWaypoint = FindClosestToGoal ();
            if (this.currentWaypoint >= 0 && this.currentWaypoint < waypoints.Length)
            {
                agent.SetDestination (waypoints[this.currentWaypoint].transform.position);
            }

        }


    }

    void UpdateManualNav ()
    {
        Vector3 manualDestPosition = this.manualDestination.transform.position;
        // Calculate direction vector.
        Vector3 dirction = this.transform.position - manualDestPosition;

        if (dirction.magnitude > minDistance)
        {
            Debug.Log ("WAIT! it's still " + dirction.magnitude + " and min distance is " + minDistance);
            this.transform.position = this.manualOrigin.transform.position + (Vector3.up * 1.2f);
        }
        else
        {
            //Debug.Log("NOWGO!!!");

            // Normalize resultant vector to unit Vector.
            dirction = -dirction.normalized;

            // Move in the direction of the direction vector every frame.
            this.transform.position += dirction * Time.deltaTime * speed;

            //Vector3 jump;
            //float jumpForce = 2.0f;
            //rb.AddForce(dirction * jumpForce, ForceMode.Impulse);
        }

        //Debug.Log("TO REACH :" + Vector3.Distance(this.transform.position, manualDestPosition));

        if (Vector3.Distance (this.transform.position, manualDestPosition) < 4)
        {
            if (this.manualDestination.tag == "MovingWP")
            {
                this.transform.position = this.manualDestination.transform.position + (Vector3.up * 1.2f);
            }
            else
            {
                moveState = NPCMoveState.Idle;
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

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (moveState == NPCMoveState.NavMesh)
        {
            this.UpdateNavMeshNav ();
        }
        //if moveState = NPCMoveState.Manual
        else if (moveState == NPCMoveState.Manual)
        {
            //Debug.Log("Moving manuall: "+ moveState);
            this.UpdateManualNav ();
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

    public void ResumeNameMesh ()
    {
        this.moveState = NPCMoveState.NavMesh;
        this.agent.enabled = true;


        this.currentWaypoint = System.Array.IndexOf (waypoints, this.manualDestination) + 1;
        Debug.Log ("Move to " + currentWaypoint);

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

}
