using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public enum OffMeshLinkMoveMethod {
	Teleport,
	NormalSpeed,
	Parabola
}

[RequireComponent (typeof (NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour {
    public GameObject goal;
	public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
	IEnumerator Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
        // if (agent.enabled)
        //{
        //    agent.SetDestination(goal.transform.position);
        //}
        //transform.LookAt(goal.transform.position);
		agent.autoTraverseOffMeshLink = false;
		while (true) {
			if (agent.isOnOffMeshLink) {
                //Debug.Log("On off link: " + Time.time);
				if (method == OffMeshLinkMoveMethod.NormalSpeed)
					yield return StartCoroutine (NormalSpeed (agent));
				else if (method == OffMeshLinkMoveMethod.Parabola)
					yield return StartCoroutine (Parabola (agent, 2.0f, 0.5f));
				agent.CompleteOffMeshLink ();
			}
			yield return null;
		}
	}
	IEnumerator NormalSpeed (NavMeshAgent agent) {
		OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
		while (agent.transform.position != endPos) {
			agent.transform.position = Vector3.MoveTowards (agent.transform.position, endPos, agent.speed*Time.deltaTime);
			yield return null;
		}
	}
	IEnumerator Parabola (NavMeshAgent agent, float height, float duration) {
		OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 startPos = agent.transform.position;
		Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
		float normalizedTime = 0.0f;
		while (normalizedTime < 1.0f) {
			float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
			agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
	}
}