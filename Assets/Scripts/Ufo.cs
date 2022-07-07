using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : Enemy
{
	private float horizontalSpeed = default;
	private float xLimit = default;
	private Vector2 targetPosition = default;
	private int direction;
	private Transform leftSpawnPoint = default;
	private Transform rightSpawnPoint = default;

	private void Start()
	{
		SetRandomPosition();
		GetData();
	}
	private void SetRandomPosition()
	{
		bool random = Random.value > 0.5f;
		if (random)
		{
			transform.position = leftSpawnPoint.position;
			direction = 1;
		}
		else
		{
			transform.position = rightSpawnPoint.position;
			direction = -1;
		}
	}
	public void SetSpawnPoints(Transform leftSpawnPoint, Transform rightSpawnPoint)
	{
		this.leftSpawnPoint = leftSpawnPoint;
		this.rightSpawnPoint = rightSpawnPoint;
	}
	private void Update()
    {
		UfoMovement();
	}
	private void GetData()
	{
		targetPosition = transform.position;
		horizontalSpeed = GameData.UfoSpeed;
		xLimit = GameData.ScreenWidth + 0.5f;
	}
	private void UfoMovement()
	{
		targetPosition.x += direction*Time.deltaTime * horizontalSpeed;
		CheckLimit();
		transform.position = targetPosition;
	}

	private void CheckLimit()
	{
		if ((direction == 1 && targetPosition.x >= xLimit) || (direction == -1 && targetPosition.x <= -xLimit)) Destroy(); 
	}

	public override void Destroy(float destroyTime = 0)
	{
		base.Destroy(destroyTime);
	}
}
