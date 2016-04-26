using UnityEngine;
using System.Collections;

public class BuildButtonScript : MonoBehaviour {

	public int buttonIndex;

	public void BuildButtonClicked()
	{
		GameObject gameUI = GameObject.Find ("GameUI");
		gameUI.GetComponent<GameUIController> ().BuildTurret (buttonIndex);
	}
}
