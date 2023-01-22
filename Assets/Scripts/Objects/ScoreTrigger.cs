using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
	[SerializeField] private int score = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		FlappyBirdGameManager.i.AddScore(score);
	}
}
