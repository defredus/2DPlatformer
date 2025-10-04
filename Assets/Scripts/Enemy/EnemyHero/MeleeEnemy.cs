using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
	[Header("Attack parametrs")]
	[SerializeField] private float _attackCooldown;
	[SerializeField] private float _range;
	[SerializeField] private float _damage;

	[Header("Collider parametrs")]
	[SerializeField] private float _colliderDistance;
	[SerializeField] private BoxCollider2D _boxCollider;

	[Header("Player Layer")]
	[SerializeField] private LayerMask _playerLayer;
	private float _cooldownTimer = Mathf.Infinity;

	[Header("Components")]
	private Animator _animator;
	private EnemyPatrol _enemyPatrol;
	private Health _playerHealth;

	private void Awake()
	{
		_animator = GetComponent<Animator>();	
		_enemyPatrol = GetComponentInParent<EnemyPatrol>();
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
		if (_enemyPatrol != null)
			_enemyPatrol.enabled = !IsPlayerInSign();
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
