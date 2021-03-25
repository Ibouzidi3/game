using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TextHelper : MonoBehaviour
{

    private static GameObject CreateText(GameObject player, GameObject text)
    {
        GameObject textGameObject = null;
        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 1.8f, player.transform.position.z);
        textGameObject = Instantiate(text, position, Quaternion.identity);
        return textGameObject;
    }

    public static void ShowText(GameObject player, GameObject text)
    {
        GameObject t = CreateText(player, text);
        ParticleSystem particle = t.GetComponent<ParticleSystem>();
        particle.Play();

    }

}
