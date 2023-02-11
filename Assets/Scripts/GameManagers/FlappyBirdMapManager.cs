using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdMapManager : MonoBehaviour
{
	public FlappyBirdMapSettingsData settings;

	[Header("References")]
	[SerializeField] private GameObject pipePrefab;
	[SerializeField] private Transform pipesParent;
	[SerializeField] private Transform finish;
	[SerializeField] private Transform destroyMapTrigger;

	public void Init(FlappyBirdMapSettingsData settings)
	{
		this.settings = settings;

		if (settings.finishVisualization == FinishVisualization.hideWithGap || settings.finishVisualization == FinishVisualization.hideWithoutGap)
		{
			finish.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}

		if (settings.pipeGapX < settings.minPipeGapX) settings.pipeGapX = settings.minPipeGapX;
		settings.pipesPosXRandomness = Mathf.Clamp(settings.pipesPosXRandomness, 0, settings.pipeGapX - settings.minPipeGapX);

		foreach (Transform child in pipesParent) Destroy(child.gameObject);

		Vector2 prevPipePos = Vector2.zero;
		for (int i = 0; i < settings.pipeAmount; i++)
		{
			GameObject newPipe = Instantiate(pipePrefab, pipesParent);

			float pipePosX = prevPipePos.x + settings.pipeGapX + Random.Range(-settings.pipesPosXRandomness, settings.pipesPosXRandomness);

			float pipePosY = Random.Range(-settings.pipesPosYRandomness, settings.pipesPosYRandomness);
			if (settings.pipesPosY.Count > i) pipePosY += settings.pipesPosY[i];
			else pipePosY += prevPipePos.y;
			pipePosY = Mathf.Clamp(pipePosY, settings.pipeYConstraints.x, settings.pipeYConstraints.y);

			Vector2 pipePos = new Vector2(pipePosX, pipePosY);
			newPipe.transform.localPosition = pipePos;
			prevPipePos = pipePos;

			ControlChildDistance pipeManager = newPipe.GetComponent<ControlChildDistance>();

			// Pipe Settings
			ControlChildDistanceSettings newPipeSettings = settings.defaultPipeSettings;
			for (int ii = 0; ii < settings.overridePipeSettings.Count; ii++)
			{
				if (settings.overridePipeSettings[ii].index == i)
				{
					Debug.Log("Overwriting pipe with i = " + i + ", ii = " + ii);
					newPipeSettings = settings.overridePipeSettings[ii];
				}
			}

			pipeManager.settings = newPipeSettings;

			float newPipeGapY = newPipeSettings.verticalDistance + Random.Range(-settings.pipeGapYRandomness, settings.pipeGapYRandomness);
			if (newPipeGapY < settings.minPipeGapY) newPipeGapY = settings.minPipeGapY;
			pipeManager.settings.verticalDistance = newPipeGapY;

			pipeManager.Init();
		}

		destroyMapTrigger.localPosition = new Vector2((prevPipePos.x + settings.pipeGapX) * 2, 0);
		MapRespawner mapRespawner = GetComponentInChildren<MapRespawner>();

		if (settings.finishVisualization == FinishVisualization.hideWithoutGap)
		{
			finish.localPosition = new Vector3(prevPipePos.x, 0, 20);
			mapRespawner.relativeSpawnLocation = new Vector2(prevPipePos.x, 0);
		}
		else
		{
			finish.localPosition = new Vector3(prevPipePos.x + settings.pipeGapX, 0, 20);
			mapRespawner.relativeSpawnLocation = new Vector2(prevPipePos.x + settings.pipeGapX, 0);
		}
	}
}

[System.Serializable]
public enum FinishVisualization
{
	show,
	hideWithGap,
	hideWithoutGap
}