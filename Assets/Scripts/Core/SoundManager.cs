using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }
	private AudioSource _soundSource;
	private AudioSource _musicSource;

	private void Awake()
	{
		_soundSource = GetComponent<AudioSource>();
		_musicSource = transform.GetChild(0).GetComponent<AudioSource>();
		
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != null && Instance != this){
			Destroy(gameObject);
		}
		ChangeMusicVolume(0);
		ChangeSoundVolume(0);
	}

	public void PlaySound(AudioClip sound)
	{
		_soundSource.PlayOneShot(sound);
	}
	public void ChangeSoundVolume(float volume)
	{
		ChangeSourceVolume(1, "soundVolume", volume, _soundSource);
	}
	public void ChangeMusicVolume(float volume)
	{
		ChangeSourceVolume(0.3f, "musicVolume", volume, _musicSource);
	}

	private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
	{

		float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
		currentVolume += change;

		if(currentVolume > 1)
			currentVolume = 0;
		else if(currentVolume < 0)
			currentVolume = 1;

		float finalVolume = currentVolume * baseVolume;
		source.volume = finalVolume;
		PlayerPrefs.SetFloat(volumeName, currentVolume);
	}
}
