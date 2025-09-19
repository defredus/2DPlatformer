using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private Transform _previousRoom;
	[SerializeField] private Transform _nextRoom;
	[SerializeField] private CameraController _cam;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if(collision.transform.position.x < transform.position.x)
			{
				_cam.MoveToNewRoom(_nextRoom);
			}
			else
			{
				_cam.MoveToNewRoom(_previousRoom);	 
			}
		}
	}
}
