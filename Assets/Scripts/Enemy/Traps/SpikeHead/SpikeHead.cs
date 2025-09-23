using UnityEngine;

public class SpikeHead : EnemyDamage
{
	[Header("SpikeHead Attributes")]
	[SerializeField] private float _speed;
	[SerializeField] private float _range;
	[SerializeField] private float _checkDelay;
	[SerializeField] private LayerMask _playerLayer;
	private float _checkTimer;
	private bool _attacking;
	private Vector3 _destination;
	private Vector3[] _directions = new Vector3[4];

	private void Update()
	{
		if (_attacking)
		{
			transform.Translate(_destination * Time.deltaTime * _speed);
		}
		else
		{
			_checkTimer += Time.deltaTime;
			if(_checkTimer > _checkDelay)
			{
				CheckForPlayer();
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
		Stop();//Stop spike once he  hits smth
		
	}
	private void OnEnable()
	{
		Stop();
	}
	private void Stop()
	{
		_destination = transform.position;
		_attacking = false;
	}
	private void CheckForPlayer()
	{
		for (int i = 0; i < _directions.Length; i++)
		{
			CalculateDiractions();
			Debug.DrawRay(transform.position, _directions[i], Color.red);
			RaycastHit2D hit = Physics2D.Raycast
				(
				transform.position, _directions[i], 
				_range, _playerLayer	
				);
			if(hit.collider != null && !_attacking)
			{
				_attacking = true;
				_destination = _directions[i];
				_checkTimer = 0;
			}
		}
	}
	private void CalculateDiractions()
	{
		_directions[0] = transform.right * _range; // Right direction
		_directions[1] = -transform.right * _range; // Left direction
		_directions[2] = transform.up * _range; // Up direction
		_directions[3] = -transform.up * _range; // Down direction
	}
}
