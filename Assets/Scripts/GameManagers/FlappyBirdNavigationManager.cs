using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlappyBirdNavigationManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject button;
	[SerializeField] private SavingManager savingManager;
	[SerializeField] private FlappyBirdGameManager flappyBirdGameManager;
	[SerializeField] private Transform levelsScrollviewContent;
	[SerializeField] private GameObject levelSelection;
	[SerializeField] private GameObject mapBuilder;

	public void Start()
    {
		InitLevelSelectionButtons();
	}

	public void InitLevelSelectionButtons()
	{
		foreach (Transform child in levelsScrollviewContent)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < flappyBirdGameManager.mapsSettings.Count; i++)
		{
			if (i >= savingManager.flappyBirdMapHighscores.Count)
			{
				savingManager.flappyBirdMapHighscores.Add(0);
			}

			GameObject newButton = Instantiate(button, levelsScrollviewContent);
			newButton.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = flappyBirdGameManager.mapsSettings[i].data.mapName;
			newButton.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
			newButton.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Highscore: " + savingManager.flappyBirdMapHighscores[i];

			// x is to prevent a Unity bug with dynamic onClicks, see http://answers.unity.com/comments/1479566/view.html
			int x = new int();
			x = i;
			newButton.GetComponent<Button>().onClick.AddListener(delegate { SelectLevel(x); });
		}

		for (int i = 0; i < savingManager.customFlappyBirdMaps.Count; i++)
		{
			GameObject newButton = Instantiate(button, levelsScrollviewContent);
			newButton.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = savingManager.customFlappyBirdMaps[i].mapSettings.mapName;
			newButton.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
			newButton.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Highscore: " + savingManager.customFlappyBirdMaps[i].highScore;

			// x is to prevent a Unity bug with dynamic onClicks, see http://answers.unity.com/comments/1479566/view.html
			int x = new int();
			x = i;
			newButton.GetComponent<Button>().onClick.AddListener(delegate { SelectCustomLevel(x); });
		}
	}

	void SelectLevel(int id)
	{
		flappyBirdGameManager.StartLevel(id, false);
	}
	void SelectCustomLevel(int id)
	{
		flappyBirdGameManager.StartLevel(id, true);
	}

	public void OpenLevelSelection()
	{
		mapBuilder.SetActive(false);
		levelSelection.SetActive(true);

		InitLevelSelectionButtons();
	}

	public void OpenLevelBuilder()
	{
		levelSelection.SetActive(false);
		mapBuilder.SetActive(true);
	}
}
