using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip _checkpointSound;
    private Transform _currentCheckpoint;
    private Health _playerHealth;
	private UIManager _uIManager; 

	private void Awake()
	{	
		_uIManager = FindFirstObjectByType<UIManager>();
		_playerHealth = GetComponent<Health>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.transform.tag == "Checkpoint")
		{
			_currentCheckpoint = collision.transform;
			SoundManager.Instance.PlaySound(_checkpointSound);
			collision.GetComponent<Collider2D>().enabled = false;
			collision.GetComponent<Animator>().SetTrigger("Appear");
		}
	}
	public void CheckRespawn()
	{
		if(_currentCheckpoint == null)
		{
			//Show game menu
			_uIManager.GameOver();
			return;
		}

		transform.position = _currentCheckpoint.position;
		_playerHealth.Respawn();

		Camera.main.GetComponent<CameraController>().MoveToNewRoom(_currentCheckpoint.parent);
	}

}
