using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdMapBuilder : MonoBehaviour
{
	private int backgroundIndex;

	[Header("References")]
	[SerializeField] private SavingManager savingManager;
	[SerializeField] private FlappyBirdGameManager gameManager;
	[SerializeField] private FlappyBirdNavigationManager navigationManager;

	[Space]
	[SerializeField] private InputWithLabel inputWithLabel_MapName;
	[SerializeField] private InputWithLabel inputWithLabel_MovingSpeed;
	[SerializeField] private InputWithLabel inputWithLabel_MovingSpeedIncreasePerLap;
	[SerializeField] private InputWithLabel inputWithLabel_FinishVisualization;
	[SerializeField] private InputWithLabel inputWithLabel_PipeAmount;
	[SerializeField] private InputWithLabel inputWithLabel_PipeGapX;
	[SerializeField] private InputWithLabel inputWithLabel_DecreasePipeGapXPerLap;
	[SerializeField] private InputWithLabel inputWithLabel_MinPipeGapX;
	[SerializeField] private InputWithLabel inputWithLabel_PipesPosXRandomness;
	[SerializeField] private InputWithLabel inputWithLabel_DecreasePipeGapYPerLap;
	[SerializeField] private InputWithLabel inputWithLabel_MinPipeGapY;
	[SerializeField] private InputWithLabel inputWithLabel_PipesPosYRandomness;
	[SerializeField] private InputWithLabel inputWithLabel_PipesPosY;
	[SerializeField] private InputWithLabel inputWithLabel_PipeYConstraints;

	[Space]
	[SerializeField] private PipeSettingsCreator defaultPipeSettings;
	[SerializeField] private List<PipeSettingsCreator> overridePipeSettings = new List<PipeSettingsCreator>();

	private void Start()
	{
		SetTemplatedValues(gameManager.mapsSettings[0]);
	}

	private void SetTemplatedValues(FlappyBirdMapSettings settings)
	{
		inputWithLabel_MapName.primaryInputField.text = "Copy of " + settings.data.mapName;
		inputWithLabel_MovingSpeed.primaryInputField.text = settings.data.movingSpeed.ToString();
		inputWithLabel_MovingSpeedIncreasePerLap.primaryInputField.text = settings.data.movingSpeedIncreasePerLap.ToString();
		
		inputWithLabel_FinishVisualization.dropdown.ClearOptions();
		List<string> newOptions = new List<string>();
		for (int i = 0; i < Enum.GetNames(typeof(FinishVisualization)).Length; i++)
		{
			FinishVisualization finishVisualization = (FinishVisualization)i;
			string newOption = finishVisualization.ToString();
			newOptions.Add(newOption);
		}
		inputWithLabel_FinishVisualization.dropdown.AddOptions(newOptions);

		inputWithLabel_PipeAmount.primaryInputField.text = settings.data.pipeAmount.ToString();
		inputWithLabel_PipeGapX.primaryInputField.text = settings.data.pipeGapX.ToString();
		inputWithLabel_DecreasePipeGapXPerLap.primaryInputField.text = settings.data.decreasePipeGapXPerLap.ToString();
		inputWithLabel_MinPipeGapX.primaryInputField.text = settings.data.minPipeGapX.ToString();
		inputWithLabel_PipesPosXRandomness.primaryInputField.text = settings.data.pipesPosXRandomness.ToString();
		inputWithLabel_DecreasePipeGapYPerLap.primaryInputField.text = settings.data.decreasePipeGapYPerLap.ToString();
		inputWithLabel_MinPipeGapY.primaryInputField.text = settings.data.minPipeGapY.ToString();
		inputWithLabel_PipesPosYRandomness.primaryInputField.text = settings.data.pipesPosYRandomness.ToString();

		//inputWithLabel_PipesPosY

		inputWithLabel_PipeYConstraints.primaryInputField.text = settings.data.pipeYConstraints.x.ToString();
		inputWithLabel_PipeYConstraints.secondaryInputField.text = settings.data.pipeYConstraints.y.ToString();

		defaultPipeSettings.inputWithLabel_HorizontalDistance.primaryInputField.text = settings.data.defaultPipeSettings.horizontalDistance.ToString();
		defaultPipeSettings.inputWithLabel_VerticalDistance.primaryInputField.text = settings.data.defaultPipeSettings.verticalDistance.ToString();
		defaultPipeSettings.inputWithLabel_Center.primaryInputField.text = settings.data.defaultPipeSettings.center.x.ToString();
		defaultPipeSettings.inputWithLabel_Center.secondaryInputField.text = settings.data.defaultPipeSettings.center.y.ToString();
		defaultPipeSettings.inputWithLabel_TriggerDelay.primaryInputField.text = settings.data.defaultPipeSettings.triggerDelay.ToString();
		defaultPipeSettings.inputWithLabel_OpeningDuration.primaryInputField.text = settings.data.defaultPipeSettings.openingDuration.ToString();
		defaultPipeSettings.inputWithLabel_StayingOpenDuration.primaryInputField.text = settings.data.defaultPipeSettings.stayingOpenDuration.ToString();
		defaultPipeSettings.inputWithLabel_ClosingDuration.primaryInputField.text = settings.data.defaultPipeSettings.closingDuration.ToString();
		defaultPipeSettings.inputWithLabel_StayingClosedDuration.primaryInputField.text = settings.data.defaultPipeSettings.stayingClosedDuration.ToString();
		defaultPipeSettings.inputWithLabel_CanOpenOrClose.SetCheckBox(settings.data.defaultPipeSettings.canOpenOrClose);
		defaultPipeSettings.inputWithLabel_StartsClosed.SetCheckBox(settings.data.defaultPipeSettings.startsClosed);
		defaultPipeSettings.inputWithLabel_AutomaticallOpensAfterClosing.SetCheckBox(settings.data.defaultPipeSettings.automaticallyOpensAfterClosing);
		defaultPipeSettings.inputWithLabel_AutomaticallyClosesAfterOpening.SetCheckBox(settings.data.defaultPipeSettings.automaticallyClosesAfterOpening);
	}

	public void SetBackgroundIndex(int index)
	{
		backgroundIndex = index;
	}

	public void SaveMap()
	{
		FlappyBirdMapSettingsData newSettings = new FlappyBirdMapSettingsData();

		newSettings.mapName = inputWithLabel_MapName.primaryInputField.text;
		newSettings.movingSpeed = float.Parse(inputWithLabel_MovingSpeed.primaryInputField.text);
		newSettings.movingSpeedIncreasePerLap = float.Parse(inputWithLabel_MovingSpeedIncreasePerLap.primaryInputField.text);
		newSettings.finishVisualization = (FinishVisualization)inputWithLabel_FinishVisualization.dropdown.value;
		newSettings.pipeAmount = Int32.Parse(inputWithLabel_PipeAmount.primaryInputField.text);
		newSettings.pipeGapX = float.Parse(inputWithLabel_PipeGapX.primaryInputField.text);
		newSettings.decreasePipeGapXPerLap = float.Parse(inputWithLabel_DecreasePipeGapXPerLap.primaryInputField.text);
		newSettings.minPipeGapX = float.Parse(inputWithLabel_MinPipeGapX.primaryInputField.text);
		newSettings.pipesPosXRandomness = float.Parse(inputWithLabel_PipesPosXRandomness.primaryInputField.text);
		newSettings.decreasePipeGapYPerLap = float.Parse(inputWithLabel_DecreasePipeGapYPerLap.primaryInputField.text);
		newSettings.minPipeGapY = float.Parse(inputWithLabel_MinPipeGapY.primaryInputField.text);
		newSettings.pipesPosYRandomness = float.Parse(inputWithLabel_PipesPosYRandomness.primaryInputField.text);
		
		//inputWithLabel_PipesPosY

		newSettings.pipeYConstraints.x = float.Parse(inputWithLabel_PipeYConstraints.primaryInputField.text);
		newSettings.pipeYConstraints.y = float.Parse(inputWithLabel_PipeYConstraints.secondaryInputField.text);

		newSettings.defaultPipeSettings = new ControlChildDistanceSettings();
		newSettings.defaultPipeSettings.horizontalDistance = float.Parse(defaultPipeSettings.inputWithLabel_HorizontalDistance.primaryInputField.text);
		newSettings.defaultPipeSettings.verticalDistance = float.Parse(defaultPipeSettings.inputWithLabel_VerticalDistance.primaryInputField.text);
		newSettings.defaultPipeSettings.center.x = float.Parse(defaultPipeSettings.inputWithLabel_Center.primaryInputField.text);
		newSettings.defaultPipeSettings.center.y = float.Parse(defaultPipeSettings.inputWithLabel_Center.secondaryInputField.text);
		newSettings.defaultPipeSettings.triggerDelay = float.Parse(defaultPipeSettings.inputWithLabel_TriggerDelay.primaryInputField.text);
		newSettings.defaultPipeSettings.openingDuration = float.Parse(defaultPipeSettings.inputWithLabel_OpeningDuration.primaryInputField.text);
		newSettings.defaultPipeSettings.stayingOpenDuration = float.Parse(defaultPipeSettings.inputWithLabel_StayingOpenDuration.primaryInputField.text);
		newSettings.defaultPipeSettings.closingDuration = float.Parse(defaultPipeSettings.inputWithLabel_ClosingDuration.primaryInputField.text);
		newSettings.defaultPipeSettings.stayingClosedDuration = float.Parse(defaultPipeSettings.inputWithLabel_StayingClosedDuration.primaryInputField.text);
		newSettings.defaultPipeSettings.canOpenOrClose = defaultPipeSettings.inputWithLabel_CanOpenOrClose.checkBoxIsChecked;
		newSettings.defaultPipeSettings.startsClosed = defaultPipeSettings.inputWithLabel_StartsClosed.checkBoxIsChecked;
		newSettings.defaultPipeSettings.automaticallyOpensAfterClosing = defaultPipeSettings.inputWithLabel_AutomaticallOpensAfterClosing.checkBoxIsChecked;
		newSettings.defaultPipeSettings.automaticallyClosesAfterOpening = defaultPipeSettings.inputWithLabel_AutomaticallyClosesAfterOpening.checkBoxIsChecked;

		newSettings.backgroundIndex = backgroundIndex;

		CustomFlappyBirdMapReference customFlappyBirdMapReference = new CustomFlappyBirdMapReference();
		customFlappyBirdMapReference.mapSettings = newSettings;

		savingManager.customFlappyBirdMaps.Add(customFlappyBirdMapReference);
		savingManager.SaveData();

		navigationManager.OpenLevelSelection();
	}
}
