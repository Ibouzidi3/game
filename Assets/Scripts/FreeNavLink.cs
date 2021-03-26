using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeNavLink : MonoBehaviour
{
    public GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        NPCController npc = other.GetComponent<NPCController>();
        if(other.isTrigger){
            Debug.Log(other + " -= " + other.tag);
        }
        
        if(npc) {
            Debug.Log("Triggered with NPC!!!");
            npc.MoveManuallyTo(destination);
        }
    }
}
