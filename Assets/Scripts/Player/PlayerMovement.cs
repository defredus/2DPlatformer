using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    [SerializeField] private float _speed = 5;
	private Animator _anim;
	private bool _isGrounded;

	public void Awake()
	{
		//Grab references from Unity
		_body = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
	}
	public void Update()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		_body.linearVelocity = new Vector2(horizontalInput * _speed, _body.linearVelocity.y);
		
		//Flip-flop
		if(horizontalInput > 0.01f)
			transform.localScale = Vector3.one;
		if(horizontalInput < -0.01f)
			transform.localScale = new Vector3(-1,1,1);

		if (Input.GetKey(KeyCode.Space))
			_body.linearVelocity = new Vector2(_body.linearVelocity.x, _speed);

		if (Input.GetKey(KeyCode.Space) && _isGrounded)
			Jump();
		//Set animator parameters
		_anim.SetBool("isRunning", horizontalInput != 0);
		_anim.SetBool("isGrounded", _isGrounded);
	}

	private void Jump()
	{
		_body.linearVelocity = new Vector2(_body.linearVelocity.x, _speed);
		_anim.SetTrigger("jump");
		_isGrounded = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			_isGrounded = true;
	}
}
 