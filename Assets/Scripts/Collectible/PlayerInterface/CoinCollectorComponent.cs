using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    private TMP_Text textMeshPro;
    public void Start ()
    {
        Gamestate.coinsCurrentGame = 0;
        if (GameObject.Find ("coin_text") != null)
            textMeshPro = GameObject.Find ("coin_text").GetComponent<TMP_Text> ();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other != null && other.gameObject.CompareTag ("Coin"))
        {

            EventManager.TriggerEvent<CoinCollectionEvent, Vector3> (other.transform.position);
            other.gameObject.SetActive (false);
            Gamestate.coinsCurrentGame++;
            if (textMeshPro != null)
                textMeshPro.text = Gamestate.coinsCurrentGame.ToString ();

        }

    }
}
