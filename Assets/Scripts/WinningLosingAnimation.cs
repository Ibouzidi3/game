using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningLosingAnimation : MonoBehaviour
{
    public int playerVSnpc;
    public GameObject fireworks;
    private Animator anim; 

    void Awake()
    { 

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        playerVSnpc = FinishLine.instance.playerVSnpc;

        if (playerVSnpc == -1)
        {
            fireworks.SetActive(false);
            anim.SetBool("isWinner", false);
            FinishLine.instance = null;
        }
        else
        {
            fireworks.SetActive(true);
            anim.SetBool("isWinner", true);
            FinishLine.instance = null;
        }
    }
}
