using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Stats")]
    [SerializeField] private float jumpPower = 3f;
    [SerializeField] private float gravityScale = 0.6f;

	[Header("References")]
    private Rigidbody2D rb;
	private SquashStretchEffect squashStretchEffect;
	private WingAnimation wingAnim;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		squashStretchEffect = GetComponentInChildren<SquashStretchEffect>();
		wingAnim = GetComponentInChildren<WingAnimation>();
		rb.gravityScale = gravityScale;
	}

	private void OnJump()
	{
		if (FlappyBirdGameManager.i.gameOver) return;
		rb.velocity = Vector2.up * jumpPower;
		if (squashStretchEffect != null) StartCoroutine(squashStretchEffect.PlayEffect());
		if (wingAnim != null) StartCoroutine(wingAnim.FlapProcedural());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Die();
	}

	public void Die()
	{
		FlappyBirdGameManager.i.GameOver();
	}
}
