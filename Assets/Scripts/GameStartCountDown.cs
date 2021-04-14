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
    private AudioSource audioSource;

    void Start()
    {
        
        GameObject player = GameObject.Find("Player");
        StartCoroutine(ShowText(player));
        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
            audioSource = camera.GetComponent<AudioSource>();



    }

    public IEnumerator ShowText(GameObject player)
    {
        Time.timeScale = 0;
        if (audioSource != null)
            audioSource.mute = true;
        yield return new WaitForSecondsRealtime(1);
        TextHelper.ShowText(player, threeText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSecondsRealtime(1);
        TextHelper.ShowText(player, twoText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSecondsRealtime(1);
        TextHelper.ShowText(player, oneText);
        EventManager.TriggerEvent<CountDownEvent, Vector3>(player.transform.position);
        yield return new WaitForSecondsRealtime(1);
        TextHelper.ShowText(player, goText);
        EventManager.TriggerEvent<StartCountDownEvent, Vector3>(player.transform.position);
        Time.timeScale = 1;
        if (audioSource != null)
            audioSource.mute = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
