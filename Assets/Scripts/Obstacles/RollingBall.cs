using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREDIT/ https://forum.unity.com/threads/randomly-enable-and-disable-gameobjects-after-a-certain-time.835528/
public class RollingBall : MonoBehaviour
{
    
    public GameObject[] objectPool;
    public Vector3[] positions;
    private int currentIndex = 0;


    float elapsedTime = 0f; // Counts up to repeatTime
    public float repeatTime = 5f; // Time taken to repeat in seconds

    float roundTime = 60f; // Time of one round
    float totalRoundTime = 60f;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[objectPool.Length];
        int i = 0;
        foreach (GameObject obj in objectPool)
        {
            positions[i] = objectPool[i].transform.position;
            obj.SetActive(false); 
            i += 1;
        }

        gameObject.active = false;

    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= repeatTime)
        { 
            NewRandomObject(); 
            elapsedTime -= repeatTime;
        }

        totalRoundTime -= Time.deltaTime;
        if (totalRoundTime <= 0)
        {

            repeatTime -= 0.5f;
            totalRoundTime = 10f;
        }
    }

    public void NewRandomObject()
    {
        int newIndex = Random.Range(0, objectPool.Length);
        // Deactivate old gameobject
        objectPool[currentIndex].SetActive(false);
        // Activate new gameobject
        currentIndex = newIndex;

        objectPool[currentIndex].SetActive(true);
        objectPool[currentIndex].transform.position = positions[currentIndex];
    }
 



}

