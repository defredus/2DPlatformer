using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
	[SerializeField] private float _attackCooldown;
	[SerializeField] private float _range;
	[SerializeField] private float _damage;
	[SerializeField] private float _colliderDistance;
	[SerializeField] private LayerMask _playerLayer;
	private float _cooldownTimer = Mathf.Infinity;

	private Animator _animator;
	private BoxCollider2D _boxCollider;

	private Health _playerHealth;

	private void Awake()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_animator = GetComponent<Animator>();	
	}
	private void Update()
	{
		_cooldownTimer += Time.deltaTime;
		if (IsPlayerInSign())
		{
			if (_cooldownTimer >= _attackCooldown)
			{
				_cooldownTimer = 0;
				_animator.SetTrigger("MeleeAttack");
			}
		}
	}
	private bool IsPlayerInSign()
	{
		RaycastHit2D hit = Physics2D.BoxCast
			(
			_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, 
			new Vector3(_boxCollider.bounds.size.x * _range, 
						_boxCollider.bounds.size.y, 
						_boxCollider.bounds.size.z),
			0, Vector2.left, 0, _playerLayer
			);

		if(hit.collider != null)	
			_playerHealth = hit.transform.GetComponent<Health>();	


		return hit.collider != null;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube
			(
			_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
			new Vector3(_boxCollider.bounds.size.x * _range,
						_boxCollider.bounds.size.y,
						_boxCollider.bounds.size.z)
			);
	}
	private void DamagePlayer()
	{
		if (IsPlayerInSign())
			_playerHealth.TakeDamage(_damage);
	}
}
