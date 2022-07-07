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
		bulletSpeed = GameManager.Manager.AlienBulletSpeed;
		speed = GameManager.Manager.AlienSpeed;
		bulletPrefab = GameManager.Manager.AlienBullet;

		GameManager.Manager.OnDirectionChanged += LowerRow;
	}
	private void Update()
    {
		AlienMovement();
		AlienShooting();
	}
	private void GetData()
	{
		moveTime = GameManager.Manager.AlienMoveTime;
		shootTime = GameManager.Manager.AlienTimeForNextShoot;
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
			transform.Translate(new Vector2(GameManager.Manager.AliensXMoveDirection * speed, 0));
			time1 = 0;
		}
	}

	private void AlienShooting()
	{
		time2 += Time.deltaTime;
		if (time2 > shootTime)
		{
			int randomNumber = Random.Range(0, 100);
			if (randomNumber <= GameManager.Manager.AlienShootProbability)
			{
				newBullet = Instantiate(bulletPrefab, bulletSpawnPoint);
				newBullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
			}
			time2 = 0;
		}
	}

	public override void Destroy(float destroyTime = 0)
	{
		GameManager.Manager.OnDirectionChanged -= LowerRow;
		GameManager.Manager.AlienKilled();
		base.Destroy(destroyTime);
	}
}
