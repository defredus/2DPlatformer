using UnityEngine;

public class ArrowProjectile : EnemyDamage //Will damage the player every time they touch
{
	[SerializeField] private float _speed;
	[SerializeField] private float _resetTime;
	private float _lifeTime;


	private void Update()
	{
		float movementSpeed = _speed * Time.deltaTime;
		transform.Translate(movementSpeed, 0, 0);

		_lifeTime += Time.deltaTime;
		if(_lifeTime > _resetTime)
			gameObject.SetActive(false); 
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D (collision); //Calling base_OnTriggerEnter2D from EnemyDamage
		gameObject.SetActive(false);
	}
	public void ActiveProjectile()
	{
		_lifeTime = 0;
		gameObject.SetActive(true);
	}
}
