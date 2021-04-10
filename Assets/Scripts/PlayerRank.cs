using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRank : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text textMeshPro;
    private GameObject player;
    private GameObject[] npcs;
    private Transform playerPosition;
    private Transform[] npcsPositions;
    private int prevRank = 1;
    private string[] rankString = { "0th" , "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th" };

    void Start()
    {
        player = GameObject.Find("Player");
        npcs = GameObject.FindGameObjectsWithTag("NPC");

        playerPosition = player.transform;

        npcsPositions = new Transform[npcs.Length];

        for (int i = 0; i < npcs.Length; i++)
        {
            npcsPositions[i] = npcs[i].transform;
        }

        ShowText(prevRank);
    }

    // Update is called once per frame
    void Update()
    {
        int currentRank = GetPlayerRank();
        if (prevRank != currentRank)
            ShowText(currentRank);

        prevRank = currentRank;


    }


    private void ShowText(int rank)
    {
        textMeshPro.text = rankString[rank];

    }

    private int GetPlayerRank()
    {
        int rank = 1;

        foreach (Transform npcPosition in npcsPositions)
        {
            if (playerPosition.position.x > npcPosition.position.x)
                rank++;
        }

        return rank;
    }
}
