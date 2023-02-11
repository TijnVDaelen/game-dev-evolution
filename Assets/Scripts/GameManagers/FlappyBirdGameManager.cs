using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FlappyBirdGameManager : MonoBehaviour
{
	public bool gameOver;

	private bool isCustom;
	private bool isInitialized;
	private FlappyBirdMapSettingsData mapSettingsData;

	[Header("References")]
	public List<FlappyBirdMapSettings> mapsSettings = new List<FlappyBirdMapSettings>();
	[SerializeField] private Transform movingSpeed;
	[SerializeField] private Transform player;
	[SerializeField] private Transform mapsParent;
	[SerializeField] private GameObject gameOverScreen;
	[SerializeField] private GameObject nextLevelButton;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI highScoreText;
	[SerializeField] private FlappyBirdMapManager initialMapManager;
	[SerializeField] private SavingManager savingManager;
	[SerializeField] private List<GameObject> backgrounds = new List<GameObject>();
	[SerializeField] private GameObject menus;
	[SerializeField] private GameObject gameplay;

	private int score;

	public static FlappyBirdGameManager i { get; private set; }

	private void Awake()
	{
		i = this;

		menus.SetActive(true);
		gameplay.SetActive(false);
	}

	public void StartLevel(int mapIndex, bool isCustom = false)
	{
		this.isCustom = isCustom;

		if (isCustom)
		{
			savingManager.selectedMapIndex = -1;
			savingManager.selectedCustomMapIndex = mapIndex;
			mapSettingsData = savingManager.customFlappyBirdMaps[mapIndex].mapSettings;
			Debug.Log("Starting custom level " + mapIndex);
		}
		else
		{
			savingManager.selectedMapIndex = mapIndex;
			savingManager.selectedCustomMapIndex = -1;
			mapSettingsData = mapsSettings[savingManager.selectedMapIndex].data;
			Debug.Log("Starting default level " + mapIndex);
		}

		menus.SetActive(false);
		gameplay.SetActive(true);

		Time.timeScale = 1;
		gameOver = false;
		gameOverScreen.SetActive(false);
		score = 0;
		scoreText.text = score.ToString();

		movingSpeed.position = Vector3.zero;
		player.position = Vector3.zero;

		if (mapSettingsData.backgroundIndex < backgrounds.Count)
		{
			for (int i = 0; i < backgrounds.Count; i++)
			{
				backgrounds[i].gameObject.SetActive(i == mapSettingsData.backgroundIndex);
			}
		}

		int index = 0;
		foreach (Transform child in mapsParent)
		{
			if (index > 0) Destroy(child.gameObject);
			index++;
		}
		initialMapManager.Init(mapSettingsData);

		isInitialized = true;
	}

	private void Update()
	{
		if (!isInitialized) return;
		movingSpeed.position += Vector3.right * mapSettingsData.movingSpeed * Time.deltaTime;
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		gameOver = true;
		gameOverScreen.SetActive(true);

		int highScore = 0;
		if (savingManager.selectedCustomMapIndex == -1)
		{
			highScore = savingManager.flappyBirdMapHighscores[savingManager.selectedMapIndex];
			if (score > highScore)
			{
				savingManager.flappyBirdMapHighscores[savingManager.selectedMapIndex] = score;
				savingManager.SaveData();
				highScore = score;
			}
		}
		else
		{
			highScore = savingManager.customFlappyBirdMaps[savingManager.selectedCustomMapIndex].highScore;
			if (score > highScore)
			{
				savingManager.customFlappyBirdMaps[savingManager.selectedCustomMapIndex].highScore = score;
				savingManager.SaveData();
				highScore = score;
			}
		}

		highScoreText.text = "Highscore: "  + highScore;

		if (!isCustom && savingManager.selectedMapIndex + 1 < mapsSettings.Count)
		{
			nextLevelButton.SetActive(true);
		}
		else if (isCustom && savingManager.selectedCustomMapIndex + 1 < savingManager.customFlappyBirdMaps.Count)
		{
			nextLevelButton.SetActive(true);
		}
		else
		{
			nextLevelButton.SetActive(false);
		}
	}


	public void AddScore(int amount)
	{
		score += amount;
		scoreText.text = score.ToString();
	}

	public void Finish()
	{
		mapSettingsData.movingSpeed += mapSettingsData.movingSpeedIncreasePerLap;
	}

	public void GoBack()
	{
		string mapName = SceneManager.GetActiveScene().name;
		SceneManager.UnloadScene(mapName);
		SceneManager.LoadScene(mapName);
	}

	public void RestartCurrentLevel()
	{
		if (isCustom)
		{
			StartLevel(savingManager.selectedCustomMapIndex, isCustom);
		}
		else
		{
			StartLevel(savingManager.selectedMapIndex, isCustom);
		}
	}

	public void StartNextLevel()
	{
		if (!isCustom && savingManager.selectedMapIndex + 1 < mapsSettings.Count)
		{
			StartLevel(savingManager.selectedMapIndex + 1, isCustom);
		}
		else if (isCustom && savingManager.selectedCustomMapIndex + 1 < savingManager.customFlappyBirdMaps.Count)
		{
			StartLevel(savingManager.selectedCustomMapIndex + 1, isCustom);
		}
		else
		{
			Debug.LogError("For some reason this button shouldn't even have been shown since there is no next level");
		}
	}
}
