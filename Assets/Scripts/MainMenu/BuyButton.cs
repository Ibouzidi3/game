using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public ResourceType resourceType;
    public AppearanceSelector appearanceSelector;
    public Sprite enabledBg;
    public Sprite disabledBg;
    public Image image;
    public Button button;

    private bool _enabled;
    public bool enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;

            image.sprite = value ? enabledBg : disabledBg;
            button.enabled = value;
        }
    }


    public void SetAmount (int amount)
    {
        this.gameObject.SetActive(amount > 0);
        this.amount.text = amount.ToString();
    }

    public void OnClick()
    {
        appearanceSelector.OnBuyButton(resourceType);
    }
}
