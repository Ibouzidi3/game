﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public ResourceType resourceType;

    public void SetAmount (int amount)
    {
        this.gameObject.SetActive(amount > 0);
        this.amount.text = amount.ToString();
    }
}
