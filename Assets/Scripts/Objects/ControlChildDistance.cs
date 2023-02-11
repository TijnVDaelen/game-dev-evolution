using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlChildDistance : MonoBehaviour
{
	[Header("Settings")]
	public ControlChildDistanceSettings settings;

	[Header("References")]
	[SerializeField] private Transform bottomLeftChild;
	[SerializeField] private Transform topRightChild;
	[SerializeField] private List<SpriteRenderer> colorableImages = new List<SpriteRenderer>();

	[Header("Cache")]
	private float t;
	private float timeToReachTarget;
	private bool isInitialized;

	private Vector2 bottomLeftChildOpenedPosition;
	private Vector2 topRightChildOpenedPosition;
	private Vector2 bottomLeftChildClosedPosition;
	private Vector2 topRightChildClosedPosition;
	private Vector2 bottomLeftChildTargetPosition;
	private Vector2 topRightChildTargetPosition;

	public void Init()
	{
		foreach (SpriteRenderer image in colorableImages)
		{
			image.color = settings.color;
		}

		bottomLeftChild.localPosition = settings.center - new Vector2(settings.horizontalDistance, settings.verticalDistance) / 2;
		topRightChild.localPosition = settings.center + new Vector2(settings.horizontalDistance, settings.verticalDistance) / 2;

		bottomLeftChildOpenedPosition = bottomLeftChild.transform.localPosition;
		topRightChildOpenedPosition = topRightChild.transform.localPosition;
		bottomLeftChildClosedPosition = new Vector2(bottomLeftChild.localPosition.x + settings.horizontalDistance / 2, bottomLeftChild.localPosition.y + settings.verticalDistance / 2);
		topRightChildClosedPosition = new Vector2(topRightChild.localPosition.x - settings.horizontalDistance / 2, topRightChild.localPosition.y - settings.verticalDistance / 2);

		bottomLeftChildTargetPosition = bottomLeftChildOpenedPosition;
		topRightChildTargetPosition = topRightChildOpenedPosition;

		if (settings.startsClosed)
		{
			bottomLeftChild.localPosition = bottomLeftChildClosedPosition;
			topRightChild.localPosition = topRightChildClosedPosition;

			bottomLeftChildTargetPosition = bottomLeftChildClosedPosition;
			topRightChildTargetPosition = topRightChildClosedPosition;
		}

		isInitialized = true;
	}

	private void FixedUpdate()
	{
		if (!isInitialized || !settings.canOpenOrClose) return;

		t += Time.deltaTime / timeToReachTarget;
		bottomLeftChild.localPosition = Vector2.Lerp(bottomLeftChild.localPosition, bottomLeftChildTargetPosition, t);
		topRightChild.localPosition = Vector2.Lerp(topRightChild.localPosition, topRightChildTargetPosition, t);
	}

	public void Trigger()
	{
		if (!isInitialized || !settings.canOpenOrClose) return;

		StartCoroutine(StartAfterWaiting());
	}

	private IEnumerator StartAfterWaiting()
	{
		yield return new WaitForSeconds(settings.triggerDelay);

		if (settings.startsClosed) StartCoroutine(Open());
		else StartCoroutine(Close());
	}

	private IEnumerator Open()
	{
		if (new Vector2(bottomLeftChild.localPosition.x, bottomLeftChild.localPosition.y) != bottomLeftChildTargetPosition) yield break;

		t = 0;
		timeToReachTarget = settings.openingDuration;

		bottomLeftChildTargetPosition = bottomLeftChildOpenedPosition;
		topRightChildTargetPosition = topRightChildOpenedPosition;

		if (settings.automaticallyClosesAfterOpening)
		{
			yield return new WaitForSeconds(settings.stayingOpenDuration);
			StartCoroutine(Close());
		}
	}

	private IEnumerator Close()
	{
		if (new Vector2(bottomLeftChild.localPosition.x, bottomLeftChild.localPosition.y) != bottomLeftChildTargetPosition) yield break;

		t = 0;
		timeToReachTarget = settings.closingDuration;

		bottomLeftChildTargetPosition = bottomLeftChildClosedPosition;
		topRightChildTargetPosition= topRightChildClosedPosition;

		if (settings.automaticallyOpensAfterClosing)
		{
			yield return new WaitForSeconds(settings.stayingClosedDuration);
			StartCoroutine(Open());
		}
	}
}

[System.Serializable]
public class ControlChildDistanceSettings
{
	public int index;
	public bool canOpenOrClose;

	[Space]
	public float horizontalDistance;
	public float verticalDistance;
	public Vector2 center;
	public bool startsClosed;

	[Space]
	public float triggerDelay;

	[Space]
	public bool automaticallyOpensAfterClosing;
	public float openingDuration;
	public float stayingOpenDuration;

	[Space]
	public bool automaticallyClosesAfterOpening;
	public float closingDuration;
	public float stayingClosedDuration;

	[Space]
	public Color color = Color.blue;
}