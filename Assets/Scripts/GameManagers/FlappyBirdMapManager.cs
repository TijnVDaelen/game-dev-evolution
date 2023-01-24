using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdMapManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private FinishVisualization finishVisualization;
    [SerializeField] private int pipeAmount = 5;

	[Space]
	public float pipeGapX = 4f;
	public float decreasePipeGapX;
	[SerializeField] private float minPipeGapX = 1.5f;
    [Tooltip("Limited to pipeGapX - minPipeGapX")]
    [SerializeField] private float pipesPosXRandomness;

	[Space]
	public float pipeGapY = 3f;
	public float decreasePipeGapY;
	[SerializeField] private float pipeGapYRandomness;
    [SerializeField] private float minPipeGapY = 1.5f;
	[Tooltip("Values larger than the pipeYConstraints will increasingly result in more pipes stuck to the top/bottom")]
    [SerializeField] private float pipesPosYRandomness = 1f;
    [SerializeField] private List<int> pipesPosY = new List<int>();
	[SerializeField] private Vector2 pipeYConstraints = new Vector2(-3, 3);

	[Header("References")]
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private Transform pipesParent;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform destroyMapTrigger;

	private void Awake()
	{
		if (finishVisualization == FinishVisualization.hideWithGap || finishVisualization == FinishVisualization.hideWithoutGap)
		{
			finish.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void Init()
    {
        if (pipeGapX < minPipeGapX) pipeGapX = minPipeGapX;
        if (pipeGapY < minPipeGapY) pipeGapY = minPipeGapY;
		pipesPosXRandomness = Mathf.Clamp(pipesPosXRandomness, 0, pipeGapX - minPipeGapX);

		foreach (Transform child in pipesParent) Destroy(child.gameObject);

        Vector2 prevPipePos = Vector2.zero;
		for (int i = 0; i < pipeAmount; i++)
        {
            GameObject newPipe = Instantiate(pipePrefab, pipesParent);

            float pipePosX = prevPipePos.x + pipeGapX + Random.Range(-pipesPosXRandomness, pipesPosXRandomness);

            float pipePosY = Random.Range(-pipesPosYRandomness, pipesPosYRandomness);
            if (pipesPosY.Count > i) pipePosY += pipesPosY[i];
            else pipePosY += prevPipePos.y;
			pipePosY = Mathf.Clamp(pipePosY, pipeYConstraints.x, pipeYConstraints.y);

            Vector2 pipePos= new Vector2(pipePosX, pipePosY);
			newPipe.transform.localPosition = pipePos;
			prevPipePos = pipePos;

			ControlChildDistance pipeManager = newPipe.GetComponent<ControlChildDistance>();

			float newPipeGapY = pipeGapY + Random.Range(-pipeGapYRandomness, pipeGapYRandomness);
            if (newPipeGapY < minPipeGapY) newPipeGapY = minPipeGapY;
            pipeManager.verticalDistance = newPipeGapY;

			pipeManager.Init();
		}

        destroyMapTrigger.localPosition = new Vector2((prevPipePos.x + pipeGapX) * 2, 0);
		MapRespawner mapRespawner = GetComponentInChildren<MapRespawner>();

		if (finishVisualization == FinishVisualization.hideWithoutGap)
		{
			finish.localPosition = new Vector3(prevPipePos.x, 0, 20);
			mapRespawner.relativeSpawnLocation = new Vector2(prevPipePos.x, 0);
		}
		else
		{
			finish.localPosition = new Vector3(prevPipePos.x + pipeGapX, 0, 20);
			mapRespawner.relativeSpawnLocation = new Vector2(prevPipePos.x + pipeGapX, 0);
		}
	}

	public enum FinishVisualization
	{
		show,
		hideWithGap,
		hideWithoutGap
	}
}
