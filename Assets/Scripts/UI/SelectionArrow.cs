using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
	[SerializeField] private RectTransform[] _options;
	private RectTransform _rectTransform;
	private int _currentPosition;
	[SerializeField] private AudioClip _changeSound;
	[SerializeField] private AudioClip _interactSound;


	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			ChangePosition(-1);
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			ChangePosition(1);

		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
			Interact();
	}
	private void Interact()
	{
		SoundManager.Instance.PlaySound(_interactSound);
		_options[_currentPosition].GetComponent<Button>().onClick.Invoke();
	}
	private void ChangePosition(int change)
	{
		_currentPosition += change;

		if (change != 0)
			SoundManager.Instance.PlaySound(_changeSound);
		if (_currentPosition < 0)
			_currentPosition = _options.Length - 1;
		else if (_currentPosition > _options.Length - 1)
			_currentPosition = 0;

		_rectTransform.position = new Vector3
			(_rectTransform.position.x,
			_options[_currentPosition].position.y, 0);
	}
}
