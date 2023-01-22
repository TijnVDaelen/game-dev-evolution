using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChildDistance : MonoBehaviour
{
	[Header("Settings")]
	public float horizontalDistance = 0f;
    public float verticalDistance = 3f;
    public Vector2 center = Vector2.zero;

	[Header("References")]
	[SerializeField] private Transform bottomLeftChild;
	[SerializeField] private Transform topRightChild;

	public void Init()
	{
		bottomLeftChild.localPosition = center - new Vector2(horizontalDistance, verticalDistance) / 2;
		topRightChild.localPosition = center + new Vector2(horizontalDistance, verticalDistance) / 2;
	}
}
