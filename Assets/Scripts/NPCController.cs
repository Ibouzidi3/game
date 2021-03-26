using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCMoveState
{
    NavMesh,
    Manual
}

[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    public Vector3 spawnPoint;
    // Start is called before the first frame update

    public GameObject goal;
    public GameObject[] waypoints;
    public int currentWaypoint = 0;

    private Dictionary<int, float> distancesToGoal;

    private SortedDictionary<float, int> sortedDistancesToGoal;

    private Dictionary<int, bool> isReachable;

    private NavMeshAgent agent;

    private NPCMoveState moveState = NPCMoveState.NavMesh;

    private GameObject manualDestination;

    public int speed = 6;

    int FindClosestToGoal() {
        float[] keys = new float[this.sortedDistancesToGoal.Count];

        var isReachable = false;

        this.sortedDistancesToGoal.Keys.CopyTo(keys, 0);

        foreach(KeyValuePair<float, int> pair in this.sortedDistancesToGoal) {
            
             NavMeshPath navMeshPath = new NavMeshPath();
            isReachable =  agent.CalculatePath(this.waypoints[pair.Value].transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;

            Debug.Log("i="+ this.waypoints[pair.Value].name+" is reachable? " + isReachable + " - "+ pair.Key);
            /*
            foreach (KeyValuePair<float, int> kvp in this.sortedDistancesToGoal)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Debug.Log(string.Format("---------------------------- Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }*/
            if(isReachable) {
                return pair.Value;
            }
        }

       return -1;
    }


    int getNextClosestWaypoint()
    {
        Vector3 currPosition = this.transform.position;

        float minDistance = float.MaxValue;
        int selectedWaypoint = currentWaypoint;

        List<GameObject> results = new List<GameObject>();
        if (waypoints.Length > currentWaypoint + 1)
        {
            float d = Vector3.Distance(currPosition, waypoints[currentWaypoint + 1].transform.position);
            if (d < minDistance)
            {
                minDistance = d;
                selectedWaypoint = currentWaypoint + 1;
            }
        }
        else if (waypoints.Length > currentWaypoint + 2)
        {
            float d = Vector3.Distance(currPosition, waypoints[currentWaypoint + 2].transform.position);
            if (d < minDistance)
            {
                minDistance = d;
                selectedWaypoint = currentWaypoint + 2;
            }
        }
        return selectedWaypoint;
    }

    void CalculateDistancesToGoal() {
        this.sortedDistancesToGoal = new SortedDictionary<float, int>();
        this.distancesToGoal = new Dictionary<int, float>();
        for(int i = 0; i < this.waypoints.Length; i++) {
            var distance =  Vector3.Distance(this.waypoints[i].transform.position, this.goal.transform.position);
            this.distancesToGoal.Add(i, distance);
            this.sortedDistancesToGoal.Add(distance, i);
        }
    }

    void Start()
    {
        this.transform.position = this.spawnPoint;
        agent = GetComponent<NavMeshAgent>();

        CalculateDistancesToGoal();

        this.currentWaypoint = FindClosestToGoal();
        agent.SetDestination(waypoints[this.currentWaypoint].transform.position);
    }

    void UpdateNavMeshNav()
    {

        if (this.currentWaypoint >= waypoints.Length)
        {
            Debug.Log("Reached the goal!");
            return;
        }


         this.currentWaypoint = FindClosestToGoal();
        agent.SetDestination(waypoints[this.currentWaypoint].transform.position);

    }

    void UpdateManualNav()
    {
        Vector3 manualDestPosition = this.manualDestination.transform.position;
        // Calculate direction vector.
        Vector3 dirction = this.transform.position - manualDestPosition;

        // Normalize resultant vector to unit Vector.
        dirction = -dirction.normalized;

        // Move in the direction of the direction vector every frame.
        this.transform.position += dirction * Time.deltaTime * speed;

        if (Vector3.Distance(this.transform.position, manualDestPosition) < 2)
        {
            this.moveState = NPCMoveState.NavMesh;
            this.agent.enabled = true;

            this.currentWaypoint = System.Array.IndexOf(waypoints, this.manualDestination) + 1;
            Debug.Log("Move to " + currentWaypoint);

            agent.SetDestination(waypoints[this.currentWaypoint].transform.position);
            Debug.Log("Move to navmesh");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveState == NPCMoveState.NavMesh)
        {
            this.UpdateNavMeshNav();
        }
        //if moveState = NPCMoveState.Manual
        else
        {
            this.UpdateManualNav();
        }
    }

    public void MoveManuallyTo(GameObject positionTo)
    {
        Debug.Log("Move manually to " + positionTo);

        moveState = NPCMoveState.Manual;
        this.manualDestination = positionTo;
        this.agent.enabled = false;
    }
}
