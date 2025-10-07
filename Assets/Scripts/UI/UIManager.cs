using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject _gameOverScreen;
	[SerializeField] private AudioClip _gameOverSound;

	private void Awake()
	{
		_gameOverScreen.SetActive(false);
	}
	public void GameOver()
	{
		_gameOverScreen.SetActive(true);
		SoundManager.Instance.PlaySound(_gameOverSound);
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
	public void Quit()
	{
		Application.Quit(); //Quits the game(only in build version)
		UnityEditor.EditorApplication.isPlaying = false; //Exits Play Mode

	}

}
