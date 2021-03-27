using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

	public Transform finishLine;
	public GameObject congratsText;
	public GameObject losingText;

	private int playerVSnpc = 0;

	private ParticleSystem particle;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && playerVSnpc == 0)
		{
			playerVSnpc = 1;
			EventManager.TriggerEvent<VictoryEvent, Vector3>(finishLine.parent.position);
			Vector3 position = new Vector3(other.transform.position.x, other.transform.position.y + 3, other.transform.position.z);
			GameObject text = Instantiate(congratsText, position, Quaternion.identity);
			particle = text.GetComponent<ParticleSystem>();
			particle.Play();
			transform.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Player" && playerVSnpc == -1)
		{
			EventManager.TriggerEvent<LosingEvent, Vector3>(finishLine.parent.position);
			Vector3 position = new Vector3(other.transform.position.x, other.transform.position.y + 3, other.transform.position.z);
			GameObject text = Instantiate(losingText, position, Quaternion.identity);
			particle = text.GetComponent<ParticleSystem>();
			particle.Play();
			transform.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "NPC" && playerVSnpc == 0)
		{
			playerVSnpc = -1;
		}
	}
}
