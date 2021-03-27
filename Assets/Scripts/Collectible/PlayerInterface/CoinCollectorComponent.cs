using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    private int numberOfCoins = 0;
    public TMP_Text textMeshPro;
    public void Start()
    {
        if(GameObject.Find("coin_text") != null)
            textMeshPro = GameObject.Find("coin_text").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Coin"))
        {

            EventManager.TriggerEvent<CoinCollectionEvent, Vector3>(other.transform.position);
            other.gameObject.SetActive(false);
            numberOfCoins++;
            if (textMeshPro != null)
                textMeshPro.text = numberOfCoins.ToString();

        }

    }
}
