using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Trigger : MonoBehaviour
{
	[SerializeField] private ControlChildDistance controlChildDistance;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		controlChildDistance.Trigger();
	}
}
