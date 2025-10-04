using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
	[SerializeField] private float _damage;
	[Header("Firetrap timer")]
	[SerializeField] private float _activationDelay;
	[SerializeField] private float _activeTime;
	
	private Animator _animator;
	private SpriteRenderer _spriteRender;
	private Health _playerHealth;

	private bool _triggered;
	private bool _activated;

	private void Update()
	{
		if(_playerHealth != null)
			_playerHealth.TakeDamage(_damage);
	}
	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteRender = GetComponent<SpriteRenderer>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			_playerHealth = collision.GetComponent<Health>();
			if (!_triggered)
			{
				StartCoroutine(ActiveteFiretrap());
			}
			if (_activated) { collision.GetComponent<Health>().TakeDamage(_damage); }
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "Player")
			_playerHealth = null;
		
	}
	private IEnumerator ActiveteFiretrap()
	{
		_triggered = true;
		_spriteRender.color = Color.red;

		yield return new WaitForSeconds(_activationDelay);
		_spriteRender.color = Color.white;
		_activated = true;
		_animator.SetBool("activated", true);

		yield return new WaitForSeconds(_activationDelay);
		_activated = false;
		_triggered = false;
		_animator.SetBool("activated", false);
	}
}
