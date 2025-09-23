using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] private GameObject[] _enemies;
	private Vector3[] _initialPosition;

	private void Awake()
	{
		_initialPosition = new Vector3[_enemies.Length];
		for (int i = 0; i < _enemies.Length; i++)
		{
			if (_enemies[i] != null)
				_initialPosition[i] = _enemies[i].transform.position;
		}
	}
	public void ActivateRoom(bool status)
	{
		for (int i = 0; i < _enemies.Length; i++)
		{
			if (_enemies[i] != null)
			{
				_enemies[i].SetActive(status);
				_enemies[i].transform.position = _initialPosition[i];
			}
		}
	}
}
