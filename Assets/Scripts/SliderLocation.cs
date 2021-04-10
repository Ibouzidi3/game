using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLocation : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private GameObject player;
    private float startX;
    private float endX;
    void Start()
    {
        slider = this.GetComponent<Slider>();
        player = GameObject.Find("Player");
        startX = GameObject.Find("Start Line").transform.position.x;
        endX = GameObject.Find("End Line").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = calculatePosition();
    }

    private float calculatePosition()
    {
       float result =  player.transform.position.x / (startX + endX);
        if (result < 0)
            return 0;
        else if (result > 1)
            return 1;
        else
            return result;
    }
}
