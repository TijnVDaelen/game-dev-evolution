using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flappy Bird Map", menuName = "Flappy Bird/Flappy Bird Map")]
public class FlappyBirdMapSettings : ScriptableObject
{
	public FlappyBirdMapSettingsData data;
}

[System.Serializable]
public class FlappyBirdMapSettingsData
{
	public string mapName;
	public float movingSpeed;
	public float movingSpeedIncreasePerLap;
	public FinishVisualization finishVisualization;
	public int pipeAmount;
	public float pipeGapX;
	public float decreasePipeGapXPerLap;
	public float minPipeGapX;
	public float pipesPosXRandomness;
	public float decreasePipeGapYPerLap;
	public float pipeGapYRandomness;
	public float minPipeGapY;
	public float pipesPosYRandomness;
	public List<float> pipesPosY;
	public Vector2 pipeYConstraints;
	public ControlChildDistanceSettings defaultPipeSettings;
	public List<ControlChildDistanceSettings> overridePipeSettings;
	public int backgroundIndex;
}
