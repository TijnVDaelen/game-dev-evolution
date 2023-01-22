using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		other.gameObject.GetComponent<PlayerController>().Die();
	}
}
