using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
	public static FinishLine instance = null;
	private ParticleSystem particle;

	public Transform finishLine;
	public GameObject congratsText;
	public GameObject losingText;
	public int playerVSnpc 
		{ get; private set; }



	void Start()
    {
		playerVSnpc = 0; 
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && playerVSnpc == 0)
		{
			playerVSnpc = 1;
			EventManager.TriggerEvent<VictoryEvent, Vector3>(finishLine.parent.position);
			Vector3 position = new Vector3(other.transform.position.x-10, other.transform.position.y + 3, other.transform.position.z);
			GameObject text = Instantiate(congratsText, position, Quaternion.identity);
			particle = text.GetComponent<ParticleSystem>();
			particle.Play();
			//transform.gameObject.SetActive(false);
			instance = this;
			StartCoroutine(RestartGame());
		}

		if (other.gameObject.tag == "Player" && playerVSnpc == -1)
		{
			EventManager.TriggerEvent<LosingEvent, Vector3>(finishLine.parent.position);
			Vector3 position = new Vector3(other.transform.position.x-10, other.transform.position.y + 3, other.transform.position.z);
			GameObject text = Instantiate(losingText, position, Quaternion.identity);
			particle = text.GetComponent<ParticleSystem>();
			particle.Play();
			//transform.gameObject.SetActive(false);
			instance = this;
			StartCoroutine(RestartGame());
		}

		if (other.gameObject.tag == "NPC" && playerVSnpc == 0)
		{
			playerVSnpc = -1;
		}
	}
 

	IEnumerator RestartGame()
	{
 
		Debug.Log("Started Coroutine at timestamp : " + Time.time); 
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("EndOfRace"); 
		Debug.Log("Finished Coroutine at timestamp : " + Time.time);
	}

}