using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeJumpLink : MonoBehaviour
{

    // GameOobjects that already pass through here will no pass again
    private HashSet<string> collidedObjects;
    public GameObject destination;

    public float minDistance = 20;

    public bool isEndOfChain = false;

    public bool useParabola = false;

    public float parabolaHeight = 1.0f;

    public float parabolaTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetDestination() {
        return this.destination;
    }

    private void OnTriggerEnter(Collider other){
        NPCController npc = other.GetComponent<NPCController>();

        //Debug.Log("Collision between " + this.name + " and " +other.name);
        
        if(npc) {

            if(isEndOfChain) {
                npc.ResumeNameMesh();
            }
            else if(useParabola) {
                Debug.Log("parabolla Collision between " + this.name + " and " +other.name);
                npc.ParabolaTo(gameObject, destination, parabolaHeight, parabolaTime);
            }
            else {
                
                Debug.Log("Collision between " + this.name + " and " +other.name);
                //if(!collidedObjects.Contains(other.name) ) {
                    npc.MoveManuallyTo(gameObject, destination, minDistance);
                    //collidedObjects.Add(other.name);
                //}
            }

        }
    }
}
