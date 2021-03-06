using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject item;
    public int numberOfItems = 5;
    public float spacing = 1.8f;
    public bool isHorizontal = true;
   
    void Awake()
    {
        item = Instantiate(item, gameObject.transform.position, Quaternion.identity);
        item.transform.parent = gameObject.transform;
        GameObject clone = item;
        for (int i = 0; i < numberOfItems -1; i++)
        {
            clone = Instantiate(clone, GeneratePostion(clone), Quaternion.identity);
            clone.transform.parent = gameObject.transform;

        }
        
        
    }

    private Vector3 GeneratePostion(GameObject item)
    {
        Vector3 position;

        if (isHorizontal)
            position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + spacing);
        else
            position = new Vector3(item.transform.position.x - spacing, item.transform.position.y, item.transform.position.z); // for vertical alignment items
        return position;
    }

}
