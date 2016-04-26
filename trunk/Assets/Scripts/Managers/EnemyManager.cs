using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public GameObject[] AllPaths;
	public GameObject[] AllEnemies;
	public float SpawnDelay = 2f;

	float timer = 0f;
	List<GameObject> enemies = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (timer >= SpawnDelay) {
			SpawnEnemy ();
		}

		UpdateEnemies ();
	}

	void SpawnEnemy() 
	{
		timer = 0f;

		int randPathIndex = Random.Range (0, AllPaths.Length);
		Transform spawnTransform = AllPaths [randPathIndex].GetComponent<EditorPathScript> ().path_objs [0];

		int randEnemyIndex = Random.Range (0, AllEnemies.Length);
		GameObject selEnemy = AllEnemies [randEnemyIndex];

		GameObject newEnemy = Instantiate (selEnemy, spawnTransform.position, spawnTransform.rotation) as GameObject;
		newEnemy.GetComponent<MoveOnPathScript> ().PathToFollow = AllPaths [randPathIndex].GetComponent<EditorPathScript> ();
		enemies.Add (newEnemy);
	}

	void UpdateEnemies()
	{
		for (int i = enemies.Count - 1; i >= 0; i--) {
			GameObject selEnemy = enemies [i];
			if ( (!selEnemy.activeSelf) || selEnemy.GetComponent<MoveOnPathScript> ().IsPathComplete ()) {
				enemies.Remove (selEnemy);

				ParticleSystem deathEffect = selEnemy.GetComponent<EnemyController> ().GetDeathEffect ();
				deathEffect.transform.parent = null;
				deathEffect.transform.position = selEnemy.transform.position;
				deathEffect.Play ();
				GameObject.Destroy (deathEffect.gameObject, 2f);

				GameObject.Destroy (selEnemy, 0.5f);
			}
		}
	}
}
