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
			Vector3 playerPos = other.transform.position;
			Vector3 playerDirection = other.transform.forward;
			Quaternion playerRotation = other.transform.rotation;
			float spawnDistance = 10;
			Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
			Vector3 position = new Vector3(spawnPos.x , spawnPos.y +3, spawnPos.z);
			GameObject text = Instantiate(checkpointText, position, playerRotation);
			particle = text.GetComponent<ParticleSystem>(); 
			particle.Play();
			transform.gameObject.SetActive(false);
		}
	}
}
