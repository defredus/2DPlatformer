using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    [SerializeField] private float _speed = 5;


	public void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}
	public void Update()
	{
		_body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * _speed, _body.linearVelocity.y);
		if (Input.GetKey(KeyCode.Space))
			_body.linearVelocity = new Vector2(_body.linearVelocity.x, _speed);
	}
}
