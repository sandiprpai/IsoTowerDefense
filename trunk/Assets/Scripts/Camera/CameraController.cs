using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float panSpeed = 10f;
	public float zoomSteps = 5f;
	public float zoomSpeed = 5f;

	public int minZoom = 5;
	public int maxZoom = 20;

	float targetZoomSize;
	Vector3 targetPostion;


	// Use this for initialization
	void Start () {
		targetZoomSize = GetComponent<Camera> ().orthographicSize;
		targetPostion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		float hori = Input.GetAxis ("Horizontal");
		float verti = Input.GetAxis ("Vertical");

		float adjPanSpeed = panSpeed * targetZoomSize / zoomSteps;
		targetPostion += new Vector3 (verti * adjPanSpeed * Time.deltaTime, 0, verti * adjPanSpeed * Time.deltaTime);
		targetPostion += new Vector3 (hori * adjPanSpeed * Time.deltaTime, 0, -hori * adjPanSpeed * Time.deltaTime);

		float scroll = Input.GetAxisRaw ("Mouse ScrollWheel");

		if (scroll > 0f) {
			float newSize = targetZoomSize + zoomSteps;
			newSize = Mathf.Max (newSize, minZoom);
			newSize = Mathf.Min (newSize, maxZoom);
			targetZoomSize = newSize;
		}

		if (scroll < 0f) {
			float newSize = targetZoomSize - zoomSteps;
			newSize = Mathf.Max (newSize, minZoom);
			newSize = Mathf.Min (newSize, maxZoom);
			targetZoomSize = newSize;
		}


		float camSize = GetComponent<Camera> ().orthographicSize;
		GetComponent<Camera> ().orthographicSize = Mathf.Lerp (camSize, targetZoomSize, zoomSpeed * Time.deltaTime);

		transform.position = Vector3.Lerp (transform.position, targetPostion, panSpeed * Time.deltaTime);

	}
}
