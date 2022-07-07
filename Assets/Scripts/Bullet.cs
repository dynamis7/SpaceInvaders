using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody = default;
	[SerializeField] private float timeToDestroy = default;

	private float speed = default;
	private float time = default;
 	private void Awake()
	{
		transform.parent = null;
	}
	private void Start()
	{
		rigidbody.velocity = Vector2.up * speed;
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (time > timeToDestroy) Destroy();
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	public void SetBulletSpeed(float speed)
	{
		this.speed = speed;
	}
}
