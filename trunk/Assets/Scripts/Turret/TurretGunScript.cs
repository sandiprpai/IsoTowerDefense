using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurretGunScript : MonoBehaviour {

	public GameObject gunComponent;
	public Light muzzleFlash;
	public Slider reloadSlider;
	LineRenderer laser;
	ParticleSystem bulletParticles;

	public float maxAmmo;
	float currentAmmo;

	List<GameObject> enemiesInRange = new List<GameObject> ();
	GameObject trackedEnemy = null;
	Quaternion targetRotation;
	float rotationSpeed = 10f;
	float timer;
	float fireDelay = 0.1f;
	float effectsDelay = 0.02f;
	public float damagePerBullet = 1f;

	bool isReloading;
	float reloadTime = 0.5f;
	float reloadTimer;

	void Start()
	{
		targetRotation = gunComponent.transform.rotation;
		laser = GetComponentInChildren<LineRenderer> ();
		bulletParticles = GetComponentInChildren<ParticleSystem> ();
		currentAmmo = maxAmmo;
		reloadSlider.gameObject.SetActive (false);
	}

	void Update()
	{
		if (isReloading) {
			reloadTimer += Time.deltaTime;
			if (reloadTimer >= reloadTime) {
				Reload ();
			}

			UpdateReloadSlider ();
		}
		
		timer += Time.deltaTime;
		if (timer >= effectsDelay)
			DisableEffects ();


		if (trackedEnemy == null && enemiesInRange.Count > 0) {
			trackedEnemy = enemiesInRange [0];
		}

		if (trackedEnemy != null) {
			Vector3 dirVec = trackedEnemy.transform.position - this.transform.position;
			Quaternion lookRot = Quaternion.LookRotation (dirVec);

			targetRotation = Quaternion.Euler (gunComponent.transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, gunComponent.transform.rotation.eulerAngles.z);

			if (timer >= fireDelay && currentAmmo > 0)
				Shoot ();
		}
		gunComponent.transform.rotation = Quaternion.Lerp (gunComponent.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		CleanUpTracking ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") {
			enemiesInRange.Add (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Enemy" && enemiesInRange.Contains(other.gameObject)) {
			enemiesInRange.Remove (other.gameObject);

			if (other.gameObject == trackedEnemy)
				trackedEnemy = null;
		}
	}

	void Shoot()
	{
		timer = 0f;
		currentAmmo -= 1;
		muzzleFlash.enabled = true;
		laser.enabled = true;
		bulletParticles.Play ();

		Vector3 startPoint = muzzleFlash.transform.position;
		Vector3 shootDir = -gunComponent.transform.up;
		Vector3 endPoint = startPoint + shootDir * 2f;
		laser.SetPosition (0, startPoint);
		laser.SetPosition (1, endPoint);

		EnemyController trackedController = trackedEnemy.GetComponent<EnemyController> ();
		if (trackedController != null)
			trackedController.TakeDamage (damagePerBullet);

		if (currentAmmo <= 0) {
			reloadSlider.gameObject.SetActive(true);
		}
	}

	void DisableEffects()
	{
		muzzleFlash.enabled = false;
		laser.enabled = false;
		bulletParticles.Stop ();
	}

	void CleanUpTracking()
	{
		for(int i = enemiesInRange.Count - 1; i >= 0; i--) {
			GameObject selEnemy = enemiesInRange [i];

			if (selEnemy == null || !selEnemy.activeSelf) {
				enemiesInRange.Remove (selEnemy);

				if (trackedEnemy == selEnemy)
					trackedEnemy = null;
			}
		}
	}

	public void StartReloading()
	{
		reloadTimer = 0f;
		isReloading = true;
	}

	public void StopReloading()
	{
		reloadTimer = 0f;
		isReloading = false;
		UpdateReloadSlider ();
	}

	void Reload()
	{
		StopReloading ();
		currentAmmo = maxAmmo;
		reloadSlider.gameObject.SetActive (false);
	}

	void UpdateReloadSlider()
	{
		reloadSlider.value = reloadTimer / reloadTime * 100f;
	}
}
