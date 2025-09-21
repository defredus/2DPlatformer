using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
	[SerializeField] private float _healthValue;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			collision.GetComponent<Health>().AddHealth(_healthValue);
			gameObject.SetActive(false);
		}
	}
}
