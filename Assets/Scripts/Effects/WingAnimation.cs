using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAnimation : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private bool isProcedural = false;
	[SerializeField] private float staticFlapWaitTime = 0.1f;
	[SerializeField] private float proceduralFlapWaitTime = 0.015f;
	[SerializeField] private float proceduralUpSpeed = 0.08f;

	private List<float> staticFlapYScales = new List<float>() { 1, 0.33f, -0.33f, -1, -0.33f, 0.33f };
	private List<float> proceduralFlapYScales = new List<float>() { 0.33f, 0.44f, 0.55f, 0.66f, -0.77f, 0.88f, 0.99f, 1f, 0.99f, 0.88f, 0.77f, 0.66f, 0.55f, 0.44f, 0.33f };

	private float wingScaleY;
	private float wingScaleYMin = -1;
	private bool proceduralFlapAnimActive;

	[Header("References")]
	[SerializeField] private Transform wing;

	private void Awake()
	{
		if (!isProcedural) StartCoroutine(FlapStatic());
	}

	private IEnumerator FlapStatic()
	{
		for (int i = 0; i < staticFlapYScales.Count; i++)
		{
			wingScaleY = staticFlapYScales[i];
			transform.localScale = new Vector3(1, wingScaleY, 1);
			yield return new WaitForSeconds(staticFlapWaitTime);
		}

		StartCoroutine(FlapStatic());
	}

	public IEnumerator FlapProcedural()
	{
		if (!isProcedural || proceduralFlapAnimActive) yield break;

		proceduralFlapAnimActive = true;

		for (int i = 0; i < proceduralFlapYScales.Count; i++)
		{
			wingScaleY = proceduralFlapYScales[i];
			transform.localScale = new Vector3(1, wingScaleY, 1);
			yield return new WaitForSeconds(proceduralFlapWaitTime);
		}

		proceduralFlapAnimActive = false;
	}

	private void FixedUpdate()
	{
		if (isProcedural && wingScaleY > wingScaleYMin && !proceduralFlapAnimActive)
		{
			// Wing goes up because bird is falling
			wingScaleY = wingScaleY - proceduralUpSpeed;
			transform.localScale = new Vector3(1, wingScaleY, 1);
		}
	}
}
