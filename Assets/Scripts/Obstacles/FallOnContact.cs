using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnContact : MonoBehaviour
{

	public float fallDelay = 0.5f;

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{ 
			if (collision.gameObject.tag == "Player")
			{
				StartCoroutine(Fall(fallDelay));
			}
		}
	}

	IEnumerator Fall(float time)
	{

		yield return new WaitForSeconds(time);

		foreach (Transform child in this.transform)
		{
			child.gameObject.SetActive(false);
			
		} 

		yield return new WaitForSeconds(15);
		foreach (Transform child in this.transform)
		{
			child.gameObject.SetActive(true);

		}
		//Destroy(gameObject);
	}


}
