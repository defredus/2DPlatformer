using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
	[Header ("Health")]
    [SerializeField] private float _startingHealth;
	private Animator _animator;
	private bool _dead;
    public float _currentHealth { get; private set; }

	[Header("iFrames")]
	[SerializeField] private float _iFramesDuration;
	[SerializeField] private int _numberOfFlashes;
	private SpriteRenderer _spriteRenderer;


	private void Awake()
	{
		_currentHealth = _startingHealth;
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	public void AddHealth(float value)
	{
		_currentHealth = Mathf.Clamp(_currentHealth + value, 0, _startingHealth);
	}
	public void TakeDamage(float damage)
	{
		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);
		if( _currentHealth > 0)
		{
			_animator.SetTrigger("hurt");
			StartCoroutine(Invunerability());
		}
		else
		{
			if (!_dead)
			{
				_animator.SetTrigger("die");
				GetComponent<PlayerMovement>().enabled = false;
				_dead = true;
			}
		}
	}
	private IEnumerator Invunerability()
	{
		Physics2D.IgnoreLayerCollision(8, 9, true); //8 and 9 - layers
		for (int i = 0; i < _numberOfFlashes; i++)
		{
			_spriteRenderer.color = new Color(1, 0, 0, 0.5f);
			yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
			_spriteRenderer.color = Color.white;
			yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
		}
		Physics2D.IgnoreLayerCollision(8, 9, false);
	}
}
