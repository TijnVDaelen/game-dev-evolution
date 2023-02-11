using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSettingsCreator : MonoBehaviour
{
	[Header("References")]
	public InputWithLabel inputWithLabel_PipeIndex;
	public InputWithLabel inputWithLabel_CanOpenOrClose;
	public InputWithLabel inputWithLabel_HorizontalDistance;
	public InputWithLabel inputWithLabel_VerticalDistance;
	public InputWithLabel inputWithLabel_Center;
	public InputWithLabel inputWithLabel_StartsClosed;
	public InputWithLabel inputWithLabel_TriggerDelay;
	public InputWithLabel inputWithLabel_AutomaticallOpensAfterClosing;
	public InputWithLabel inputWithLabel_OpeningDuration;
	public InputWithLabel inputWithLabel_StayingOpenDuration;
	public InputWithLabel inputWithLabel_AutomaticallyClosesAfterOpening;
	public InputWithLabel inputWithLabel_ClosingDuration;
	public InputWithLabel inputWithLabel_StayingClosedDuration;
}
