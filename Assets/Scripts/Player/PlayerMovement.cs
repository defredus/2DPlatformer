using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Unity Components")]
    private Rigidbody2D _body;
	private Animator _anim;
	private BoxCollider2D _boxCollider;
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private LayerMask _wallLayer;

	[Header("Movement Parametrs")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
	private float _wallJumpCooldown;
	private float _horizontalInput;

	[Header("Coyote Time")]
	[SerializeField] private float _coyoteTime;
	private float _coyoteCounter;

	[Header("Multiple Jump")]
	[SerializeField] private int _extraJumps;
	private int _jumpCounter;

	[Header("SFX")]
	[SerializeField] private AudioClip _jumpSound;
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

		//Jump

		if (Input.GetKeyDown(KeyCode.Space))
			Jump();

		//Adjustable jump height
		if (Input.GetKeyUp(KeyCode.Space) && _body.linearVelocity.y > 0)
			_body.linearVelocity = new Vector2
				(
				_body.linearVelocity.x, 
				_body.linearVelocity.y / 2
				);

		if (OnWall())
		{
			_body.gravityScale = 0;
			_body.linearVelocity = Vector2.zero;
		} else
		{
			_body.gravityScale = 7;
			_body.linearVelocity = new Vector2
				(
				_horizontalInput * _speed,
				_body.linearVelocity.y
				);

			if (IsGrounded())
			{
				_coyoteCounter = _coyoteTime;
				_jumpCounter = _extraJumps;
			}
			else
			{
				_coyoteCounter -= Time.deltaTime;
			}
		}
	}

	public bool CanAttack()
	{
		return  _horizontalInput == 0 && IsGrounded() && !OnWall();
	}
	private void Jump()
	{
		if (_coyoteCounter < 0 && !OnWall() && _jumpCounter <= 0) return;
		
		SoundManager.Instance.PlaySound(_jumpSound);

		if (OnWall())
			WallJump();
		else
		{
			if (IsGrounded())
				_body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpPower);
			else
			{
				if(_coyoteCounter > 0)
				{
					_body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpPower);
				}
				else
				{
					if(_jumpCounter > 0)
					{
						_body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpPower);
						_jumpCounter--;
					}
				}
			}
			_coyoteCounter = 0;
		}
	}

	private void WallJump()
	{

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
 