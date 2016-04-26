using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] allGuns;
	public int[] selectedGunIndexes;

	public GameObject GetGunPrefab(int selectedIndex)
	{
		int gunIndex = selectedGunIndexes [selectedIndex];
		return allGuns [gunIndex];
	}
}
