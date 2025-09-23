using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private Transform _previousRoom;
	[SerializeField] private Transform _nextRoom;
	[SerializeField] private CameraController _cam;


	private void Awake()
	{
		_cam = Camera.main.GetComponent<CameraController>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if(collision.transform.position.x < transform.position.x)
			{
				_cam.MoveToNewRoom(_nextRoom);
				_nextRoom.GetComponent<Room>().ActivateRoom(true);
				_previousRoom.GetComponent<Room>().ActivateRoom(false);
			}
			else
			{
				_cam.MoveToNewRoom(_previousRoom);
				_nextRoom.GetComponent<Room>().ActivateRoom(false);
				_previousRoom.GetComponent<Room>().ActivateRoom(true);
			}
		}
	}
}
