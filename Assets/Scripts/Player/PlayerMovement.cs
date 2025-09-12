using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
	private Animator _anim;
	private BoxCollider2D _boxCollider;
	
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

	private float _wallJumpCooldown;
	private float _horizontalInput;

	public void Awake()
	{
		//Grab references from Unity
		_body = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();
	}
	public void Update()
	{
		_horizontalInput = Input.GetAxis("Horizontal");
		
		//Flip-flop
		if(_horizontalInput > 0.01f)
			transform.localScale = Vector3.one;
		if(_horizontalInput < -0.01f)
			transform.localScale = new Vector3(-1,1,1);


		//Set animator parameters
		_anim.SetBool("isRunning", _horizontalInput != 0);
		_anim.SetBool("isGrounded", IsGrounded());

		//wall jump logic
		if(_wallJumpCooldown > 0.2f)
		{
			_body.linearVelocity = new Vector2(_horizontalInput * _speed, _body.linearVelocity.y);

			if (OnWall() && !IsGrounded()){
				_body.gravityScale = 0;
				_body.linearVelocity = Vector2.zero;
			} else {
				_body.gravityScale = 7;
			}
			if (Input.GetKey(KeyCode.Space))
				Jump();
		}
		else
		{
			_wallJumpCooldown += Time.deltaTime; 
		}
	}

	public bool CanAttack()
	{
		return  _horizontalInput == 0 && IsGrounded() && !OnWall();
	}
	private void Jump()
	{
		if (IsGrounded())
		{
			_body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpPower);
			_anim.SetTrigger("jump");
		}
		else if (OnWall() && !IsGrounded())
		{
			_wallJumpCooldown = 0;
			_body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
		}
	}

	private bool IsGrounded() {
		RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, 
			_boxCollider.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
		
		return raycastHit.collider != null;
	}
	private bool OnWall() {
		RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, 
			_boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, _wallLayer);
		
		return raycastHit.collider != null;
	}
}
 