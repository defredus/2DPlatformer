using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    
	[SerializeField] private float _attackCooldown;
	[SerializeField] private float _cooldownTimer = Mathf.Infinity;
	[SerializeField] private Transform _firePosition;
	[SerializeField] private GameObject[] _fireballs;

	[Header("Sounds Parameters")]
	[SerializeField] private AudioClip _fireballSound;


	public void Awake()
	{
		_animator = GetComponent<Animator>();
		_playerMovement = GetComponent<PlayerMovement>();
	}

	public void Update()
	{
		if (Input.GetMouseButton(0) && _cooldownTimer > _attackCooldown && _playerMovement.CanAttack())
		{
			Attack();
		}
		_cooldownTimer += Time.deltaTime;
	}
	private void Attack()
	{
		SoundManager.Instance.PlaySound(_fireballSound);
		_animator.SetTrigger("attack");
		_cooldownTimer = 0;

		_fireballs[FindFireball()].transform.position = _firePosition.position;
		_fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
	}
	private int FindFireball()
	{
		for(int i = 0; i < _fireballs.Length; i++)
		{
			if (!_fireballs[i].activeInHierarchy)
				return i;
		}
		return 0;
	}

}
