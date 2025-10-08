using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[Header("Game Over")]
	[SerializeField] private GameObject _gameOverScreen;
	[SerializeField] private AudioClip _gameOverSound;

	[Header("Pause")]
	[SerializeField] private GameObject _pauseScreen;


	private void Awake()
	{
		_gameOverScreen.SetActive(false);
		_pauseScreen.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			PauseGame(true);
		else
			PauseGame(false);
	}
	#region Game Over
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

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; //Exits Play Mode
		#endif
	}
	#endregion

	#region Pause
	private void PauseGame(bool status)
	{
		_pauseScreen.SetActive(status);
		if(status)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	#endregion
}