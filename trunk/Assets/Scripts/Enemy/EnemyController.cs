using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float maxHealth;
	public ParticleSystem deathParticles;
	public Slider healthSlider;

	float currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Destination") {
			gameObject.SetActive (false);
		}
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		UpdateHealthSlider ();

		if (currentHealth <= 0f)
			gameObject.SetActive (false);
	}

	public ParticleSystem GetDeathEffect()
	{
		return deathParticles;
	}

	void UpdateHealthSlider()
	{
		healthSlider.value = currentHealth / maxHealth * healthSlider.maxValue;
	}
}
