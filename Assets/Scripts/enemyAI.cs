using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour {

	[Header ("AI Agent")]
	public NavMeshAgent ai;
	public float fov = 94;
	public float angle = 0;

	[Header ("Player Information")]
	public GameObject player;
	public GameObject playerGhost;

	[Header ("Visualization")]
	public bool visualize = false;
	public LineRenderer line;
	public Color visibleColor;
	public Color hiddenColor;

	void Update () {
		if (PlayerInFoV ()) {
			playerGhost.SetActive(false);
			playerGhost.transform.position = player.transform.position;
			ai.destination = player.transform.position;
		} else {
			playerGhost.SetActive(true);
		}
	}

	bool PlayerInFoV () {
		bool visibility = false;
		RaycastHit hit;
		Vector3 rayDirection = player.transform.position - transform.position;
		if (Physics.Raycast (transform.position, rayDirection, out hit, Mathf.Infinity)) {
			if (hit.transform.tag.Equals (player.tag)) {
				angle = Vector3.Angle (transform.forward, rayDirection);
				if (angle <= fov) {
					visibility = true;
				}
			}
		}
		Visualize (visibility);
		return visibility;
	}

	void Visualize (bool visible) {
		if (!visualize) {
			line.positionCount = 0;
			return;
		}
		if (visible) {
			line.material.color = visibleColor;
		} else {
			line.material.color = hiddenColor;
		}
		Vector3[] positions = { transform.position, player.transform.position };
		line.positionCount = positions.Length;
		line.SetPositions (positions);
	}
}