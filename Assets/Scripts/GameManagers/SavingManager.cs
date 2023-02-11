using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavingManager : MonoBehaviour
{
    public string playerName = "Player 1";

	[Header("Flappy Bird")]
    public List<int> flappyBirdMapHighscores;
	public List<CustomFlappyBirdMapReference> customFlappyBirdMaps;
	public int selectedMapIndex;
	public int selectedCustomMapIndex = -1;

	private string saveFile;

	public static SavingManager i { get; private set; }

	private void Awake()
	{
		i = this;

		saveFile = Application.persistentDataPath + "/SaveData.json";
		LoadData();
	}

	public void LoadData()
	{
		if (File.Exists(saveFile))
		{
			string fileContents = File.ReadAllText(saveFile);
			JsonUtility.FromJsonOverwrite(fileContents, this);
			Debug.Log("Loaded save file");
			Debug.Log(fileContents);
		}
		else
		{
			Debug.Log("Didn't find save file");
		}
	}

	public void SaveData()
	{
		string json = JsonUtility.ToJson(this, true);
		Debug.Log(json);
		File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
	}

	public void DeleteAllSaveData()
	{
		flappyBirdMapHighscores.Clear();
		customFlappyBirdMaps.Clear();
		selectedMapIndex = 0;
		selectedCustomMapIndex = -1;
		SaveData();

		Debug.Log("Deleted all data");

		string mapName = SceneManager.GetActiveScene().name;
		SceneManager.UnloadScene(mapName);
		SceneManager.LoadScene(mapName);
	}
}

[System.Serializable]
public class CustomFlappyBirdMapReference
{
	public FlappyBirdMapSettingsData mapSettings;
	public int highScore;
}