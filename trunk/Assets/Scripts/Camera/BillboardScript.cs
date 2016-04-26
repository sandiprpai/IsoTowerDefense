using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour {

	Camera mainCam;

	// Use this for initialization
	void Start () 
	{
		mainCam = FindObjectOfType<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt (transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
	}
}
