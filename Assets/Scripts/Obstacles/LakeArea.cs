using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeArea : MonoBehaviour
{
    public GameObject instruction;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TextMeshProUGUI text = instruction.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.SetText("Beware of the green squares down below");
            instruction.SetActive(true);
        }


    }
}
