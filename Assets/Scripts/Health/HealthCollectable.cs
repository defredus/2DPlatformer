using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
	[SerializeField] private float _healthValue;
	[Header("SFX")]
	[SerializeField] private AudioClip _healthPickUpSound;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			SoundManager.Instance.PlaySound(_healthPickUpSound);
			collision.GetComponent<Health>().AddHealth(_healthValue);
			gameObject.SetActive(false);
		}
	}
}
