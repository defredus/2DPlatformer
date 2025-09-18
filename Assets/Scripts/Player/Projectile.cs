using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _hit;
	private float _direction;
	private float _lifeTime;

    private BoxCollider2D _boxCollieder;
    private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_boxCollieder = GetComponent<BoxCollider2D>();
	}
	private void Update()
	{
		if (_hit) return;
		float movementSpeed = _speed * Time.deltaTime * _direction;
		transform.Translate(movementSpeed, 0, 0);

		_lifeTime += Time.deltaTime;
		if (_lifeTime > 5) gameObject.SetActive(false);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		_hit = true;
		_boxCollieder.enabled = false;
		_animator.SetTrigger("Explore");
	}
	public void SetDirection(float direction)
	{
		_lifeTime = 0;
		_direction = direction;
		gameObject.SetActive(true);
		_hit = false;
		_boxCollieder.enabled = true;

		float localScaleX = transform.localScale.x;
		if(Mathf.Sign(localScaleX)!= _direction)
			localScaleX = -localScaleX;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

	}
	private void Deactivate()
	{
		gameObject.SetActive(false);
	}
}
