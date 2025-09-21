using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startingHealth;
	private Animator _animator;
	private bool _dead;
    public float _currentHealth { get; private set; }


	private void Awake()
	{
		_currentHealth = _startingHealth;
		_animator = GetComponent<Animator>();
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
			//iframes
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
}
