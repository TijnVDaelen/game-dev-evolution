using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdGameManager : MonoBehaviour
{
	public bool gameOver;

	[Header("Game Settings")]
	[SerializeField] private float flyingSpeed = 3f;
	[SerializeField] private float increaseFlyingSpeed;

	[Header("References")]
	[SerializeField] private GameObject gameOverScreen;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private FlappyBirdMapManager initialMapManager;

	private int score;

	public static FlappyBirdGameManager i { get; private set; }

	private void Awake()
	{
		i = this;

		Time.timeScale = 1;
		gameOver = false;
		gameOverScreen.SetActive(false);
		score = 0;

		initialMapManager.Init();
	}

	private void Update()
	{
		transform.position += Vector3.right * flyingSpeed * Time.deltaTime;
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		gameOver = true;
		gameOverScreen.SetActive(true);
	}

	public void RestartCurrentLevel()
	{
		SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void AddScore(int amount)
	{
		score += amount;
		scoreText.text = score.ToString();
	}

	public void Finish()
	{
		flyingSpeed += increaseFlyingSpeed;
	}
}
