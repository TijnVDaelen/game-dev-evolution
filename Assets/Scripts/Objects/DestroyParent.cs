using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		Destroy(transform.parent.gameObject);
	}
}
