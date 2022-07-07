using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{
	[Header("Spawn point")]
	[SerializeField] private Transform bulletSpawnPoint = default;

	private GameObject bulletPrefab = default;
	private GameObject newBullet = default;
	private float bulletSpeed = default;
	private float moveTime = default;
	private float shootTime = default;
    private float speed = default;
	private float time1 = default;
	private float time2 = default;
	private void Start()
	{
		GetData();
		bulletSpeed = EnemiesSpawner.Spawner.AlienBulletSpeed;
		speed = EnemiesSpawner.Spawner.AlienSpeed;
		bulletPrefab = EnemiesSpawner.Spawner.AlienBullet;

		EnemiesSpawner.Spawner.OnDirectionChanged += LowerRow;
	}
	private void Update()
    {
		AlienMovement();
		AlienShooting();
	}
	private void GetData()
	{
		moveTime = EnemiesSpawner.Spawner.AlienMoveTime;
		shootTime = EnemiesSpawner.Spawner.AlienTimeForNextShoot;
	}
	private void LowerRow()
	{
		GetData();
		transform.Translate(new Vector2(0, -speed));
	}
	private void AlienMovement()
	{
		time1 += Time.deltaTime;
		if (time1 > moveTime)
		{
			transform.Translate(new Vector2(EnemiesSpawner.Spawner.AliensXMoveDirection * speed, 0));
			time1 = 0;
		}
	}

	private void AlienShooting()
	{
		time2 += Time.deltaTime;
		if (time2 > shootTime)
		{
			int randomNumber = Random.Range(0, 100);
			if (randomNumber <= EnemiesSpawner.Spawner.AlienShootProbability)
			{
				newBullet = Instantiate(bulletPrefab, bulletSpawnPoint);
				newBullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
			}
			time2 = 0;
		}
	}

	public override void Destroy(float destroyTime = 0)
	{
		EnemiesSpawner.Spawner.OnDirectionChanged -= LowerRow;
		EnemiesSpawner.Spawner.AlienKilled();
		base.Destroy(destroyTime);
	}
}
