using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	public Sprite[] allBuildImages;
	public GameObject BuildButton;
	public GameObject BuildPanel;

	Camera gameCamera;
	TurretBaseScript selTurretBase;

	void Start()
	{
		GameObject gameManager = GameObject.Find ("GameManager");
		gameCamera = GameObject.Find ("GameCamera").GetComponent<Camera> ();

		int[] selGuns = gameManager.GetComponent<GameManager> ().selectedGunIndexes;
		Debug.Log (selGuns.Length);
		for (int i = 0; i < selGuns.Length; i++) {
			GameObject newBuildButton = Instantiate (BuildButton) as GameObject;
			newBuildButton.GetComponent<BuildButtonScript> ().buttonIndex = i;
			newBuildButton.GetComponent<Button>().image.sprite = allBuildImages [selGuns [i]];

			newBuildButton.transform.SetParent (BuildPanel.transform);
		}

		BuildPanel.gameObject.SetActive (false);
	}

	public void BuildTurret(int buttonIndex)
	{
		//called by a build button from build panel.
		Debug.Log("Building!");
		BuildPanel.SetActive (false);
		selTurretBase.BuildGun (buttonIndex);
	}

	public void ShowBuildPanel(TurretBaseScript passedBase)
	{
		Vector3 screenPos = gameCamera.WorldToScreenPoint (passedBase.transform.position);
		float bpWidth = BuildPanel.GetComponent<RectTransform> ().rect.width / 1.5f;
		float bpHeight = BuildPanel.GetComponent<RectTransform> ().rect.height / 1.5f;
		float scrWidth = Screen.width;
		float scrHeight = Screen.height;

		Debug.Log (scrWidth + " : " + scrHeight + "SP : " + screenPos.x + ":" + screenPos.y);

		if (screenPos.y - bpHeight < 0)
			screenPos.y += (bpHeight - screenPos.y);

		if (screenPos.y + bpHeight > scrHeight)
			screenPos.y -= (screenPos.y + bpHeight - scrHeight);

		if (screenPos.x - bpWidth < 0)
			screenPos.x += (bpWidth - screenPos.x);

		if (screenPos.x + bpWidth > scrWidth)
			screenPos.x -= (screenPos.x + bpWidth - scrWidth);

		BuildPanel.transform.position = screenPos;
		BuildPanel.SetActive (true);
		selTurretBase = passedBase;
	}

}
