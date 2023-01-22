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

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		squashStretchEffect = GetComponentInChildren<SquashStretchEffect>();
		rb.gravityScale = gravityScale;
	}

	private void OnJump()
	{
		if (FlappyBirdGameManager.i.gameOver) return;
		rb.velocity = Vector2.up * jumpPower;
		if (squashStretchEffect != null) StartCoroutine(squashStretchEffect.PlayEffect());
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
