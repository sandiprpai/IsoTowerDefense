using UnityEngine;
using System.Collections;

public class MoveOnPathScript : MonoBehaviour {

	public EditorPathScript PathToFollow;
	public int CurrentWayPointID = 0;
	public float speed = 5f;
	public float rotationSpeed = 5f;
	public string pathName;

	float reachDistance = 1f;
	Vector3 last_pos;
	Vector3 current_pos;
	bool isPathComplete = false;

	void Update () 
	{
		if (isPathComplete)
			return;
		
		Transform currentTransform = PathToFollow.path_objs [CurrentWayPointID];
		float distance = Vector3.Distance (currentTransform.position, transform.position);
		transform.position = Vector3.MoveTowards (transform.position, currentTransform.position, speed * Time.deltaTime);

		var rotation = Quaternion.LookRotation (currentTransform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotationSpeed * Time.deltaTime);

		if (distance <= reachDistance) {
			CurrentWayPointID++;
		}

		if (CurrentWayPointID >= PathToFollow.path_objs.Count) {
			isPathComplete = true;
		}
	}

	public bool IsPathComplete()
	{
		return isPathComplete;
	}
}
