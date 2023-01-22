using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRespawner : MonoBehaviour
{
	public Vector2 relativeSpawnLocation = Vector2.zero;

	[Header("References")]
	[SerializeField] GameObject map;
	[SerializeField] Transform mapsParent;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		GameObject newMap = Instantiate(map, mapsParent);
		newMap.transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y) + relativeSpawnLocation;

		FlappyBirdMapManager newMapManager = newMap.GetComponent<FlappyBirdMapManager>();
		newMapManager.pipeGapX -= newMapManager.decreasePipeGapX;
		newMapManager.pipeGapY -= newMapManager.decreasePipeGapY;
		newMapManager.Init();
	}
}
