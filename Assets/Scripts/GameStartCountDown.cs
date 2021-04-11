using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject threeText;
    public GameObject twoText;
    public GameObject oneText;
    public GameObject goText;

    void Start()
    {
        
        GameObject player = GameObject.Find("Player");
        StartCoroutine(ShowText(player));
        
    }

    public IEnumerator ShowText(GameObject player)
    {
        yield return new WaitForSeconds(1);
        TextHelper.ShowText(player, threeText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSeconds(1);
        TextHelper.ShowText(player, twoText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSeconds(1);
        TextHelper.ShowText(player, oneText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSeconds(1);
        TextHelper.ShowText(player, goText);
        EventManager.TriggerEvent<StartCountDownEvent, Vector3>(player.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
