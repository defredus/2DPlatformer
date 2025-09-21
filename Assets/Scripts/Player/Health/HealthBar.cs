using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Image _totalhealthBar;
	[SerializeField] private Image _currenthealthBar;
	[SerializeField] private Health _playerHealth;

	private void Start()
	{
		_totalhealthBar.fillAmount = _playerHealth._currentHealth / 10;
	}
	private void Update()
	{
		_currenthealthBar.fillAmount = _playerHealth._currentHealth / 10;
	}
}
