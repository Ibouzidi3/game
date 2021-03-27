using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

	public Transform checkpoint;
	public GameObject checkpointText;

	private ParticleSystem particle;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<CharacterControls>().checkPoint = checkpoint.parent.position;
			EventManager.TriggerEvent<CheckpointEvent, Vector3>(checkpoint.parent.position);
			Vector3 position = new Vector3(other.transform.position.x-10, other.transform.position.y+3, other.transform.position.z);
			GameObject text = Instantiate(checkpointText, position, Quaternion.identity);
			particle = text.GetComponent<ParticleSystem>(); 
			particle.Play();
			transform.gameObject.SetActive(false);
		}
	}
}
