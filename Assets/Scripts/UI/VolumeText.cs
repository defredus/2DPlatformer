using UnityEngine;
using TMPro;

public class VolumeText : MonoBehaviour
{
	[SerializeField] private string _volumeName;
	[SerializeField] private string _textIntro;
	private TextMeshProUGUI _txt;

	private void Awake()
	{
		_txt = GetComponent<TextMeshProUGUI>();

		if (_txt == null)
		{
			Debug.LogError("TextMeshProUGUI component not found on " + gameObject.name);
			enabled = false;
		}
	}

	private void Update()
	{
		UpdateText();
	}

	private void UpdateText()
	{
		if (_txt != null)
		{
			float volumeValue = PlayerPrefs.GetFloat(_volumeName, 0.5f) * 100;
			_txt.text = volumeValue.ToString("F0");
		}
	}
}