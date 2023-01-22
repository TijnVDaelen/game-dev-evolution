using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretchEffect : MonoBehaviour
{
	public IEnumerator PlayEffect()
	{
		transform.localScale += new Vector3(.2f, -.2f, 0);
		yield return new WaitForSeconds(0.1f);

		transform.localScale += new Vector3(-.2f, .2f, 0);
		yield return new WaitForSeconds(0.11f);

		transform.localScale += new Vector3(.1f, -.1f, 0);
		yield return new WaitForSeconds(0.14f);

		transform.localScale = new Vector3(1, 1, 1);
	}
}
