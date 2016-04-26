using UnityEngine;
using System.Collections;

public class TurretBaseScript : MonoBehaviour {

	GameObject turret;

	GameManager gameManager;
	GameUIController gameUI;

	void Start()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gameUI = GameObject.Find ("GameUI").GetComponent<GameUIController> ();
	}

	void OnMouseDown()
	{
		Debug.Log ("clicked");
		gameUI.ShowBuildPanel (this);
	}

	public void BuildGun(int buttonIndex)
	{
		if (turret != null) {
			Destroy (turret.gameObject);
		}
		else {
			GameObject turretPrefab = gameManager.GetGunPrefab (buttonIndex);
			turret = Instantiate (turretPrefab, transform.position, transform.rotation) as GameObject;
			turret.transform.parent = transform;
		}
	}


}
